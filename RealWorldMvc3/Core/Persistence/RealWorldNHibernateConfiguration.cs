using System;
using FluentNHibernate.Automapping;
using RealWorldMvc3.Core.Domain;
using Storm.Utils.Extensions;

namespace RealWorldMvc3.Core.Persistence
{
    internal class RealWorldNHibernateConfiguration : DefaultAutomappingConfiguration
    {
        public override bool ShouldMap(Type type)
        {
            return type.CanBeCastTo<Entity>() && base.ShouldMap(type);
        }
    }
}