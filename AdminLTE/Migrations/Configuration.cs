namespace AdminLTE.Migrations
{
    using AdminLTE.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AdminLTE.Models.DbModelContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? new ApplicationUserManager(new UserStore<ApplicationUser>(new DbModelContext()));
            }
            private set
            {
                _userManager = value;
            }
        }

        protected override void Seed(AdminLTE.Models.DbModelContext context)
        {
            var adminRole = new IdentityRole
            {
                Name = "Admin"
            };
            var userRole = new IdentityRole
            {
                Name = "User"
            };
            context.Roles.Add(adminRole);
            context.Roles.Add(userRole);
            context.SaveChanges();

            var admin = new ApplicationUser()
            {
                UserName = "admin",
                Email = "admin@adminlte.com",
                LockoutEnabled = true
            };

            var user = new ApplicationUser()
            {
                UserName = "user",
                Email = "user@adminlte.com",
                LockoutEnabled = true
            };

            UserManager.Create(admin, "Admin123!");
            UserManager.Create(user, "User123!");
            UserManager.AddToRoles(admin.Id, adminRole.Name, userRole.Name);
            UserManager.AddToRoles(user.Id, userRole.Name);
        }
    }
}
