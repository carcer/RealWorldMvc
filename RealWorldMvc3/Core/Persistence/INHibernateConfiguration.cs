using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;

namespace RealWorldMvc3.Core.Persistence
{
    public interface INHibernateConfiguration
    {
        NHibernate.Cfg.Configuration GetConfiguration();
        IPersistenceConfigurer GetConnection();
        FluentConfiguration GetFluentConfiguration();
    }
}