using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminLTE.Core
{
    public interface IUnitOfWork : IDisposable
    {
        ITranslationManager TranslationManager { get; }
        IKeyManager KeyManager { get; }

        int Complate();
    }
}
