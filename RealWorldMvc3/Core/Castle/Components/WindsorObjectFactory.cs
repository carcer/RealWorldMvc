using System;
using Castle.Windsor;
using FluentMvc.Configuration;

namespace RealWorldMvc3.Core.Castle.Components
{
    public class WindsorObjectFactory : FluentMvcObjectFactory
    {
        private readonly IWindsorContainer container;

        public WindsorObjectFactory(IWindsorContainer container)
        {
            this.container = container;
        }

        public override TFilter Resolve<TFilter>(Type type)
        {
            return (TFilter)container.Resolve(type);
        }

        public override void BuildUpProperties<T>(T filter)
        {
        }
    }
}