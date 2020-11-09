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

            //var h = new Response();
            //h.Text = "ce text vrei";
            //h.FatherId = null;

            //var s = db.Responses.Include("Responses").First(m => m.Id == 3);
            //var h = new JsonSerializerSettings();
            //h.MaxDepth = 1;
            //h.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            //Debug.WriteLine(JsonConvert.SerializeObject(s.Responses, h));

            //db.SaveChanges();


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
