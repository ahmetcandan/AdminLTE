using AdminLTE.Core;
using AdminLTE.Model;
using AdminLTE.Repository;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

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
            var result = TranslationWordRepository.Add(translationWord);
            context.SaveChanges();
            return result;
        }

        public TranslationWord UpdateTranslationWork(TranslationWord translationWord)
        {
            var result = TranslationWordRepository.Update(translationWord);
            context.SaveChanges();
            return result;
        }

        public TranslationWord DeleteTranslationWord(TranslationWord translationWord)
        {
            var result = TranslationWordRepository.Remove(translationWord);
            context.SaveChanges();
            return result;
        }

        public TranslationLanguage AddTranslationLanguage(TranslationLanguage translationLanguage)
        {
            var result = TranslationLanguageRepository.Add(translationLanguage);
            context.SaveChanges();
            return result;
        }

        public TranslationLanguage UpdateTranslationLanguage(TranslationLanguage translationLanguage)
        {
            var result = TranslationLanguageRepository.Update(translationLanguage);
            context.SaveChanges();
            return result;
        }

        public TranslationLanguage DeleteTranslationLanguage(TranslationLanguage translationLanguage)
        {
            var result = TranslationLanguageRepository.Remove(translationLanguage);
            context.SaveChanges();
            return result;
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
