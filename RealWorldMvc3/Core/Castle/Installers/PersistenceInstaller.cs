using Castle.Facilities.AutoTx;
using Castle.Facilities.NHibernateIntegration;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using RealWorldMvc3.Core.Persistence;
using RealWorldMvc3.Core.Repositories;

namespace RealWorldMvc3.Core.Castle.Installers
{
    public class PersistenceInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            RegisterTransactionFacility(container);
            RegisterNHibernateIntegration(container);
            RegisterRepository(container);
        }

        protected virtual void RegisterTransactionFacility(IWindsorContainer container)
        {
            container.AddFacility<TransactionFacility>();
        }

        protected virtual void RegisterNHibernateIntegration(IWindsorContainer container)
        {
            RegisterNHibernateConfiguration(container);
            RegisterConfigurationBuilder(container);
            RegisterNHibernateFacility(container);
        }

        protected virtual void RegisterNHibernateFacility(IWindsorContainer container)
        {
            container
                .AddFacility<NHibernateFacility>("nhibernatefacility",
f => f.ConfigurationBuilder<FluentNHibernateConfigurationBuilder>().IsWeb());
        }

        protected virtual void RegisterConfigurationBuilder(IWindsorContainer container)
        {
            container.Register(Component.For<IConfigurationBuilder>()
                                   .ImplementedBy<FluentNHibernateConfigurationBuilder>());
        }

        protected void RegisterNHibernateConfiguration(IWindsorContainer container)
        {
            container.Register(Component
                                   .For<INHibernateConfiguration>()
                                   .ImplementedBy<DefaultNHibernateConfiguration>()
                );
        }

        private void RegisterRepository(IWindsorContainer container)
        {
            container.Register(
                Component.For<IRepository>().ImplementedBy<NHibernateRepository>()
                );
        }
    }
}