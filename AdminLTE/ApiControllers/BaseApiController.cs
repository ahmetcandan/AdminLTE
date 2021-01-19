using AdminLTE.Core;
using AdminLTE.DataAccess;
using AdminLTE.Manager;
using System.Web.Http;

namespace AdminLTE.ApiControllers
{
    public class BaseApiController : ApiController
    {
        private IUnitOfWork _unitOfWork;

        protected IUnitOfWork UnitOfWork
        {
            get
            {
                return _unitOfWork;
            }
        }

        public BaseApiController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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