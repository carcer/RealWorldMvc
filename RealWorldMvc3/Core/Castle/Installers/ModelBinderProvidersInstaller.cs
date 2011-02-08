using System;
using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using RealWorldMvc3.Core.Castle.Components;

namespace RealWorldMvc3.Core.Castle.Installers
{
    public class ModelBinderProvidersInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IModelBinderProvider>().ImplementedBy<CastleModelBinderProvider>(),
                AllTypes.FromThisAssembly()
                    .BasedOn<IModelBinder>()
                    .Configure(x => x.Named(BuildName(x)))
                );
        }

        private string BuildName(ComponentRegistration registration)
        {
            return registration.Implementation.Name;
        }
    }

    public class ModelValidatorInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<ModelValidatorProvider>().ImplementedBy<CastleModelValidatorProvider>());
        }
    }
}