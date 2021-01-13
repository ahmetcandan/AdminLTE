using AdminLTE.Interface;
using AdminLTE.Model;
using System.Collections.Generic;

namespace AdminLTE.Core
{
    public interface ITranslationWordRepository : IRepository<TranslationWord>
    {
        IEnumerable<TranslationWord> GetTranslationWordsForLanguageCode(string languageCode);
    }
}
