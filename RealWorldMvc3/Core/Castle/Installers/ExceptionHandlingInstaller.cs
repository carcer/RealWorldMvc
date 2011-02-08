using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Core;
using Castle.Core.Logging;
using Castle.DynamicProxy;
using Castle.MicroKernel;
using Castle.MicroKernel.ModelBuilder;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using RealWorldMvc3.Core.Castle.Components;

namespace RealWorldMvc3.Core.Castle.Installers
{
    public class ExceptionHandlingInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<ExceptionHandlingInterceptor>());
            container.Kernel.ComponentModelBuilder.AddContributor(new ExceptionHandlingCompnentModelBuilder(new[] { typeof(CastleModelBinderProvider) }));
        }
    }

    public class ExceptionHandlingCompnentModelBuilder : IContributeComponentModelConstruction
    {
        private readonly IEnumerable<Type> types;

        public ExceptionHandlingCompnentModelBuilder(IEnumerable<Type> types)
        {
            this.types = types;
        }

        public void ProcessModel(IKernel kernel, ComponentModel model)
        {
            if ( types.Contains(model.Implementation))
                model.Interceptors.Add(InterceptorReference.ForType<ExceptionHandlingInterceptor>());
        }
    }

    public class ExceptionHandlingInterceptor : StandardInterceptor
    {
        private ILogger logger = NullLogger.Instance;

        public ILogger Logger { set { logger = value; } }

        protected override void PerformProceed(IInvocation invocation)
        {
            try
            {
                invocation.Proceed();
            }
            catch (Exception ex)
            {
                logger.Error("An error occured", ex);
                throw;
            }
        }
    }
}