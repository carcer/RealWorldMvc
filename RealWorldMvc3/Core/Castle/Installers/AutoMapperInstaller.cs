using AutoMapper;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using RealWorldMvc3.Core.ViewResults;
using Storm.Utils.Extensions;

namespace RealWorldMvc3.Core.Castle.Installers
{
    public class AutoMapperInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            Mapper.Initialize(x => x.ConstructServicesUsing(container.Resolve));

            RegisterProfiles(container);
            RegisterMapperEngine(container);
            SetAutoMappedViewResultMapMethod();
        }

        private void RegisterMapperEngine(IWindsorContainer container)
        {
            container.Register(
                Component.For<IMappingEngine>().Instance(Mapper.Engine)
                );
        }

        private void SetAutoMappedViewResultMapMethod()
        {
            AutoMappedViewResult.Map = (entity, entityType, dtoType) => Mapper.Map(entity, entityType, dtoType);
        }

        private void RegisterProfiles(IWindsorContainer container)
        {
            container.Register(AllTypes.FromThisAssembly().BasedOn<Profile>());
            var profiles = container.ResolveAll<Profile>();

            profiles.Each(Mapper.AddProfile);
        }
    }
}