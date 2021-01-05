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

        public ActionResult Details(int id, string srt)
        {
            if (TempData.ContainsKey("msg"))
            {
                ViewBag.msg = TempData["msg"];
            }
            ViewBag.Srt = srt;

            var h = db.Subjects.Include("Responses").First(s => s.Id == id);

            foreach(var r in h.Responses)
            {
                if(srt == null)
                {
                    r.Responses = r.Responses.OrderByDescending(s => s.Responses.Count).ToList();
                }
                else
                {
                    r.Responses = r.Responses.OrderByDescending(s => s.Date).ToList();
                }
            }
            if (srt == null)
            {
                h.Responses = h.Responses.OrderByDescending(s => s.Responses.Count).ToList();
            }
            else
            {
                h.Responses = h.Responses.OrderByDescending(s => s.Date).ToList();
            }

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
            var p = db.Categories;
            ViewBag.Categories = p;
            if (ui.Identity.GetUserId() == c.UserId || ui.IsInRole("Moderator") || ui.IsInRole("Admin"))
                return View(c);
            else
                return Redirect("/Category/Index/" + c.CategoryId);
        }

        [HttpPut]
        [Authorize]
        public ActionResult Edit(int id, Subject sub)
        {
            System.Diagnostics.Debug.WriteLine(sub.CategoryId);
            var ui = System.Web.HttpContext.Current.User;
            var subj = db.Subjects.Find(id);
            var p = db.Categories;
            ViewBag.Categories = p;
            if (ui.Identity.GetUserId() == sub.UserId)
            {
                try
                {
                    subj.Title = sub.Title;
                    subj.Text = sub.Text;
                    db.SaveChanges();
                    TempData["msg"] = "Subiectul a fost editat.";


                }
                catch (Exception e)
                {
                    return View(sub);
                }
            }
            if (ui.IsInRole("Moderator") || ui.IsInRole("Admin"))
            {
                try
                {

                    subj.CategoryId = sub.CategoryId;
                    subj.Category = db.Categories.Find(sub.CategoryId);
                    db.SaveChanges();
                    TempData["msg"] = "Subiectul a fost editat.";
                }
                catch (Exception e)
                {
                    return View(sub);
                }
            }

            return RedirectToAction("Index", "Category", new { id = subj.CategoryId });
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
