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
                {
                    if (_unitOfWork == null)
                        return new UnitOfWork(new DbModelContext(), User);
                    //lock (_lock)
                    //{
                    //    if (_unitOfWork == null)
                    //        return new UnitOfWork(new DbModelContext());

                    //}
                }
                return _unitOfWork;
            }
        }

        public BaseApiController()
        {

        }
    }
}