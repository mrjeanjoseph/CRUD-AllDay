using Moq;
using Ninject;
using SportsStore.Domain;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SportsStore.Web.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private readonly IKernel _kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            _kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new List<Product>
            {
                new Product { ProductName = "Football", Price = 25 },
                new Product { ProductName = "Football", Price = 25 },
                new Product { ProductName = "Football", Price = 25 },
                new Product { ProductName = "Football", Price = 25 },
                new Product { ProductName = "Football", Price = 25 },
            });

            _kernel.Bind<IProductRepository>().ToConstant(mock.Object);
        }

    }
}