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
    public class TranslateLanguageRepository : Repository<TranslationLanguage>, ITranslationLanguageRepository
    {
        public TranslateLanguageRepository(DbContext context) : base(context)
        {
        }
    }
}
