using Perusedit.Models;
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
    }
}
