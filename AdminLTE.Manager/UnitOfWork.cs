using AdminLTE.Core;
using System.Data.Entity;
using System.Security.Principal;

namespace AdminLTE.Manager
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext Context;
        private IPrincipal User;

        public UnitOfWork(DbContext context, IPrincipal user)
        {
            Context = context;
            User = user;
            TranslationManager = new TranslationManager(Context, User);
            KeyManager = new KeyManager(Context, User);
            CredentialManager = new CredentialManager(Context, User);
        }

        public ITranslationManager TranslationManager { get; private set; }
        public IKeyManager KeyManager { get; private set; }
        public ICredentialManager CredentialManager { get; private set; }

        public int Complate()
        {
            return Context.SaveChanges();
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
