using Perusedit.Models;
using System;
using System.Web.Mvc;

namespace Perusedit.Controllers
{
    public class CategoryController : Controller
    {

        private readonly DatabaseContext db = new DatabaseContext();
        // GET: Category
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult New(Category c)
        {
            try
            {
                db.Categories.Add(c);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            catch (Exception e)
            {
                return Redirect("GOOG");
            }
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {


            var c = db.Categories.Find(id);
            db.Categories.Remove(c);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");



        }



    }
}
