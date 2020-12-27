using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminLTE.Models
{
    public class UserView
    {
        public UserView()
        {
            Roles = new List<RoleView>();
        }

        public string Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public bool RememberMe { get; set; }
        public string RoleNames { get; set; }

        public IList<RoleView> Roles { get; set; }
    }

    public class RoleView
    {
        public string Id { get; set; }
        public string RoleName { get; set; }
    }
}