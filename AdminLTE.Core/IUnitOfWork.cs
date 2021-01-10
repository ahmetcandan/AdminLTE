using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminLTE.Core
{
    public interface IUnitOfWork : IDisposable
    {
        ITranslationLanguageRepository TranslationLanguages { get; }
        IKeyValueRepository KeyValues { get; }
        IKeyTypeRepository KeyTypes { get; }

        int Complate();
    }
}
