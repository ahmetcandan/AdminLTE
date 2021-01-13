using AdminLTE.Model;
using AdminLTE.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdminLTE.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RolesController : BaseController
    {
        public ActionResult Index()
        {
            return PartialView();
        }

        // GET: Roles
        public ActionResult List()
        {
            var result = (from c in UnitOfWork.CredentialManager.GetAllRoles()
                          select new RoleView
                          {
                              Id = c.Id,
                              RoleName = c.Name
                          });
            return PartialView(result.ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            return PartialView(new RoleView());
        }

        public ActionResult Create(RoleView instance)
        {
            var role = new Role
            {
                Name = instance.RoleName
            };
            UnitOfWork.CredentialManager.AddRole(role);
            return PartialView(instance);
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            throw new UserException(Translate("user-not-found"));
            var role = UnitOfWork.CredentialManager.GetRole(id);
            return PartialView(new RoleView
            {
                Id = role.Id,
                RoleName = role.Name
            });
        }

        [HttpPost]
        public ActionResult Edit(RoleView instance)
        {
            var role = UnitOfWork.CredentialManager.GetRole(instance.Id);
            role.Name = instance.RoleName;
            UnitOfWork.CredentialManager.UpdateRole(role);
            return PartialView(instance);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                UnitOfWork.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}