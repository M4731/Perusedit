using Newtonsoft.Json;
using Perusedit.Models;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
namespace Perusedit.Controllers
{
    public class HomeController : Controller
    {

        private readonly DatabaseContext db = new DatabaseContext();
        public ActionResult Index()
        {
            var CatList = from category in db.Categories select category;
            ViewBag.Categories = CatList;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
