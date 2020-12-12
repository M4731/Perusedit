using Microsoft.AspNet.Identity;
using Perusedit.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Perusedit.Controllers
{
    public class SubjectController : Controller
    {

        private readonly ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Details(int id)
        {
            if (TempData.ContainsKey("msg"))
            {
                ViewBag.msg = TempData["msg"];
            }

            var h = db.Subjects.Include("Responses").First(s => s.Id == id);
            return View(h);
        }

        [Authorize]
        public ActionResult Create(int id)
        {
            ViewBag.CategoryId = id;
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult Create(Subject sub)
        {
            try
            {
                db.Subjects.Add(sub);
                db.SaveChanges();
                TempData["msg"] = "Subiectul a fost adaugat.";
                return Redirect("/Category/Index/" + sub.CategoryId);
            }
            catch
            {
                return View(sub);
            }
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            var ui = System.Web.HttpContext.Current.User;
            var c = db.Subjects.Find(id);
            if (ui.Identity.GetUserId() == c.UserId)
                return View(c);
            else
                return Redirect("/Category/Index/" + c.CategoryId);
        }

        [HttpPut]
        [Authorize]
        public ActionResult Edit(int id, Subject sub)
        {
            var ui = System.Web.HttpContext.Current.User;
            if (ui.Identity.GetUserId() == sub.UserId)
            {
                try
                {
                    var subj = db.Subjects.Find(id);
                    if (TryUpdateModel(subj))
                    {
                        subj.Title = sub.Title;
                        subj.Text = sub.Text;
                        db.SaveChanges();
                        TempData["msg"] = "Subiectul a fost editat.";
                    }
                    else
                    {
                        return View(sub);
                    }
                    return RedirectToAction("Index", "Category", new { id = subj.CategoryId });
                }
                catch (Exception e)
                {
                    return View(sub);
                }
            }
            else
                return View(sub);
        }

        [HttpDelete]
        [Authorize]
        public ActionResult Delete(int id)
        {
            var ui = System.Web.HttpContext.Current.User;
            var c = db.Subjects.Find(id);
            if (ui.Identity.GetUserId() == c.UserId || ui.IsInRole("Moderator") || ui.IsInRole("Admin"))
            {
                db.Subjects.Remove(c);
                db.SaveChanges();
                TempData["msg"] = "Subiectul a fost sters.";
            }
            return RedirectToAction("Index", "Category", new { id = c.CategoryId });
        }
    }
}
