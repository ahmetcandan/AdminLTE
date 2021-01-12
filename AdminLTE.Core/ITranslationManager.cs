using AdminLTE.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdminLTE.Model;

namespace AdminLTE.Core
{
    public interface ITranslationManager : IManager
    {
        IEnumerable<TranslationWord> GetTranslationWords(string languageCode);
        IEnumerable<TranslationLanguage> GetTranslationLanguages();


        TranslationWord AddTranslationWord(TranslationWord translationWord);
        TranslationWord UpdateTranslationWork(TranslationWord translationWord);
        TranslationWord DeleteTranslationWord(TranslationWord translationWord);
        TranslationLanguage AddTranslationLanguage(TranslationLanguage translationLanguage);
        TranslationLanguage UpdateTranslationLanguage(TranslationLanguage translationLanguage);
        TranslationLanguage DeleteTranslationLanguage(TranslationLanguage translationLanguage);
    }
}
