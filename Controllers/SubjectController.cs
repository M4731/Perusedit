using Newtonsoft.Json;
using Perusedit.Models;
using System;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;

namespace Perusedit.Controllers
{
    public class SubjectController : Controller
    {

        private readonly DatabaseContext db = new DatabaseContext();

        public ActionResult Details(int id)
        {
            if (TempData.ContainsKey("msg"))
            {
                ViewBag.msg = TempData["msg"];
            }

            var h = db.Subjects.Include("Responses").First(s => s.Id == id);
            Debug.WriteLine(h.Responses.Count);
            return View(h);
        }

        public ActionResult Create(int id)
        {
            ViewBag.CategoryId = id;
            return View();
        }

        [HttpPost]
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

        public ActionResult Edit(int id)
        {
            var c = db.Subjects.Find(id);
            return View(c);
        }

        [HttpPut]
        public ActionResult Edit(int id, Subject sub)
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

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var c = db.Subjects.Find(id);
            db.Subjects.Remove(c);
            db.SaveChanges();
            TempData["msg"] = "Subiectul a fost sters.";
            return RedirectToAction("Index", "Category", new { id = c.CategoryId });
        }
    }
}
