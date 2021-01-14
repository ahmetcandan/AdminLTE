using AdminLTE.Core;
using AdminLTE.Model;
using System.Data.Entity;

namespace AdminLTE.Repository
{
    public class KeyTypeRepository : Repository<KeyType>, IKeyTypeRepository
    {
        public KeyTypeRepository(DbContext context) : base(context)
        {
        }
    }
}
