using AdminLTE.Core;
using System.Data.Entity;
using System.Security.Principal;

namespace AdminLTE.Manager
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext Context;
        private IPrincipal User;
        private ITranslationManager _translationManager;
        private IKeyManager _keyManager;
        private ICredentialManager _credentialManager;

        public ITranslationManager TranslationManager
        {
            get
            {
                if (_translationManager == null)
                    _translationManager = new TranslationManager(Context, User);
                return _translationManager;
            }
        }

        public IKeyManager KeyManager
        {
            get
            {
                if (_keyManager == null)
                    _keyManager = new KeyManager(Context, User);
                return _keyManager;
            }
        }

        public ICredentialManager CredentialManager
        {
            get
            {
                if (_credentialManager == null)
                    _credentialManager = new CredentialManager(Context, User);
                return _credentialManager;
            }
        }

        public UnitOfWork(DbContext context, IPrincipal user)
        {
            Context = context;
            User = user;
        }

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
