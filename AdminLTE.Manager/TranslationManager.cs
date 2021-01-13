using AdminLTE.Core;
using AdminLTE.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdminLTE.Repository;

namespace AdminLTE.Manager
{
    public class TranslationManager : ITranslationManager
    {
        private readonly DbContext context;
        ITranslationLanguageRepository TranslationLanguageRepository;
        ITranslationWordRepository TranslationWordRepository;

        public TranslationManager(DbContext context)
        {
            this.context = context;
            TranslationLanguageRepository = new TranslationLanguageRepository(context);
            TranslationWordRepository = new TranslationWordRepository(context);
        }

        public IEnumerable<TranslationLanguage> GetTranslationLanguages()
        {
            return TranslationLanguageRepository.GetAll().ToList();
        }

        public IEnumerable<TranslationWord> GetTranslationWords(string languageCode)
        {
            return TranslationWordRepository.GetTranslationWordsForLanguageCode(languageCode);
        }

        public TranslationWord AddTranslationWord(TranslationWord translationWord)
        {
            return TranslationWordRepository.Add(translationWord);
        }

        public TranslationWord UpdateTranslationWork(TranslationWord translationWord)
        {
            return TranslationWordRepository.Update(translationWord);
        }

        public TranslationWord DeleteTranslationWord(TranslationWord translationWord)
        {
            return TranslationWordRepository.Remove(translationWord);
        }

        public TranslationLanguage AddTranslationLanguage(TranslationLanguage translationLanguage)
        {
            return TranslationLanguageRepository.Add(translationLanguage);
        }

        public TranslationLanguage UpdateTranslationLanguage(TranslationLanguage translationLanguage)
        {
            return TranslationLanguageRepository.Update(translationLanguage);
        }

        public TranslationLanguage DeleteTranslationLanguage(TranslationLanguage translationLanguage)
        {
            return TranslationLanguageRepository.Remove(translationLanguage);
        }

        public TranslationWord GetTranslationWord(int id)
        {
            return TranslationWordRepository.Get(id);
        }

        public TranslationLanguage GetTranslationLanguage(int id)
        {
            return TranslationLanguageRepository.Get(id);
        }

        public TranslationLanguage GetTranslationLanguage(string languageCode)
        {
            return TranslationLanguageRepository.Find(c => c.Code == languageCode).SingleOrDefault();
        }
    }
}
