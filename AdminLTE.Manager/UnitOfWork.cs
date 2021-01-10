using AdminLTE.Core;
using AdminLTE.Interface;
using AdminLTE.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminLTE.Manager
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;

        public UnitOfWork(DbContext context)
        {
            _context = context;
            TranslationLanguages = new TranslateLanguageRepository(_context);
            KeyValues = new KeyValueRepository(_context);
            KeyTypes = new KeyTypeRepository(_context);
        }

        public ITranslationLanguageRepository TranslationLanguages { get; private set; }
        public IKeyValueRepository KeyValues { get; private set; }
        public IKeyTypeRepository KeyTypes { get; private set; }

        public int Complate()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
