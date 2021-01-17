using AdminLTE.DataAccess;
using AdminLTE.Manager;
using System.Web.Http;

namespace AdminLTE.ApiControllers
{
    public class BaseApiController : ApiController
    {
        private UnitOfWork _unitOfWork;
        private static object _lock;

        protected UnitOfWork UnitOfWork
        {
            get
            {
                if (_unitOfWork == null)
                    return new UnitOfWork(DbModelContext.Create(), User);
                return _unitOfWork;
            }
        }

        public BaseApiController()
        {

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //UnitOfWork.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}