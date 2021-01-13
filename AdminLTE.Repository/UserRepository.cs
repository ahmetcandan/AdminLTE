using AdminLTE.Core;
using AdminLTE.Model;
using System.Data.Entity;

namespace AdminLTE.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DbContext context) : base(context)
        {
        }
    }
}
