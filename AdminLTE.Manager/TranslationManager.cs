﻿using AdminLTE.Core;
using AdminLTE.Model;
using AdminLTE.Repository;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Principal;

namespace AdminLTE.Manager
{
    public class TranslationManager : ITranslationManager
    {
        private readonly DbContext Context;
        private readonly IPrincipal User;
        private ITranslationLanguageRepository _translationLanguageRepository;
        private ITranslationWordRepository _translationWordRepository;

        ITranslationLanguageRepository TranslationLanguageRepository 
        {
            get
            {
                if (_translationLanguageRepository == null)
                    _translationLanguageRepository = new TranslationLanguageRepository(Context, User);
                return _translationLanguageRepository;
            }
        }

        ITranslationWordRepository TranslationWordRepository
        {
            get
            {
                if (_translationWordRepository == null)
                    _translationWordRepository = new TranslationWordRepository(Context, User);
                return _translationWordRepository;
            }
        }

        public TranslationManager(DbContext context, IPrincipal user)
        {
            Context = context;
            User = user;
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
            Context.SaveChanges();
            return result;
        }

        public TranslationWord UpdateTranslationWork(TranslationWord translationWord)
        {
            var result = TranslationWordRepository.Update(translationWord);
            Context.SaveChanges();
            return result;
        }

        public TranslationWord DeleteTranslationWord(TranslationWord translationWord)
        {
            var result = TranslationWordRepository.Remove(translationWord);
            Context.SaveChanges();
            return result;
        }

        public TranslationLanguage AddTranslationLanguage(TranslationLanguage translationLanguage)
        {
            var result = TranslationLanguageRepository.Add(translationLanguage);
            Context.SaveChanges();
            return result;
        }

        public TranslationLanguage UpdateTranslationLanguage(TranslationLanguage translationLanguage)
        {
            var result = TranslationLanguageRepository.Update(translationLanguage);
            Context.SaveChanges();
            return result;
        }

        public TranslationLanguage DeleteTranslationLanguage(TranslationLanguage translationLanguage)
        {
            var result = TranslationLanguageRepository.Remove(translationLanguage);
            Context.SaveChanges();
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
