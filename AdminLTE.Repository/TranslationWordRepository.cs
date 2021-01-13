using AdminLTE.Core;
using AdminLTE.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminLTE.Repository
{
    public class TranslationWordRepository : Repository<TranslationWord>, ITranslationWordRepository
    {
        public TranslationWordRepository(DbContext context) : base(context)
        {
        }

        public IEnumerable<TranslationWord> GetTranslationWordsForLanguageCode(string languageCode)
        {
            return (from w in Context.Set<TranslationWord>()
                    join l in Context.Set<TranslationLanguage>()
                        on w.TranslationLanguageId equals l.TranslationLanguageId
                    where l.Code == languageCode
                    select w).ToList();
        }
    }
}
