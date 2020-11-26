using Perusedit.Models;
using System;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;

namespace Perusedit.Controllers
{
    public class CategoryController : Controller
    {

        private readonly DatabaseContext db = new DatabaseContext();

        public ActionResult Index(int id)
        {
            if(TempData.ContainsKey("msg"))
            {
                ViewBag.msg = TempData["msg"];
            }

            var c = db.Categories.Include("Subjects").First(m => m.Id == id);
            return View(c);
        }


        [HttpPost]
        public ActionResult New(Category c)
        {
            try
            {
                db.Categories.Add(c);
                db.SaveChanges();
                TempData["msg"] = "Categoria a fost adaugata.";
                return RedirectToAction("Index", "Home");
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                var CatList = from category in db.Categories select category;
                ViewBag.Categories = CatList;
                return View("~/Views/Home/Index.cshtml",c);
            }
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var c = db.Categories.Find(id);
            db.Categories.Remove(c);
            db.SaveChanges();
            TempData["msg"] = "Categoria a fost stearsa.";
            return RedirectToAction("Index", "Home");
        }


        public ActionResult Edit(int id)
        {
            var c = db.Categories.Find(id);
            return View(c);

        }
        [HttpPut]
        public ActionResult Edit(int id, Category cat)
        {
            try
            {
                Category category = db.Categories.Find(id);
                if (TryUpdateModel(category))
                {
                    category.Name = cat.Name;

                    db.SaveChanges();
                    TempData["msg"] = "Categoria a fost editata.";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return View(cat);
                }
            }
            catch (Exception e)
            {
                return View(cat);
            }

        }


    }
}
