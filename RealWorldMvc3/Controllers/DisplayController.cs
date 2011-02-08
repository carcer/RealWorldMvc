using System;
using System.Web.Mvc;
using Castle.Core.Logging;
using RealWorldMvc3.Core.Domain;
using RealWorldMvc3.Core.Repositories;

namespace RealWorldMvc3.Controllers
{
    public class DisplayController : Controller
    {
        private readonly IRepository repository;
        private readonly ILogger logger;

        public DisplayController(IRepository repository, ILogger logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

        public ActionResult Product(Guid id)
        {
            logger.Info("Loading products: {0}", id);
            var product = repository.FindById<Product>(id);

            return View(product);
        }
    }
}