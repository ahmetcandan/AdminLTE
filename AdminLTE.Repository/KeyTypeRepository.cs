using AdminLTE.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdminLTE.Core;

namespace AdminLTE.Repository
{
    public class KeyTypeRepository : Repository<KeyType>, IKeyTypeRepository
    {
        public KeyTypeRepository(DbContext context) : base(context)
        {
        }
    }
}
