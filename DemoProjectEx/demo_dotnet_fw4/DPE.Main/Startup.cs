using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using DPE.Application.Interfaces;
using DPE.Application.Services;
using DPE.Domain.Aggregates.Person;
using DPE.Infrastructure.Persistence;
using DPE.Infrastructure.Repositories;
using Microsoft.Owin;
using Owin;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;

[assembly: OwinStartup(typeof(DPE.Main.Startup))]

namespace DPE.Main
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var builder = new ContainerBuilder();

            // Register MVC controllers
            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            // Register WebAPI controllers
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // Register DbContext
            builder.RegisterType<AdventureWorksDbContext>()
                   .AsSelf()
                   .InstancePerRequest();

            // Register Repositories
            builder.RegisterType<PersonRepository>()
                   .As<IPersonRepository>()
                   .InstancePerRequest();

            // Register Services
            builder.RegisterType<PersonService>()
                   .As<IPersonService>()
                   .InstancePerRequest();

            // Build container
            var container = builder.Build();

            // Set MVC Dependency Resolver
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            // Set WebAPI Dependency Resolver
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            ConfigureAuth(app);
        }
    }
}
