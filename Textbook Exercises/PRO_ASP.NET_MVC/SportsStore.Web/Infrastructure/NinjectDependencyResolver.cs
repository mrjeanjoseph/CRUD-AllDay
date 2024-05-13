using Moq;
using Ninject;
using SportsStore.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;

namespace SportsStore.Web.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver {
        private readonly IKernel _kernel;

        public NinjectDependencyResolver(IKernel kernelParam) {
            _kernel = kernelParam;
            AddBindings();
        }

        private void AddBindings() {

            //All Merch Items go thru here
            Mock<IMerchandiseRepository> mockOld = new Mock<IMerchandiseRepository>();
            mockOld.Setup(m => m.Merchandises).Returns(new List<Merchandise> {
                new Merchandise{ Name = "Ipad Pro 12.9", Price = 1560M},
                new Merchandise{ Name = "Ipad Pro 12.9", Price = 1560M},
                new Merchandise{ Name = "Ipad Pro 12.9", Price = 1560M},
                new Merchandise{ Name = "Ipad Pro 12.9", Price = 1560M},
                new Merchandise{ Name = "Ipad Pro 12.9", Price = 1560M}
            }); //We're not using this one

            //_kernel.Bind<IMerchRepo>().ToConstant(mock.Object);
            _kernel.Bind<IMerchandiseRepository>().To<EFMerchRepo>();
            EmailSettings emailsettings = new EmailSettings
            {
                WriteAsFile = bool.Parse(ConfigurationManager.AppSettings["Email.WriteAsFile"] ?? "false")
            };

            _kernel.Bind<IOrderProceessor>().To<EmailOrderProcessor>()
                .WithConstructorArgument("settings", emailsettings);

            _kernel.Bind<IAuthProvider>().To<FormsAuthProvider>();

            // Attempting another object here
            Mock<IProductVendorRepo> mock = new Mock<IProductVendorRepo>();
            mock.Setup(m => m.ProductVendor).Returns(new List<ProductVendor> {
                new ProductVendor{ ProductID = 5, StandardPrice = 5233, LastReceiptDate = DateTime.Now}
            }); //Temporary
            _kernel.Bind<IProductVendorRepo>().ToConstant(mock.Object);

            //_kernel.Bind<IProductVendorRepo>().To<ProductVendorRepository>();

        }

        public object GetService(Type serviceType) {
            return _kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType) {
            return _kernel.GetAll(serviceType);
        }
    }
}