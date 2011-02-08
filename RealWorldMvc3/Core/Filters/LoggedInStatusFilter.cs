using System.Web.Mvc;
using RealWorldMvc3.Models;

namespace RealWorldMvc3.Core.Filters
{
    public class LoggedInStatusFilter : ActionFilterBase
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if ( !(filterContext.Result is ViewResult) ) return;

            var viewResult = filterContext.Result as ViewResult;

            viewResult.ViewBag.LoggedInStatus = new LoggedInStatusModel
                                                    {
                                                        LoggedIn = true,
                                                        Username = "chriscanal"
                                                    };
        }
    }
}