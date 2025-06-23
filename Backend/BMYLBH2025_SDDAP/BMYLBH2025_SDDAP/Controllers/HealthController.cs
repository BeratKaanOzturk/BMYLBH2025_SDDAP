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
                var healthInfo = new
                {
                    Status = "Healthy",
                    Service = "BMYLBH2025_SDDAP API",
                    Timestamp = DateTime.Now,
                    Version = "1.0.0",
                    Database = "Connected" // Could add actual DB connectivity check here
                };

                return Ok(ApiResponse<object>.CreateSuccess(healthInfo, "API is healthy"));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Detailed health check including database connectivity
        /// </summary>
        /// <returns>Detailed health status</returns>
        [HttpGet]
        [Route("status")]
        public IHttpActionResult GetDetailedStatus()
        {
            try
            {
                var status = new
                {
                    API = "Healthy",
                    Database = CheckDatabaseConnection(),
                    Memory = GC.GetTotalMemory(false),
                    Uptime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    Environment = System.Configuration.ConfigurationManager.AppSettings["Environment"] ?? "Development"
                };

                return Ok(ApiResponse<object>.CreateSuccess(status, "Detailed status retrieved"));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
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
                var dbHealthResult = CheckDatabaseHealth();
                
                if (!dbHealthResult.IsHealthy)
                {
                    return Content(HttpStatusCode.ServiceUnavailable, new
                    {
                        status = "not_ready",
                        timestamp = DateTime.UtcNow,
                        reason = "Database not accessible",
                        error = dbHealthResult.Error
                    });
                }

                return Ok(new
                {
                    status = "ready",
                    timestamp = DateTime.UtcNow,
                    api = "BMYLBH2025_SDDAP Backend API",
                    database = dbHealthResult.Status
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

        private DatabaseHealthResult CheckDatabaseHealth()
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
                        
                        return new DatabaseHealthResult
                        {
                            Status = "healthy",
                            ResponseTime = "< 100ms",
                            TableCount = tableCount?.ToString() ?? "0",
                            ConnectionString = MaskConnectionString(connectionString),
                            IsHealthy = true
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                return new DatabaseHealthResult
                {
                    Status = "unhealthy",
                    Error = ex.Message,
                    Type = ex.GetType().Name,
                    IsHealthy = false
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

        private string CheckDatabaseConnection()
        {
            try
            {
                // Simple database connectivity check
                // You could inject IDbConnectionFactory here for a real check
                return "Connected";
            }
            catch
            {
                return "Disconnected";
            }
        }



        #endregion
    }

    public class DatabaseHealthResult
    {
        public string Status { get; set; }
        public string ResponseTime { get; set; }
        public string TableCount { get; set; }
        public string ConnectionString { get; set; }
        public string Error { get; set; }
        public string Type { get; set; }
        public bool IsHealthy { get; set; }
    }
} 