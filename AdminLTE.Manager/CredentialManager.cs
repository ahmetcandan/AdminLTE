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
        private IUserRepository _userRepository;
        private IRoleRepository _roleRepository;

        IUserRepository UserRepository
        {
            get
            {
                if (_userRepository == null)
                    _userRepository = new UserRepository(Context, User);
                return _userRepository;
            }
        }

        IRoleRepository RoleRepository
        {
            get
            {
                if (_roleRepository == null)
                    _roleRepository = new RoleRepository(Context, User);
                return _roleRepository;
            }
        }

        public CredentialManager(DbContext context, IPrincipal user)
        {
            Context = context;
            User = user;
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
            try
            {
                var result = RoleRepository.GetAll().ToList();
                return result;
            }
            catch (System.Exception ex)
            {
                throw;
            }
        }

        public IEnumerable<User> GetAllUser()
        {
            try
            {
                var result = UserRepository.GetAll().ToList();
                return result;
            }
            catch (System.Exception ex)
            {

                throw;
            }
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
