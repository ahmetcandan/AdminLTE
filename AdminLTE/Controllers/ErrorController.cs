using System.Web.Mvc;

namespace AdminLTE.Controllers
{
    public class ErrorController : BaseController
    {
        [HttpGet]
        public ActionResult InternalServerError()
        {
            return View();
        }

        [HttpGet]
        public ActionResult NotFound()
        {
            return View();
        }
    }
}
