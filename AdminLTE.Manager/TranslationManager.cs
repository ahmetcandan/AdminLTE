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
    }
}
