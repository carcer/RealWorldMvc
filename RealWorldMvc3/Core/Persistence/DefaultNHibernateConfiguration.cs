using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using RealWorldMvc3.Core.Domain;
using RealWorldMvc3.Core.Mapping.Conventions;
using Storm.Utils.Extensions;

namespace RealWorldMvc3.Core.Persistence
{
    public class DefaultNHibernateConfiguration : INHibernateConfiguration
    {
        public virtual NHibernate.Cfg.Configuration GetConfiguration()
        {
            var cfg = GetFluentConfiguration();

            return cfg.BuildConfiguration();
        }

        public IPersistenceConfigurer GetConnection()
        {
            return MsSqlConfiguration.MsSql2008
                .ConnectionString(c => c.FromConnectionStringWithKey("Default"))
                .AdoNetBatchSize(100)
                .FormatSql().ShowSql();
        }

        public FluentConfiguration GetFluentConfiguration()
        {
            var config = Fluently.Configure()
                .Database(GetConnection)
                .Mappings(m => m.AutoMappings.Add(BuildAutoMap()));

            return config;
        }

        private AutoPersistenceModel BuildAutoMap()
        {
            return AutoMap.AssemblyOf<Entity>(new RealWorldNHibernateConfiguration())
                .Conventions.AddFromAssemblyOf<DefaultStormConventions>()
                .Alterations(alt => alt.AddFromAssemblyOf<DefaultStormConventions>())
                .UseOverridesFromAssemblyOf<DefaultStormConventions>()
                .IgnoreBase<Entity>();
        }
    }
}