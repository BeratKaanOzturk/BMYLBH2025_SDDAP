using System;
using Unity;
using Unity.WebApi;
using System.Web.Http;
using BMYLBH2025_SDDAP.Models;
using BMYLBH2025_SDDAP.Services;
using System.Configuration;

namespace BMYLBH2025_SDDAP
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            var connStr = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;

            container.RegisterInstance<IDbConnectionFactory>(new SqliteConnectionFactory(connStr));
            container.RegisterType<IUserRepository, UserRepository>();
            container.RegisterType<IInventoryRepository, InventoryRepository>();
            container.RegisterType<IEmailService, EmailService>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}