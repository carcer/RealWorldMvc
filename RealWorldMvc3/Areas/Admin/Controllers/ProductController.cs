using System;
using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using RealWorldMvc3.Areas.Admin.Models;
using RealWorldMvc3.Controllers;
using RealWorldMvc3.Core.Attributes;
using RealWorldMvc3.Core.Domain;
using RealWorldMvc3.Core.Repositories;
using RealWorldMvc3.Models;

namespace RealWorldMvc3.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IRepository repository;
        private readonly IMappingEngine mappingEngine;

        public ProductController(IRepository repository, IMappingEngine mappingEngine)
        {
            this.repository = repository;
            this.mappingEngine = mappingEngine;
        }

        //
        // GET: /Admin/Product/

        public ActionResult Index()
        {
            var categories = repository.FindAll<Product>();

            return AutoMappedView<IEnumerable<ProductDetail>>(categories);
        }

        //
        // GET: /Admin/Product/Details/5

        public ActionResult Details(Guid id)
        {
            var product = repository.FindById<Product>(id);

            return AutoMappedView<ProductDetail>(product);
        }

        //
        // GET: /Admin/Product/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Admin/Product/Create

        [HttpPost]
        public ActionResult Create(ProductUpdate productUpdate)
        {
            if (!ModelState.IsValid) return View(productUpdate);

            var product = mappingEngine.Map<ProductUpdate, Product>(productUpdate);

            repository.Save(product);

            return RedirectToAction("Index");
        }

        //
        // GET: /Admin/Product/Edit/5

        [RequiresCategoryList]
        public ActionResult Edit(Guid id)
        {
            var product = repository.FindById<Product>(id);

            return AutoMappedView<ProductUpdate>(product);
        }

        //
        // POST: /Admin/Product/Edit/5

        [HttpPost]
        [RequiresCategoryList]
        public ActionResult Edit(ProductUpdate productUpdate)
        {
            if (!ModelState.IsValid) return View(productUpdate);

            var product = mappingEngine.Map<ProductUpdate, Product>(productUpdate);

            repository.Save(product);

            return RedirectToAction("Index");
        }

        //
        // GET: /Admin/Product/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Admin/Product/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
