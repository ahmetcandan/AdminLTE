using AdminLTE.Core;
using AdminLTE.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdminLTE.Repository;

namespace AdminLTE.Manager
{
    public class KeyManager : IKeyManager
    {
        private readonly DbContext context;
        IKeyValueRepository KeyValueRepository;
        IKeyTypeRepository KeyTypeRepository;

        public KeyManager(DbContext context)
        {
            this.context = context;
            KeyValueRepository = new KeyValueRepository(context);
            KeyTypeRepository = new KeyTypeRepository(context);
        }

        public KeyType GetKeyType(int keyTypeId)
        {
            return KeyTypeRepository.Get(keyTypeId);
        }

        public KeyValue GetKeyValue(int keyValueId)
        {
            return KeyValueRepository.Get(keyValueId);
        }

        public IEnumerable<KeyType> GetKeyTypes()
        {
            return KeyTypeRepository.GetAll().ToList();
        }

        public IEnumerable<KeyValue> GetKeyValues(int keyTypeId)
        {
            return KeyValueRepository.GetKeyValuesForKeyTypeId(keyTypeId).ToList();
        }

        public KeyValue AddKeyValue(KeyValue keyValue)
        {
            return KeyValueRepository.Add(keyValue);
        }

        public KeyValue UpdateKeyValue(KeyValue keyValue)
        {
            return KeyValueRepository.Update(keyValue);
        }

        public KeyValue DeleteKeyValue(KeyValue keyValue)
        {
            return KeyValueRepository.Remove(keyValue);
        }

        public KeyType AddKeyType(KeyType keyType)
        {
            return KeyTypeRepository.Add(keyType);
        }

        public KeyType UpdateKeyType(KeyType keyType)
        {
            return KeyTypeRepository.Update(keyType);
        }

        public KeyType DeleteKeyType(KeyType keyType)
        {
            return KeyTypeRepository.Remove(keyType);
        }
    }
}
