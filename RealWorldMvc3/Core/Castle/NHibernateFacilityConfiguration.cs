using System;
using System.Collections.Generic;
using Castle.Core.Configuration;
using Castle.Facilities.NHibernateIntegration;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace RealWorldMvc3.Core.Castle
{
    public class NHibernateFacilityConfiguration
    {
        private readonly IWindsorContainer container;
        private bool facilityHasBeenRegistered;
        private readonly IConfigurationStore configurationStore;

        private const string FacilityKey = "nhibernateIntegration";

        private Type configurationBuilderType;

        public NHibernateFacilityConfiguration(IWindsorContainer container, IConfigurationStore configurationStore)
        {
            this.container = container;
            this.configurationStore = configurationStore;

            InitaliseConfiguration();
        }

        private void InitaliseConfiguration()
        {
            var configuration = ConfigurationHelper.CreateConfiguration(null, "facility", new Dictionary<string, object>());
            configurationStore.AddFacilityConfiguration(FacilityKey, configuration);
        }

        private IConfiguration GetConfiguration()
        {
            return configurationStore.GetFacilityConfiguration(FacilityKey);
        }


        public NHibernateFacilityConfiguration ConfigurationBuilder<T>()
            where T : IConfigurationBuilder
        {
            configurationBuilderType = typeof(T);
            container.Register(Component.For<IConfigurationBuilder>().ImplementedBy(configurationBuilderType));
            var config = GetConfiguration();
            config.Attributes["configurationBuilder"] = configurationBuilderType.AssemblyQualifiedName;

            return this;
        }

        public NHibernateFacilityConfiguration CreateFactory(Action<NHibernateFactoryConfiguration> func)
        {
            var factoryConfig = new NHibernateFactoryConfiguration();
            func(factoryConfig);
            var configuration = GetConfiguration();
            factoryConfig.Configure(configuration);

            if (!facilityHasBeenRegistered)
            {
                container.AddFacility(FacilityKey, new NHibernateFacility());
                facilityHasBeenRegistered = true;
            }

            return this;
        }

        public class NHibernateFactoryConfiguration
        {
            protected string Name { get; set; }

            protected bool IsWeb { get; set; }

            public NHibernateFactoryConfiguration Named(string name)
            {
                Name = name;

                return this;
            }

            public void Configure(IConfiguration configuration)
            {
                var factoryConfig = ConfigurationHelper.CreateConfiguration(configuration, "factory", new Dictionary<string, string>());
                factoryConfig.Attributes.Add("isWeb", IsWeb.ToString());
                factoryConfig.Attributes.Add("Id", Name);
            }

            public NHibernateFactoryConfiguration EnabledWeb()
            {
                IsWeb = true;

                return this;
            }
        }

    }
}