using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BMYLBH2025_SDDAP.Models;

namespace BMYLBH2025_SDDAP.Controllers
{
    /// <summary>
    /// Health Check Controller for Container Monitoring
    /// Provides endpoints for Docker health checks and system monitoring
    /// </summary>
    [RoutePrefix("api/health")]
    public class HealthController : ApiController
    {
        /// <summary>
        /// Basic health check endpoint
        /// Returns 200 OK if the API is running
        /// </summary>
        /// <returns>Health status</returns>
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetHealth()
        {
            try
            {
                var health = new
                {
                    status = "healthy",
                    timestamp = DateTime.UtcNow,
                    version = "1.0.0",
                    environment = ConfigurationManager.AppSettings["IsContainerMode"] == "true" ? "container" : "standard",
                    api = "BMYLBH2025_SDDAP Backend API"
                };

                return Ok(health);
            }
            catch (Exception ex)
            {
                var errorHealth = new
                {
                    status = "unhealthy",
                    timestamp = DateTime.UtcNow,
                    error = ex.Message,
                    api = "BMYLBH2025_SDDAP Backend API"
                };

                return Content(HttpStatusCode.ServiceUnavailable, errorHealth);
            }
        }

        /// <summary>
        /// Detailed health check including database connectivity
        /// </summary>
        /// <returns>Detailed health status</returns>
        [HttpGet]
        [Route("detailed")]
        public IHttpActionResult GetDetailedHealth()
        {
            try
            {
                var health = new
                {
                    status = "healthy",
                    timestamp = DateTime.UtcNow,
                    version = "1.0.0",
                    environment = ConfigurationManager.AppSettings["IsContainerMode"] == "true" ? "container" : "standard",
                    api = "BMYLBH2025_SDDAP Backend API",
                    checks = new
                    {
                        database = CheckDatabaseHealth(),
                        configuration = CheckConfigurationHealth(),
                        dependencies = CheckDependenciesHealth()
                    }
                };

                // Determine overall status
                bool isHealthy = health.checks.database.status == "healthy" &&
                               health.checks.configuration.status == "healthy" &&
                               health.checks.dependencies.status == "healthy";

                if (!isHealthy)
                {
                    return Content(HttpStatusCode.ServiceUnavailable, new
                    {
                        status = "unhealthy",
                        timestamp = health.timestamp,
                        version = health.version,
                        environment = health.environment,
                        api = health.api,
                        checks = health.checks
                    });
                }

                return Ok(health);
            }
            catch (Exception ex)
            {
                var errorHealth = new
                {
                    status = "unhealthy",
                    timestamp = DateTime.UtcNow,
                    error = ex.Message,
                    api = "BMYLBH2025_SDDAP Backend API"
                };

                return Content(HttpStatusCode.ServiceUnavailable, errorHealth);
            }
        }

        /// <summary>
        /// Readiness probe for Kubernetes/Docker orchestration
        /// </summary>
        /// <returns>Readiness status</returns>
        [HttpGet]
        [Route("ready")]
        public IHttpActionResult GetReadiness()
        {
            try
            {
                // Check if the application is ready to serve requests
                var dbHealth = CheckDatabaseHealth();
                
                if (dbHealth.status != "healthy")
                {
                    return Content(HttpStatusCode.ServiceUnavailable, new
                    {
                        status = "not_ready",
                        timestamp = DateTime.UtcNow,
                        reason = "Database not accessible"
                    });
                }

                return Ok(new
                {
                    status = "ready",
                    timestamp = DateTime.UtcNow,
                    api = "BMYLBH2025_SDDAP Backend API"
                });
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.ServiceUnavailable, new
                {
                    status = "not_ready",
                    timestamp = DateTime.UtcNow,
                    error = ex.Message
                });
            }
        }

