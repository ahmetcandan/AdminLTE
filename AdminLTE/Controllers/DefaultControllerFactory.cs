using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using AdminLTE.Core;
using AdminLTE.DataAccess;
using AdminLTE.Manager;
using LightInject;

namespace AdminLTE.Controllers
{
    public class LightInjectDefaultControllerFactory : DefaultControllerFactory
    {
        public override IController CreateController(RequestContext requestContext, string controllerName)
        {
            return base.CreateController(requestContext, controllerName);
        }

        public override void ReleaseController(IController controller)
        {
            base.ReleaseController(controller);
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return base.GetControllerInstance(requestContext, controllerType);
        }
    }
}