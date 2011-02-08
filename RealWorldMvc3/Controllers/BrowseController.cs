using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Castle.Core;
using RealWorldMvc3.Core.Castle.Interceptors;
using RealWorldMvc3.Core.Domain;
using RealWorldMvc3.Core.Repositories;
using RealWorldMvc3.Models;

namespace RealWorldMvc3.Controllers
{
    [Interceptor(typeof(LoggingInterceptor))]
    public class BrowseController : BaseController
    {
        private readonly IRepository repository;

        public BrowseController(IRepository repository)
        {
            this.repository = repository;
        }

        public ActionResult Products(Guid id)
        {
            var category = repository.FindById<Category>(id);

            return AutoMappedView<BrowseProducts>(category);
        }

        public ActionResult Categories()
        {
            var categories = repository.FindAll<Category>();

            return AutoMappedView<IEnumerable<CategoryDetail>>(categories);
        }
    }
}
