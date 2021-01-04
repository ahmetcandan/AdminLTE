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
        private DbModelContext db = new DbModelContext();

        public ActionResult Index()
        {
            return PartialView();
        }

        // GET: Roles
        public ActionResult List()
        {
            var result = (from c in db.Roles
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
            var role = new IdentityRole
            {
                Name = instance.RoleName
            };
            db.Roles.Add(role);
            db.SaveChanges();
            return PartialView(instance);
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            throw new UserException(Translate("user-not-found"));
            var role = db.Roles.Find(id);
            return PartialView(new RoleView
            {
                Id = role.Id,
                RoleName = role.Name
            });
        }

        [HttpPost]
        public ActionResult Edit(RoleView instance)
        {
            var role = db.Roles.Find(instance.Id);
            role.Name = instance.RoleName;
            db.SaveChanges();
            return PartialView(instance);
        }
    }
}