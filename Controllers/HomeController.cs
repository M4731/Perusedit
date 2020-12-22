using Perusedit.Models;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
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
            Debug.WriteLine(str.Length);
            ViewBag.Subjects = db.Subjects.Where(sub => sub.Text.Contains(str) | sub.Title.Contains(str));
            ViewBag.Responses = db.Responses.Where(r => r.Text.Contains(str));

            return View();
        }

    }
}
