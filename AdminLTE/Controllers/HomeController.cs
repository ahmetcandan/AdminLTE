using AdminLTE.Core;
using System.Web.Mvc;

namespace AdminLTE.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {

        }

        public HomeController(IUnitOfWork unitOfWork)
        {

        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}
