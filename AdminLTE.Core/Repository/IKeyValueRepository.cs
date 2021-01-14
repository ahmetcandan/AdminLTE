using AdminLTE.Interface;
using AdminLTE.Model;
using System.Linq;

namespace AdminLTE.Core
{
    public interface IKeyValueRepository : IRepository<KeyValue>
    {
        IQueryable<KeyValue> GetKeyValuesForKeyTypeId(int keyTypeId);
    }
}
