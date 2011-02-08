using System.Configuration;
using Castle.Components.DictionaryAdapter;
using Castle.Facilities.FactorySupport;
using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using RealWorldMvc3.Core.Castle.Components;

namespace RealWorldMvc3.Core.Castle.Installers
{
    public class FactoriesInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.AddFacility<TypedFactoryFacility>();
            container.AddFacility<FactorySupportFacility>();

            container.Register(
                Component.For<ModelBinderFactoryComponentSelector, ITypedFactoryComponentSelector>(),
                Component.For<IModelBinderFactory>().AsFactory(x => x.SelectedWith<ModelBinderFactoryComponentSelector>()),
                Component.For<ISiteConfiguration>()
                    .UsingFactoryMethod(() => new DictionaryAdapterFactory()
                                                  .GetAdapter<ISiteConfiguration>(ConfigurationManager.AppSettings))

                );
        }
    }
}