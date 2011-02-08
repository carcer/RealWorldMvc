using FluentMvc.Configuration;
using FluentMvc.Conventions;
using RealWorldMvc3.Controllers;
using RealWorldMvc3.Core.Filters;

namespace RealWorldMvc3.Core.FMvc
{
    public class TestFilterRegistration : IFilterConvention
    {
        public void ApplyConvention(IFilterRegistration filterRegistration)
        {
            
        }
    }
}