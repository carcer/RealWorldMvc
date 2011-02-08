using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using RealWorldMvc3.Core.Domain;
using RealWorldMvc3.Core.Repositories;
using MvcContrib;

namespace RealWorldMvc3.Core.Attributes
{
    public class RequiresCategoryListAttribute : ActionFilterAttribute
    {
        public IRepository Repository { get; set; }

        public IMappingEngine MappingEngine { get; set; }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var categories = Repository.FindAll<Category>();
            var selectListItems = MappingEngine.Map<IEnumerable<Category>, IEnumerable<SelectListItem>>(categories);
            var controllerBase = filterContext.Controller;
            controllerBase.ViewData.Add(selectListItems);
        }
    }
}