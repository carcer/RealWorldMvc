using System;
using System.Linq;
using System.Web;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using RealWorldMvc3.Core.Castle.Components;
using Storm.Utils.Extensions;

namespace RealWorldMvc3.Core.Castle.Installers
{
    public class EmailerInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Kernel.AddHandlerSelector(new EmailerHandlerSelector(container.Kernel));
            container.Register(AllTypes.FromThisAssembly().BasedOn<IEmailer>().WithService.FirstInterface());

        }
    }

    public class EmailerHandlerSelector : IHandlerSelector
    {
        private readonly IKernel kernel;

        public EmailerHandlerSelector(IKernel kernel)
        {
            this.kernel = kernel;
        }

        public bool HasOpinionAbout(string key, Type service)
        {
            return service.CanBeCastTo<IEmailer>();
        }

        public IHandler SelectHandler(string key, Type service, IHandler[] handlers)
        {
            IHandler handler;

            if ( HttpContext.Current.Request.IsLocal )
                handler = handlers.First(x => x.ComponentModel.Implementation.Equals(typeof(OffLineEmailer)));
            else
            {
                var settings = kernel.Resolve<ISiteConfiguration>();
                handler = handlers.First(x => x.ComponentModel.Implementation.Name.Equals(settings.Emailer));
                kernel.ReleaseComponent(settings);
            }

            return handler;
        }
    }
}