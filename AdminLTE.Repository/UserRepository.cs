using AdminLTE.Core;
using AdminLTE.Model;
using System.Data.Entity;
using System.Security.Principal;

namespace AdminLTE.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DbContext context, IPrincipal user) : base(context, user)
        {
        }
    }
}
