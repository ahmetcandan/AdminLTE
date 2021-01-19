using AdminLTE.Core;
using System.Web.Mvc;

namespace AdminLTE.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}
