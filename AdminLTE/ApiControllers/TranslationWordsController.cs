using AdminLTE.Models;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace AdminLTE.ApiControllers
{
    public class TranslationWordsController : BaseApiController
    {
        // GET: api/TranslationWords/TR
        [HttpGet]
        [ResponseType(typeof(IList<TranslationWordView>))]
        public IHttpActionResult GetTranslationWord(string id)
        {
            id = id.ToLower();
            var result = (from w in UnitOfWork.TranslationManager.GetTranslationWords(id)
                          select new TranslationWordView
                          {
                              Code = w.Code,
                              Description = w.Description,
                              LanguageCode = id
                          }).ToList();
            return Ok(result);
        }
    }
}