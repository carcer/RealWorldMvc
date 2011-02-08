using System.Web.Mvc;
using RealWorldMvc3.Core.ViewResults;

namespace RealWorldMvc3.Controllers
{
    public abstract class BaseController : Controller
    {
        protected ActionResult AutoMappedView<TModel>(object entity)
        {
            ViewData.Model = entity;

            return new AutoMappedViewResult(typeof(TModel))
            {
                ViewData = ViewData,
                TempData = TempData
            };
        }
    }
}