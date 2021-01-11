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

        public IEnumerable<KeyType> GetKeyTypes()
        {
            return KeyTypeRepository.GetAll().ToList();
        }

        public IEnumerable<KeyValue> GetKeyValues(int keyTypeId)
        {
            return KeyValueRepository.GetKeyValuesForKeyTypeId(keyTypeId).ToList();
        }
    }
}
