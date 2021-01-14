using AdminLTE.Core;
using System.Data.Entity;

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
