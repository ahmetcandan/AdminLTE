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
        private readonly DbContext context;

        public UnitOfWork(DbContext context)
        {
            this.context = context;
            TranslationManager = new TranslationManager(this.context);
            KeyManager = new KeyManager(this.context);
            CredentialManager = new CredentialManager(this.context);
        }

        public ITranslationManager TranslationManager { get; private set; }
        public IKeyManager KeyManager { get; private set; }
        public ICredentialManager CredentialManager { get; private set; }

        public int Complate()
        {
            return context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
