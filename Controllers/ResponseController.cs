using Microsoft.AspNet.Identity;
using Perusedit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Perusedit.Controllers
{
    public class ResponseController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        [Authorize]
        public ActionResult New(int id)
        {
            ViewBag.Sub = db.Responses.Find(id).SubjectId;
            ViewBag.Id = id;
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult New(Response res)
        {
            try
            {
                var r = db.Responses.Find(res.FatherId);
                res.SubjectId = r.SubjectId;
                res.Date = DateTime.Now;
                db.Responses.Add(res);
                db.SaveChanges();
                TempData["msg"] = "Raspunsul a fost adaugat.";
                return Redirect("/Subject/Details/" + res.SubjectId);
            }
            catch
            {
                ViewBag.Id = res.FatherId;
                ViewBag.Sub = res.SubjectId;
                return View(res);
            }
        }

        [Authorize]
        public ActionResult NewNull(int id)
        {
            ViewBag.Id = id;
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult NewNull(Response res)
        {
            try
            {
                res.FatherId = null;
                res.Date = DateTime.Now;
                db.Responses.Add(res);
                db.SaveChanges();
                TempData["msg"] = "Raspunsul a fost adaugat.";
                return Redirect("/Subject/Details/" + res.SubjectId);
            }
            catch
            {
                ViewBag.Id = res.SubjectId;
                return View(res);
            }
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            var r = db.Responses.Find(id);
            if (System.Web.HttpContext.Current.User.Identity.GetUserId() == r.UserId)
                return View(r);
            else
                return Redirect("/Subject/Details/" + r.SubjectId);
        }

        [HttpPut]
        [Authorize]
        public ActionResult Edit(int id, Response res)
        {
            var ui = System.Web.HttpContext.Current.User.Identity.GetUserId();
            var q = db.Responses.Find(id);
            if (ui != q.UserId)
                return Redirect("/Subject/Details/" + q.SubjectId);

            try
            {
                if (TryUpdateModel(q))
                {
                    q.Text = res.Text;
                    db.SaveChanges();
                    TempData["msg"] = "Raspunsul a fost editat.";
                }
                else
                {

                    return View(res);
                }
                return Redirect("/Subject/Details/" + q.SubjectId);
            }
            catch
            {
                return View(res);
            }
        }

        private void del(Response r)
        {
            var lista = new List<Response>();
            foreach (var mo in r.Responses)
            {
                lista.Add(new Response(mo));
            }
            foreach (var i in lista)
            {
                var vv = db.Responses.Include("Responses").First(s => s.Id == i.Id);
                del(vv);
            }
            db.Responses.Remove(r);
            db.SaveChanges();
        }

        [HttpDelete]
        [Authorize]
        public ActionResult Delete(int id)
        {
            var ui = System.Web.HttpContext.Current.User;

            try
            {
                var v = db.Responses.Include("Responses").First(s => s.Id == id);
                if (ui.IsInRole("Admin") || ui.IsInRole("Moderator") || ui.Identity.GetUserId() == v.UserId)
                {
                    del(v);
                    TempData["msg"] = "Raspunsul a fost sters.";
                    return Redirect("/Subject/Details/" + v.SubjectId);
                }
                else
                    return Redirect("/Subject/Details/" + v.SubjectId);


            }
            catch (Exception e)
            {
                var v = db.Responses.Find(id);
                return View("Details", "Subject", v.Subject);
            }
        }
    }
}
