using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using FluentMvc;
using RealWorldMvc3.Core.Castle.Components;
using RealWorldMvc3.Core.Filters;

namespace RealWorldMvc3.Core.Castle.Installers
{
    public class FluentMvcInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {

            container.Register(AllTypes.FromThisAssembly().BasedOn<ActionFilterBase>().Configure(x => x.LifeStyle.PerWebRequest));

            var provider = FluentMvcConfiguration
                .ConfigureFilterProvider(config =>
                                             {
                                                 config.ResolveWith(new WindsorObjectFactory(container));
                                                 config.FilterConventions.LoadFromAssemblyContaining<FluentMvcInstaller>();
                                             });

            FilterProviders.Providers.Add(provider);
        }
    }
}