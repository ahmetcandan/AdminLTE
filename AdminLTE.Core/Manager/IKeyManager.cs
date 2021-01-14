using AdminLTE.Interface;
using AdminLTE.Model;
using System.Collections.Generic;

namespace AdminLTE.Core
{
    public interface IKeyManager : IManager
    {
        IEnumerable<KeyValue> GetKeyValues(int keyTypeId);
        IEnumerable<KeyType> GetKeyTypes();


        KeyType GetKeyType(int keyTypeId);
        KeyValue GetKeyValue(int keyValueId);

        KeyValue AddKeyValue(KeyValue keyValue);
        KeyValue UpdateKeyValue(KeyValue keyValue);
        KeyValue DeleteKeyValue(KeyValue keyValue);
        KeyType AddKeyType(KeyType keyType);
        KeyType UpdateKeyType(KeyType keyType);
        KeyType DeleteKeyType(KeyType keyType);
    }
}
