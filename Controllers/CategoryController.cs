using Perusedit.Models;
using System;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;

namespace Perusedit.Controllers
{
    public class CategoryController : Controller
    {

        private readonly ApplicationDbContext db = new ApplicationDbContext();
        int limit = 3;

        public ActionResult Index(int id, int? page)
        {

            if (TempData.ContainsKey("msg"))
            {
                ViewBag.msg = TempData["msg"];
            }
            int pag = page ?? 1;

            var c = db.Categories.Find(id);
            ViewBag.MaxPage = (c.Subjects.Count + limit - 1) / 3;
            c.Subjects = c.Subjects.Skip((pag - 1) * limit).Take(limit).ToArray();
            ViewBag.Page = pag;
            return View(c);
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
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
                return View("~/Views/Home/Index.cshtml", c);
            }
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            var c = db.Categories.Find(id);
            db.Categories.Remove(c);
            db.SaveChanges();
            TempData["msg"] = "Categoria a fost stearsa.";
            return RedirectToAction("Index", "Home");
        }


        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            var c = db.Categories.Find(id);
            return View(c);

        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
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
