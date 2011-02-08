using System;
using System.Collections.Generic;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.AcceptanceCriteria;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.Conventions.Instances;

namespace RealWorldMvc3.Core.Mapping.Conventions
{
    public class DefaultStormConventions : IIdConvention, IHasManyToManyConvention, IClassConvention, IHasManyConvention
    {
        public void Apply(IIdentityInstance instance)
        {
            instance.Access.LowerCaseField();
            instance.Column(instance.EntityType.Name + "Id");
        }

        public void Apply(IClassInstance instance)
        {
            instance.Table(Inflector.Net.Inflector.Pluralize(instance.EntityType.Name));
        }

        public void Apply(IOneToManyCollectionInstance instance)
        {
            instance.LazyLoad();
        }

        public void Apply(IManyToManyCollectionInstance instance)
        {
            var names = new List<string>
                            {
                                instance.EntityType.Name,
                                instance.ChildType.Name
                            };

            names.Sort();

            instance.Table(string.Format("{0}To{1}", names[0], names[1]));
            instance.LazyLoad();
        }

    }
}