using AdminLTE.Core;
using AdminLTE.Model;
using System.Data.Entity;
using System.Linq;

namespace AdminLTE.Repository
{
    public class KeyValueRepository : Repository<KeyValue>, IKeyValueRepository
    {
        public KeyValueRepository(DbContext context) : base(context)
        {
        }

        public IQueryable<KeyValue> GetKeyValuesForKeyTypeId(int keyTypeId)
        {
            return Context.Set<KeyValue>().Where(c => c.KeyTypeId == keyTypeId);
        }
    }
}
