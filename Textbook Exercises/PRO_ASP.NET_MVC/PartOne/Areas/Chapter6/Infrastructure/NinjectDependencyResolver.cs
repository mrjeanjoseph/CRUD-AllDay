using EssentialTools.Models;
using Ninject;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace EssentialTools.Infrastructure {
    public class NinjectDependencyResolver : IDependencyResolver {
        private readonly IKernel _kernel;

        public NinjectDependencyResolver(IKernel kernelParam) {
            _kernel = kernelParam;
            AddBinding();
        }
        public object GetService(Type serviceType) {
            return _kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType) {
            return _kernel.GetAll(serviceType);
        }

        private void AddBinding() {
            _kernel.Bind<IValueCalculator>().To<LinqValueCalculator>();
        }
    }
}