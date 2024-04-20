using Ninject;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SportStore.Infrastructure {
    public class NinjectDependencyResolver : IDependencyResolver {

        private readonly IKernel _kernel;

        public NinjectDependencyResolver(IKernel kernelParm) {
            _kernel = kernelParm;
            AddBindings();
        }

        private void AddBindings() {
            // put bindings here
            throw new NotImplementedException();
        }

        public object GetService(Type serviceType) {
            return _kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType) {
            return _kernel.GetAll(serviceType);
        }
    }
}