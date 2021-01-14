using AdminLTE.Core;
using AdminLTE.Model;
using System.Data.Entity;
using System.Linq;
using System.Security.Principal;

namespace AdminLTE.Repository
{
    public class KeyValueRepository : Repository<KeyValue>, IKeyValueRepository
    {
        public KeyValueRepository(DbContext context, IPrincipal user) : base(context, user)
        {
        }

        public IQueryable<KeyValue> GetKeyValuesForKeyTypeId(int keyTypeId)
        {
            return Context.Set<KeyValue>().Where(c => c.KeyTypeId == keyTypeId);
        }
    }
}
