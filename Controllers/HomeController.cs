using Perusedit.Models;
using System;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Perusedit.Controllers
{
    public class HomeController : Controller
    {

        private readonly ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            if (TempData.ContainsKey("msg"))
            {
                ViewBag.msg = TempData["msg"];
            }

            var CatList = from category in db.Categories select category;
            ViewBag.Categories = CatList;
            return View();
        }
        public ActionResult Search(string str)
        {
            str = str ?? "";
            //Debug.WriteLine(str.Length);
            ViewBag.Subjects = db.Subjects.Where(sub => sub.Text.Contains(str) | sub.Title.Contains(str));
            ViewBag.Responses = db.Responses.Where(r => r.Text.Contains(str));

            return View();
        }

        public ActionResult Profyle(string yd)
        {
            try
            {
                //Debug.WriteLine(yd);
                var user = db.Users.First(u => u.Id == yd);
                var i = user.Roles.ToList()[user.Roles.Count-1].RoleId;
                ViewBag.Rol =  db.Roles.First(r => r.Id == i).Name;
                return View(user);
            }
            catch (Exception e)
            {
                return Redirect("/");
            }
        }

        [HttpPut]
        public ActionResult Pedit(string desc)
        {
            var user = db.Users.ToList().First(u => u.Id == User.Identity.GetUserId());
            if(TryUpdateModel(user))
            {
                user.Profil = desc;
                db.SaveChanges();
            }
            return Redirect("/Home/Profyle?yd=" + User.Identity.GetUserId());
        }

        [HttpPut]
        public ActionResult Rol(string yd)
        {
            var user = db.Users.ToList().First(u => u.Id == yd);
            if (TryUpdateModel(user))
            {
                var id_rol = user.Roles.ToList()[user.Roles.Count - 1].RoleId;
                var nume_rol = db.Roles.First(r => r.Id == id_rol).Name;

                if(nume_rol == "Moderator")
                {
                    var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
                    UserManager.RemoveFromRoles(yd,"Moderator");
                    UserManager.AddToRoles(yd, "User");
                }
                else if(nume_rol == "User")
                {
                    var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
                    UserManager.AddToRoles(yd,"Moderator");
                    UserManager.RemoveFromRoles(yd, "User");
                }
                db.SaveChanges();
            }
            return Redirect("/Home/Profyle?yd=" + yd);
        }
    }
}
