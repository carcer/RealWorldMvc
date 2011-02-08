using System;
using System.Collections;
using System.Web.Mvc;
using Castle.Facilities.TypedFactory;
using Castle.MicroKernel;

namespace RealWorldMvc3.Core.Castle.Components
{
    public class ModelBinderFactoryComponent : TypedFactoryComponent
    {
        public ModelBinderFactoryComponent(string componentName, Type componentType, IDictionary additionalArguments) 
            : base(componentName, componentType, additionalArguments)
        {

        }

        public override object Resolve(IKernel kernel)
        {
            return kernel.HasComponent(ComponentName) ? base.Resolve(kernel) : new DefaultModelBinder();
        }
    }
}