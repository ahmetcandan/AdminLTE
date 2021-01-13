using AdminLTE.Interface;
using AdminLTE.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminLTE.Core
{
    public interface IKeyValueRepository : IRepository<KeyValue>
    {
        IQueryable<KeyValue> GetKeyValuesForKeyTypeId(int keyTypeId);
    }
}
