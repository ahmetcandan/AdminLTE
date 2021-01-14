using AdminLTE.Core;
using AdminLTE.Model;
using System.Data.Entity;

namespace AdminLTE.Repository
{
    public class TranslationLanguageRepository : Repository<TranslationLanguage>, ITranslationLanguageRepository
    {
        public TranslationLanguageRepository(DbContext context) : base(context)
        {
        }
    }
}
