namespace AdminLTE.Migrations
{
    using AdminLTE.Model;
    using AdminLTE.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AdminLTE.DataAccess.DbModelContext>
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
                return _userManager ?? new ApplicationUserManager(new UserStore<User>(new AdminLTE.DataAccess.DbModelContext()));
            }
            private set
            {
                _userManager = value;
            }
        }

        protected override void Seed(AdminLTE.DataAccess.DbModelContext context)
        {
            var adminRole = new Role
            {
                Name = "Admin"
            };
            var userRole = new Role
            {
                Name = "User"
            };
            context.Roles.Add(adminRole);
            context.Roles.Add(userRole);
            context.SaveChanges();

            var admin = new User()
            {
                UserName = "admin",
                Email = "admin@adminlte.com",
                LockoutEnabled = true
            };

            var user = new User()
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
