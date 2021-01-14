using AdminLTE.Core;
using AdminLTE.Model;
using AdminLTE.Repository;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Principal;

namespace AdminLTE.Manager
{
    public class CredentialManager : ICredentialManager
    {
        private readonly DbContext Context;
        private readonly IPrincipal User;
        IUserRepository UserRepository;
        IRoleRepository RoleRepository;

        public CredentialManager(DbContext context, IPrincipal user)
        {
            Context = context;
            User = user;
            UserRepository = new UserRepository(context, user);
            RoleRepository = new RoleRepository(context, user);
        }

        public Role AddRole(Role role)
        {
            var result = RoleRepository.Add(role);
            Context.SaveChanges();
            return result;
        }

        public Role DeleteRole(Role role)
        {
            var result = RoleRepository.Remove(role);
            Context.SaveChanges();
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
            Context.SaveChanges();
            return result;
        }

        public User GetUserForUserName(string userName)
        {
            return UserRepository.Find(c => c.UserName == userName || c.Email == userName).FirstOrDefault();
        }
    }
}
