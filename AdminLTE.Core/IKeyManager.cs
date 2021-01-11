using AdminLTE.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdminLTE.Model;

namespace AdminLTE.Core
{
    public interface IKeyManager : IManager
    {
        IEnumerable<KeyValue> GetKeyValues(int keyTypeId);
        IEnumerable<KeyType> GetKeyTypes();
    }
}
