using AdminLTE.Core;
using AdminLTE.Model;
using AdminLTE.Repository;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminLTE.Manager
{
    public class CredentialManager : ICredentialManager
    {
        private readonly DbContext context;
        IUserRepository UserRepository;
        IRoleRepository RoleRepository;

        public CredentialManager(DbContext context)
        {
            this.context = context;
            UserRepository = new UserRepository(context);
            RoleRepository = new RoleRepository(context);
        }

        public Role AddRole(Role role)
        {
            var result = RoleRepository.Add(role);
            context.SaveChanges();
            return result;
        }

        public Role DeleteRole(Role role)
        {
            var result = RoleRepository.Remove(role);
            context.SaveChanges();
            return result;
        }

        public IEnumerable<Role> GetAllRoles()
        {
            return RoleRepository.GetAll().ToList();
        }

        public IEnumerable<User> GetAllUser()
        {
            return UserRepository.GetAll().ToList();
        }

        public User GetUser(string id)
        {
            return UserRepository.Get(id);
        }

        public Role GetRole(string id)
        {
            return RoleRepository.Get(id);
        }

        public Role UpdateRole(Role role)
        {
            var result = RoleRepository.Update(role);
            context.SaveChanges();
            return result;
        }
    }
}
