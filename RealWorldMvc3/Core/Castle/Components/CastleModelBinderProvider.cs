using System;
using System.Web.Mvc;

namespace RealWorldMvc3.Core.Castle.Components
{
    public class CastleModelBinderProvider : IModelBinderProvider
    {
        private readonly IModelBinderFactory modelBinderFactory;

        public CastleModelBinderProvider(IModelBinderFactory modelBinderFactory)
        {
            this.modelBinderFactory = modelBinderFactory;
        }

        public IModelBinder GetBinder(Type modelType)
        {
            var key = modelType.Name + "Binder";
            return modelBinderFactory.GetModelBinder(key);
        }
    }

    public interface IModelBinderFactory : IDisposable
    {
        IModelBinder GetModelBinder(string key);
    }

    public class LogOnModelBinder : DefaultModelBinder
    {
    }

    public class ContactModelBinder : DefaultModelBinder
    {
    }
}