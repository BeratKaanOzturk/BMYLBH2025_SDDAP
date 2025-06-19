using BMYLBH2025_SDDAP.Models;
using System.Configuration;
using System.Web.Http;
using Unity;
using Unity.WebApi;

namespace BMYLBH2025_SDDAP
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            var connStr = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;

            container.RegisterInstance<IDbConnectionFactory>(new SqliteConnectionFactory(connStr));
            container.RegisterType<IInventoryRepository, InventoryRepository>();
            container.RegisterType<IUserRepository, UserRepository>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}