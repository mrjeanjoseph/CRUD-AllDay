using Moq;
using Ninject;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Concrete;
using System;
using System.Configuration;
using System.Web.Mvc;
using System.Web.Routing;

namespace SportsStore.WebUI.Infrastructure {

    public class NinjectControllerFactory : DefaultControllerFactory {

        private readonly IKernel _nKernel;

        public NinjectControllerFactory() {

            _nKernel = new StandardKernel();
            AddBindings();
        }

        protected override IController GetControllerInstance(
            RequestContext requestContext, Type controllerType) {

            return controllerType == null ? null : (IController)_nKernel.Get(controllerType);
        }

        private void AddBindings() {
            // put bindings here
            Mock<IProductRepository> mock = new Mock<IProductRepository>();

            _nKernel.Bind<IProductRepository>().To<EFProductRepository>();

            EmailSettings emailSettings = new EmailSettings {
                WriteAsFile = bool.Parse(ConfigurationManager.AppSettings["Email.WriteAsFile"] ?? "false")
            };

            _nKernel.Bind<IOrderProcessor>().To<EmailOrderProcessor>()
                .WithConstructorArgument("settings", emailSettings);
        }
    }
}