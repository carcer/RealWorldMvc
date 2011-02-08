using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using RealWorldMvc3.Core.Domain;

namespace RealWorldMvc3.Core.Mapping.Overrides
{
    public class CategoryOverride : IAutoMappingOverride<Category>
    {
        public void Override(AutoMapping<Category> mapping)
        {
            mapping.HasMany(x => x.Products).AsSet().Cascade.All();
        }
    }
}