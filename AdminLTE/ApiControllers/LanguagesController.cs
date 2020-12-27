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
    public class LanguagesController : ApiController
    {
        private Models.DbModelContext db = new Models.DbModelContext();

        // GET: api/Languages
        [HttpGet]
        [ResponseType(typeof(IList<TranslationLanguageView>))]
        public IHttpActionResult Languages()
        {
            var result = (from l in db.TranslationLanguages
                          where l.IsDeleted == false
                          select new TranslationLanguageView
                          {
                              Code = l.Code,
                              Description = l.Description,
                              TranslationLanguageId = l.TranslationLanguageId
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
