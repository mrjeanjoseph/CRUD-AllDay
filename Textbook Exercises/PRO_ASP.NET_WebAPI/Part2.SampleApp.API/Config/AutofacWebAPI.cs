using Autofac;
using Autofac.Integration.WebApi;
using PingYourPackage.Domain;
using System.Data.Entity;
using System.Reflection;
using System.Web.Http;

namespace PingYourPackage.WebAPI
{
    public class AutofacWebAPI
    {        
        public static void Initialize(HttpConfiguration config)
        {
            Initialize(config, RegisterServices(new ContainerBuilder()));
        }

        public static void Initialize(HttpConfiguration config, IContainer container)
        {
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static IContainer RegisterServices(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            containerBuilder.RegisterAssemblyTypes(Assembly.GetEntryAssembly()).PropertiesAutowired();

            //EF DbContext
            //containerBuilder.RegisterType<EntitiesContext>().As<DbContext>().InstancePerApiRequest();
            containerBuilder.RegisterType<EntitiesContext>().As<DbContext>().InstancePerRequest();

            //Repositories
            containerBuilder.RegisterGeneric(typeof(EntityRepository<>))
                .As(typeof(IEntityRepository<>)).InstancePerRequest();

            //Services
            containerBuilder.RegisterType<CryptoService>().As<ICryptoService>().InstancePerRequest();
            containerBuilder.RegisterType<MembershipService>().As<IMembershipService>().InstancePerRequest();
            containerBuilder.RegisterType<ShipmentService>().As<IShipmentService>().InstancePerRequest();

            //Registration goes here
            return containerBuilder.Build();
        }
    }
}
