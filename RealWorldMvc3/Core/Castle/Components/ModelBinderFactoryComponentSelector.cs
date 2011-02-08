using System;
using System.Collections;
using System.Reflection;
using Castle.Facilities.TypedFactory;

namespace RealWorldMvc3.Core.Castle.Components
{
    public class ModelBinderFactoryComponentSelector : DefaultTypedFactoryComponentSelector
    {
        protected override TypedFactoryComponent BuildFactoryComponent(MethodInfo method, string componentName, Type componentType, IDictionary additionalArguments)
        {
            return new ModelBinderFactoryComponent(componentName, componentType, additionalArguments);
        }
    }
}