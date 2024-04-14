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

            //_kernel.Bind<IDiscountCalculator>().To<DiscountValueCalculator>();

            //_kernel.Bind<IDiscountCalculator>()
            //    .To<DiscountValueCalculator>()
            //        .WithPropertyValue("DiscountSize", 50M);

            _kernel.Bind<IDiscountCalculator>()
                .To<DiscountValueCalculator>()
                    .WithConstructorArgument("discountParam", 50M);

            _kernel.Bind<IDiscountCalculator>()
                .To<FlexibleDiscountCalculator>()
                .WhenInjectedExactlyInto<LinqValueCalculator>();
        }
    }
}