using AdminLTE.Core;
using AdminLTE.Model;
using System.Data.Entity;
using System.Security.Principal;

namespace AdminLTE.Repository
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        public RoleRepository(DbContext context, IPrincipal user) : base(context, user)
        {
        }
    }
}
