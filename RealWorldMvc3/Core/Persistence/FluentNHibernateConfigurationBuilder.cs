using Castle.Core.Configuration;
using Castle.Facilities.NHibernateIntegration;

namespace RealWorldMvc3.Core.Persistence
{
    public class FluentNHibernateConfigurationBuilder : IConfigurationBuilder
    {
        private readonly INHibernateConfiguration nhibernateConfiguration;

        public FluentNHibernateConfigurationBuilder(INHibernateConfiguration nhibernateConfiguration)
        {
            this.nhibernateConfiguration = nhibernateConfiguration;
        }

        public NHibernate.Cfg.Configuration GetConfiguration(IConfiguration config)
        {
            return nhibernateConfiguration.GetConfiguration();
        }
    }
}