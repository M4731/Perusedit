using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Perusedit.Models;

namespace Perusedit.Controllers
{
    public class ResponseController : Controller
    {
        private DatabaseContext db = new DatabaseContext();
        // GET: Response
        public ActionResult Index()
        {
            return View();
        }

        // GET: Response/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Response/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Response/Create
        [HttpPost]
        public ActionResult Create(Response res)
        {
            try
            {
                // TODO: Add insert logic here
                db.Responses.Add(res);
                db.SaveChanges();
                return Redirect("/Subject/Details/" + res.SubjectId);
            }
            catch
            {
                return View();
            }
        }

        // GET: Response/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Response/Edit/5
        [HttpPut]
        public ActionResult Edit(int id, Response res)
        {
            try
            {
                Debug.WriteLine(res.Text);
                var q = db.Responses.Find(id);
                if(TryUpdateModel(q))
                {
                    q.Text = res.Text;
                    db.SaveChanges();
                }
                return Redirect("/Subject/Details/" + q.SubjectId);
            }
            catch
            {
                return View();
            }
        }

        private void del(Response r)
        {
            var lista = new List<Response>();
            foreach(var morti in r.Responses)
            {
                lista.Add(new Response(morti));
            }
            foreach (var i in lista)
            {
                var vv = db.Responses.Include("Responses").First(s => s.Id == i.Id);
                del(vv);
            }
            var v = db.Responses.Include("Responses").First(s => s.Id == r.Id);
            db.Responses.Remove(r);
            db.SaveChanges();
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            try
            {
                var v = db.Responses.Include("Responses").First(s => s.Id == id);
                del(v);
                return Redirect("/Subject/Details/" + v.SubjectId);
            }
            catch (Exception e)
            {
                //Debug.WriteLine(e.InnerException);
                return View();
            }
        }
    }
}
