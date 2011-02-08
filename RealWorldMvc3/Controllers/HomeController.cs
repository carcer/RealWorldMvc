using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Castle.Core.Logging;

namespace RealWorldMvc3.Controllers
{
    public class HomeController : Controller
    {
        private ILogger logger = NullLogger.Instance;

        public ILogger Logger { set { logger = value; }
        }

        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";

            logger.Info("Hello world");

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
