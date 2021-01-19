using AdminLTE.Core;
using AdminLTE.Models;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace AdminLTE.ApiControllers
{
    public class LanguagesController : BaseApiController
    {
        public LanguagesController(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }

        // GET: api/Languages
        [HttpGet]
        [ResponseType(typeof(IList<TranslationLanguageView>))]
        public IHttpActionResult Languages()
        {
            var result = (from l in UnitOfWork.TranslationManager.GetTranslationLanguages()
                          select new TranslationLanguageView
                          {
                              Code = l.Code,
                              Description = l.Description,
                              TranslationLanguageId = l.TranslationLanguageId
                          }).ToList();
            return Ok(result);
        }
    }
}
