using DemoMVCCore1.Data;
using DemoMVCCore1.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoMVCCore1.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDBContext _db;

        //Injecting database connection string using construtor injection
        public CategoryController(ApplicationDBContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> objList = _db.Category;
            return View(objList);
        }

        //Get
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category objCategory)
        {
            if (ModelState.IsValid)
            {
                _db.Category.Add(objCategory);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
                return View(objCategory);
        }

        //Get Edit
        public IActionResult Edit(int? id)
        {
            if(id==null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.Category.Find(id);
            if (obj == null)
                return NotFound();
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category objCategory)
        {
            if (ModelState.IsValid)
            {
                _db.Category.Update(objCategory);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
                return View(objCategory);
        }

        //Get Delete
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.Category.Find(id);
            if (obj == null)
                return NotFound();
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteCategory(int? id)
        {
            var obj = _db.Category.Find(id);
            if (obj == null)
                return NotFound();
            _db.Category.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
