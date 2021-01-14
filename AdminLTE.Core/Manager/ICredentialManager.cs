using AdminLTE.Interface;
using AdminLTE.Model;
using System.Collections.Generic;

namespace AdminLTE.Core
{
    public interface ICredentialManager : IManager
    {
        IEnumerable<User> GetAllUser();
        User GetUser(string id);
        User GetUserForUserName(string userName);
        Role GetRole(string id);
        IEnumerable<Role> GetAllRoles();
        Role AddRole(Role role);
        Role UpdateRole(Role role);
        Role DeleteRole(Role role);
    }
}
