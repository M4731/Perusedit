using Newtonsoft.Json;
using Perusedit.Models;
using System;
using System.Diagnostics;
using System.Web.Mvc;

namespace Perusedit.Controllers
{
    public class SubjectController : Controller
    {

        private readonly DatabaseContext db = new DatabaseContext();
        // GET: Subject
        public ActionResult Index()
        {
            return View();
        }

        // GET: Subject/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Subject/Create
        public ActionResult Create(int id)
        {
            ViewBag.CategoryId = id;
            return View();
        }

        // POST: Subject/Create
        [HttpPost]
        public ActionResult Create(Subject sub)
        {
            var h = JsonConvert.SerializeObject(sub);
            var s = Request.Form.Get("CategoryId");
            Debug.WriteLine(h);
            Debug.WriteLine(s);
            try
            {
                db.Subjects.Add(sub);
                db.SaveChanges();
                return Redirect("/Category/Index/" + sub.CategoryId);//TOCHANGE


            }
            catch
            {
                return View();
            }
        }

        // GET: Subject/Edit/5
        public ActionResult Edit(int id)
        {
            var c = db.Subjects.Find(id);
            return View(c);
        }

        // POST: Subject/Edit/5
        [HttpPut]
        public ActionResult Edit(int id, Subject sub)
        {
            var h = JsonConvert.SerializeObject(sub);
            try
            {
                var subj = db.Subjects.Find(id);
                if (TryUpdateModel(subj))
                {

                    subj.Title = sub.Title;
                    subj.Text = sub.Text;
                    db.SaveChanges();
                }
                return RedirectToAction("Index", "Category", new { id = subj.CategoryId });
            }
            catch (Exception e)
            {
                return View("Goog");
            }
        }

        // GET: Subject/Delete/5

        [HttpDelete]
        public ActionResult Delete(int id)
        {


            var c = db.Subjects.Find(id);
            db.Subjects.Remove(c);
            db.SaveChanges();
            return RedirectToAction("Index", "Category", new { id = c.CategoryId });



        }
        // POST: Subject/Delete/5
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
