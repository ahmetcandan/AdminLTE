using AdminLTE.Core;
using AdminLTE.Model;
using System.Data.Entity;

namespace AdminLTE.Repository
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        public RoleRepository(DbContext context) : base(context)
        {
        }
    }
}