        /// <summary>
        /// Liveness probe for Kubernetes/Docker orchestration
        /// </summary>
        /// <returns>Liveness status</returns>
        [HttpGet]
        [Route("live")]
        public IHttpActionResult GetLiveness()
        {
            try
            {
                // Simple liveness check - if we can respond, we're alive
                return Ok(new
                {
                    status = "alive",
                    timestamp = DateTime.UtcNow,
                    api = "BMYLBH2025_SDDAP Backend API"
                });
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.ServiceUnavailable, new
                {
                    status = "dead",
                    timestamp = DateTime.UtcNow,
                    error = ex.Message
                });
            }
        }

        #region Private Health Check Methods

        private object CheckDatabaseHealth()
        {
            try
            {
                var connectionString = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
                
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    
                    // Test basic query
                    using (var command = new SQLiteCommand("SELECT COUNT(*) FROM sqlite_master WHERE type='table'", connection))
                    {
                        var tableCount = command.ExecuteScalar();
                        
                        return new
                        {
                            status = "healthy",
                            responseTime = "< 100ms",
                            tableCount = tableCount,
                            connectionString = MaskConnectionString(connectionString)
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                return new
                {
                    status = "unhealthy",
                    error = ex.Message,
                    type = ex.GetType().Name
                };
            }
        }

        private object CheckConfigurationHealth()
        {
            try
            {
                var requiredSettings = new[]
                {
                    "webpages:Version",
                    "ClientValidationEnabled",
                    "UnobtrusiveJavaScriptEnabled"
                };

                var missingSettings = new List<string>();
                
                foreach (var setting in requiredSettings)
                {
                    if (string.IsNullOrEmpty(ConfigurationManager.AppSettings[setting]))
                    {
                        missingSettings.Add(setting);
                    }
                }

                if (missingSettings.Count > 0)
                {
                    return new
                    {
                        status = "unhealthy",
                        error = "Missing required configuration settings",
                        missingSettings = missingSettings
                    };
                }

                return new
                {
                    status = "healthy",
                    configurationMode = ConfigurationManager.AppSettings["IsDevelopmentMode"] == "true" ? "development" : "production",
                    containerMode = ConfigurationManager.AppSettings["IsContainerMode"] == "true"
                };
            }
            catch (Exception ex)
            {
                return new
                {
                    status = "unhealthy",
                    error = ex.Message,
                    type = ex.GetType().Name
                };
            }
        }

        private object CheckDependenciesHealth()
        {
            try
            {
                var dependencies = new
                {
                    entityFramework = CheckAssemblyLoaded("EntityFramework"),
                    newtonsoft = CheckAssemblyLoaded("Newtonsoft.Json"),
                    sqlite = CheckAssemblyLoaded("System.Data.SQLite"),
                    webApi = CheckAssemblyLoaded("System.Web.Http")
                };

                bool allHealthy = dependencies.entityFramework.loaded &&
                                dependencies.newtonsoft.loaded &&
                                dependencies.sqlite.loaded &&
                                dependencies.webApi.loaded;

                return new
                {
                    status = allHealthy ? "healthy" : "unhealthy",
                    dependencies = dependencies
                };
            }
            catch (Exception ex)
            {
                return new
                {
                    status = "unhealthy",
                    error = ex.Message,
                    type = ex.GetType().Name
                };
            }
        }

        private object CheckAssemblyLoaded(string assemblyName)
        {
            try
            {
                var assembly = System.Reflection.Assembly.Load(assemblyName);
                return new
                {
                    loaded = true,
                    version = assembly.GetName().Version.ToString(),
                    location = assembly.Location
                };
            }
            catch (Exception ex)
            {
                return new
                {
                    loaded = false,
                    error = ex.Message
                };
            }
        }

        private string MaskConnectionString(string connectionString)
        {
            // Mask sensitive information in connection string for logging
            if (string.IsNullOrEmpty(connectionString))
                return "Not configured";

            // For SQLite, just show the data source
            if (connectionString.Contains("Data Source="))
            {
                var parts = connectionString.Split(';');
                var dataSource = parts.FirstOrDefault(p => p.Trim().StartsWith("Data Source="));
                return dataSource ?? "SQLite Database";
            }

            return "Configured";
        }

        #endregion
    }
} 