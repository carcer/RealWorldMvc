using Castle.Facilities.Logging;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using RealWorldMvc3.Core.Castle.Interceptors;

namespace RealWorldMvc3.Core.Castle.Installers
{
    public class LoggingInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
    container.AddFacility("logging", new LoggingFacility(LoggerImplementation.Log4net, "log4net.config"));

            RegisterInterceptor(container);
        }

        private void RegisterInterceptor(IWindsorContainer container)
        {
            container.Register(Component.For<LoggingInterceptor>());
        }
    }
}