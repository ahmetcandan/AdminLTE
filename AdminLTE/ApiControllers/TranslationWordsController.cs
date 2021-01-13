using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using AdminLTE.DataAccess;
using AdminLTE.Models;

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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                UnitOfWork.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}