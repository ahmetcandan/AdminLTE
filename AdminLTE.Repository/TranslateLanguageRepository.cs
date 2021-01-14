using AdminLTE.Core;
using AdminLTE.Model;
using System.Data.Entity;
using System.Security.Principal;

namespace AdminLTE.Repository
{
    public class TranslationLanguageRepository : Repository<TranslationLanguage>, ITranslationLanguageRepository
    {
        public TranslationLanguageRepository(DbContext context, IPrincipal user) : base(context, user)
        {
        }
    }
}
