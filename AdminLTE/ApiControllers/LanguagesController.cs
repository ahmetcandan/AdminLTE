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
    public class LanguagesController : BaseApiController
    {

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
