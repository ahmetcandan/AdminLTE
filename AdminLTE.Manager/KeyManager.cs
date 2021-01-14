using AdminLTE.Core;
using AdminLTE.Model;
using AdminLTE.Repository;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

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
            var result = KeyValueRepository.Add(keyValue);
            context.SaveChanges();
            return result;
        }

        public KeyValue UpdateKeyValue(KeyValue keyValue)
        {
            var result = KeyValueRepository.Update(keyValue);
            context.SaveChanges();
            return result;
        }

        public KeyValue DeleteKeyValue(KeyValue keyValue)
        {
            var result = KeyValueRepository.Remove(keyValue);
            context.SaveChanges();
            return result;
        }

        public KeyType AddKeyType(KeyType keyType)
        {
            var result = KeyTypeRepository.Add(keyType);
            context.SaveChanges();
            return result;
        }

        public KeyType UpdateKeyType(KeyType keyType)
        {
            var result = KeyTypeRepository.Update(keyType);
            context.SaveChanges();
            return result;
        }

        public KeyType DeleteKeyType(KeyType keyType)
        {
            var result = KeyTypeRepository.Remove(keyType);
            context.SaveChanges();
            return result;
        }
    }
}
