using System;
using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using RealWorldMvc3.Areas.Admin.Models;
using RealWorldMvc3.Controllers;
using RealWorldMvc3.Core.Domain;
using RealWorldMvc3.Core.Repositories;
using RealWorldMvc3.Models;

namespace RealWorldMvc3.Areas.Admin.Controllers
{
    public class CategoryController : BaseController
    {
        private readonly IRepository repository;
        private readonly IMappingEngine mappingEngine;

        public CategoryController(IRepository repository, IMappingEngine mappingEngine)
        {
            this.repository = repository;
            this.mappingEngine = mappingEngine;
        }

        //
        // GET: /Admin/Category/

        public ActionResult Index()
        {
            var categories = repository.FindAll<Category>();

            return AutoMappedView<IEnumerable<CategoryDetail>>(categories);
        }

        //
        // GET: /Admin/Category/Details/5

        public ActionResult Details(Guid id)
        {
            var category = repository.FindById<Category>(id);

            return AutoMappedView<CategoryDetail>(category);
        }

        //
        // GET: /Admin/Category/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Admin/Category/Create

        [HttpPost]
        public ActionResult Create(CategoryInput categoryInput)
        {
            if (!ModelState.IsValid) return View(categoryInput);

            var category = mappingEngine.Map<CategoryInput, Category>(categoryInput);

            repository.Save(category);

            return RedirectToAction("Index");
        }

        //
        // GET: /Admin/Category/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            var category = repository.FindById<Category>(id);

            return AutoMappedView<CategoryInput>(category);
        }

        //
        // POST: /Admin/Category/Edit/5

        [HttpPost]
        public ActionResult Edit(CategoryInput categoryInput)
        {
            if (!ModelState.IsValid) return View(categoryInput);

            var category = mappingEngine.Map<CategoryInput, Category>(categoryInput);

            repository.Save(category);

            return RedirectToAction("Index");
        }

        //
        // GET: /Admin/Category/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Admin/Category/Delete/5

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
