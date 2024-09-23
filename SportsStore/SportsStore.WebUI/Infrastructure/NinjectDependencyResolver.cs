using Moq;
using Ninject;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Concrete;
using SportsStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;

namespace SportsStore.WebUI.Infrastructure {
    public class NinjectDependencyResolver : IDependencyResolver {
        private readonly IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam) {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType) {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType) {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings() {

            //All Merch Items go thru here
            Mock<IProductRepository> mockOld = new Mock<IProductRepository>();
            mockOld.Setup(m => m.Products).Returns(new List<Product> {
                new Product{ Name = "Dummy Product ", Price = 1560M},
                new Product{ Name = "Dummy Product ", Price = 1560M},
                new Product{ Name = "Dummy Product ", Price = 1560M},
                new Product{ Name = "Dummy Product ", Price = 1560M},
                new Product{ Name = "Dummy Product ", Price = 1560M}
            }); //We're not using this one

            //_kernel.Bind<IMerchRepo>().ToConstant(mock.Object);

            kernel.Bind<IProductRepository>().To<EFProductRepository>();
            EmailSettings emailsettings = new EmailSettings {
                WriteAsFile = bool.Parse(ConfigurationManager.AppSettings["Email.WriteAsFile"] ?? "false")
            };

            kernel.Bind<IOrderProcessor>().To<EmailOrderProcessor>().WithConstructorArgument("settings", emailsettings);

            //kernel.Bind<IAuthProvider>().To<FormsAuthProvider>(); We will have to do something about the auth
        }
    }
}