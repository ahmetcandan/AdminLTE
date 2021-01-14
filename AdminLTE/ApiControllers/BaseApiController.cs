using AdminLTE.DataAccess;
using AdminLTE.Manager;
using System.Web.Http;

namespace AdminLTE.ApiControllers
{
    public class BaseApiController : ApiController
    {
        protected UnitOfWork UnitOfWork;

        public BaseApiController()
        {
            UnitOfWork = new UnitOfWork(new DbModelContext());
        }
    }
}