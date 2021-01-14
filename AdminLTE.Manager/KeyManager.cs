using AdminLTE.Core;
using AdminLTE.Model;
using AdminLTE.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Principal;

namespace AdminLTE.Manager
{
    public class KeyManager : IKeyManager
    {
        private readonly DbContext Context;
        private readonly IPrincipal User;
        IKeyValueRepository KeyValueRepository;
        IKeyTypeRepository KeyTypeRepository;

        public KeyManager(DbContext context, IPrincipal user)
        {
            Context = context;
            User = user;
            KeyValueRepository = new KeyValueRepository(context, user);
            KeyTypeRepository = new KeyTypeRepository(context, user);
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
            Context.SaveChanges();
            return result;
        }

        public KeyValue UpdateKeyValue(KeyValue keyValue)
        {
            var result = KeyValueRepository.Update(keyValue);
            Context.SaveChanges();
            return result;
        }

        public KeyValue DeleteKeyValue(KeyValue keyValue)
        {
            var result = KeyValueRepository.Remove(keyValue);
            Context.SaveChanges();
            return result;
        }

        public KeyType AddKeyType(KeyType keyType)
        {
            var result = KeyTypeRepository.Add(keyType);
            Context.SaveChanges();
            return result;
        }

        public KeyType UpdateKeyType(KeyType keyType)
        {
            var result = KeyTypeRepository.Update(keyType);
            Context.SaveChanges();
            return result;
        }

        public KeyType DeleteKeyType(KeyType keyType)
        {
            var result = KeyTypeRepository.Remove(keyType);
            Context.SaveChanges();
            return result;
        }
    }
}
