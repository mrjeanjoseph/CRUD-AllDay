using Moq;
using Ninject;
using SportStore.Domain;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SportsStore.Web.Infrastructure {

    public class DependencyResolver : IDependencyResolver {

        private readonly IKernel _kernel;

        public DependencyResolver(IKernel kernelParm) {
            _kernel = kernelParm;
            AddBindings();
        }

        public object GetService(Type serviceType) {
            return _kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType) {
            return _kernel.GetAll(serviceType);
        }

        private void AddBindings() {
            Mock<ICommodityRepository> mock = new Mock<ICommodityRepository>();

            mock.Setup(m => m.Commodities).Returns(new List<Commodity> {
                new Commodity { Name = "iPad Pro 12.9", Price = 1550M},
                new Commodity { Name = "HP Envy 3VX", Price = 1170M},
                new Commodity { Name = "Boze Speaker - Athem", Price = 510M},
                new Commodity { Name = "Air Pods Pro", Price = 269M},
                new Commodity { Name = "Dell XPS Platnum", Price = 2550M},
            });

            _kernel.Bind<ICommodityRepository>().ToConstant(mock.Object);
        }
    }
}