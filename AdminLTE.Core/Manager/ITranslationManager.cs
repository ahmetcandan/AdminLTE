using AdminLTE.Interface;
using AdminLTE.Model;
using System.Collections.Generic;

namespace AdminLTE.Core
{
    public interface ITranslationManager : IManager
    {
        IEnumerable<TranslationWord> GetTranslationWords(string languageCode);
        IEnumerable<TranslationLanguage> GetTranslationLanguages();

        TranslationWord GetTranslationWord(int id);
        TranslationLanguage GetTranslationLanguage(int id);
        TranslationLanguage GetTranslationLanguage(string languageCode);
        TranslationWord AddTranslationWord(TranslationWord translationWord);
        TranslationWord UpdateTranslationWork(TranslationWord translationWord);
        TranslationWord DeleteTranslationWord(TranslationWord translationWord);
        TranslationLanguage AddTranslationLanguage(TranslationLanguage translationLanguage);
        TranslationLanguage UpdateTranslationLanguage(TranslationLanguage translationLanguage);
        TranslationLanguage DeleteTranslationLanguage(TranslationLanguage translationLanguage);
    }
}
