using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace RealWorldMvc3.Core.Castle.Installers
{
    public class MvcControllerInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(AllTypes.FromThisAssembly()
                     .BasedOn<IController>()
                    .If(t => t.Name.EndsWith("Controller"))
                   .Configure(c => c.Named(c.ServiceType.FullName).LifeStyle.Transient));
        }
    }
}