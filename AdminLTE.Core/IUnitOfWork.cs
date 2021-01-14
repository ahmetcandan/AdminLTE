using System;

namespace AdminLTE.Core
{
    public interface IUnitOfWork : IDisposable
    {
        ITranslationManager TranslationManager { get; }
        IKeyManager KeyManager { get; }

        int Complate();
    }
}
