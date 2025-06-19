using BMYLBH2025_SDDAP.Models;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace BMYLBH2025_SDDAP
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            UnityConfig.RegisterComponents();
            
            // Initialize database with complete schema and test
            DatabaseInitializer.InitializeDatabase();
            
            // Test database connection and data (for debugging)
            System.Diagnostics.Debug.WriteLine("=== Testing Database Connection ===");
            TestDbConnection.TestConnection();
        }
    }
}
