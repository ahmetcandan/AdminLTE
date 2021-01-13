using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using AdminLTE.Model;
using AdminLTE.Models;
using AdminLTE.DataAccess;
using AdminLTE.Manager;

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