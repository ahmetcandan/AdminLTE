using System.Web.Mvc;
using System.Web.Routing;

namespace AdminLTE.Controllers
{
    public class HomeController : BaseController
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}
