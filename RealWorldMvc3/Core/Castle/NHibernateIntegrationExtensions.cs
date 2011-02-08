using Castle.Windsor;

namespace RealWorldMvc3.Core.Castle
{
    public static class NHibernateIntegrationExtensions
    {
        public static NHibernateFacilityConfiguration NHibernateIntegration(this IWindsorContainer windsorContainer)
        {
            return new NHibernateFacilityConfiguration(windsorContainer, windsorContainer.Kernel.ConfigurationStore);
        }
    }
}