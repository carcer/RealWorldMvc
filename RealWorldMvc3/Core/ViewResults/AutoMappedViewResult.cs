using System;
using System.Web.Mvc;

namespace RealWorldMvc3.Core.ViewResults
{
    public class AutoMappedViewResult : ViewResult
    {
        private readonly Type viewModelType;

        public static Func<object, Type, Type, object> Map = (entity, entityType, dtoType) =>
        {
            throw new InvalidOperationException(
                "The Mapping function must be set on the AutoMappedViewResult class");
        };

        public Type ViewModelType
        {
            get { return viewModelType; }
        }

        public AutoMappedViewResult(Type viewModelType)
        {
            this.viewModelType = viewModelType;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            ViewData.Model = Map(ViewData.Model, ViewData.Model.GetType(), ViewModelType);

            base.ExecuteResult(context);
        }
    }
}