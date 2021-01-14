using AdminLTE.Core;
using AdminLTE.Model;
using System.Data.Entity;
using System.Security.Principal;

namespace AdminLTE.Repository
{
    public class KeyTypeRepository : Repository<KeyType>, IKeyTypeRepository
    {
        public KeyTypeRepository(DbContext context, IPrincipal user) : base(context, user)
        {
        }
    }
}
