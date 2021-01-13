using AdminLTE.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdminLTE.Model;

namespace AdminLTE.Core
{
    public interface ICredentialManager : IManager
    {
        IEnumerable<User> GetAllUser();
        User GetUser(string id);
        Role GetRole(string id);
        IEnumerable<Role> GetAllRoles();
        Role AddRole(Role role);
        Role UpdateRole(Role role);
        Role DeleteRole(Role role);
    }
}
