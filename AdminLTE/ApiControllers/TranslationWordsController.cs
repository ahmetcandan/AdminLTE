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
using AdminLTE.Models;

namespace AdminLTE.ApiControllers
{
    public class TranslationWordsController : ApiController
    {
        private Models.DbModelContext db = new Models.DbModelContext();

        // GET: api/TranslationWords/TR
        [HttpGet]
        [ResponseType(typeof(IList<TranslationWordView>))]
        public IHttpActionResult GetTranslationWord(string id)
        {
            id = id.ToLower();
            var result = (from l in db.TranslationLanguages
                          join w in db.TranslationWords on l.TranslationLanguageId equals w.TranslationLanguageId
                          where l.Code == id
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
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}