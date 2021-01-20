using AdminLTE.Core;
using AdminLTE.Model;
using AdminLTE.Models;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Http.Description;
using System.Web.Mvc;

namespace AdminLTE.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TranslationLanguagesController : BaseController
    {
        public TranslationLanguagesController(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        // GET: TranslationLanguages
        public ActionResult Index()
        {
            return PartialView();
        }

        public ActionResult List()
        {
            var result = (from c in UnitOfWork.TranslationManager.GetTranslationLanguages()
                          select new TranslationLanguageView
                          {
                              Code = c.Code,
                              Description = c.Description,
                              TranslationLanguageId = c.TranslationLanguageId
                          }).ToList();
            return PartialView(result);
        }

        [AllowAnonymous]
        [HttpGet]
        [ResponseType(typeof(IList<TranslationLanguageView>))]
        public JsonResult Languages()
        {
            var result = (from l in UnitOfWork.TranslationManager.GetTranslationLanguages()
                          select new TranslationLanguageView
                          {
                              Code = l.Code,
                              Description = l.Description,
                              TranslationLanguageId = l.TranslationLanguageId
                          }).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // GET: TranslationLanguages/Create
        public ActionResult Create()
        {
            return PartialView(new TranslationLanguageView());
        }

        // POST: TranslationLanguages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TranslationLanguageId,Description,Code,IsDeleted")] TranslationLanguageView instance)
        {
            TranslationLanguage translationLanguage = null;
            if (ModelState.IsValid)
            {
                translationLanguage = new TranslationLanguage
                {
                    Code = instance.Code.ToUpper(),
                    Description = instance.Description,
                    IsDeleted = false,
                    TranslationLanguageId = instance.TranslationLanguageId
                };
                UnitOfWork.TranslationManager.AddTranslationLanguage(translationLanguage);
                return PartialView(instance);
            }

            return PartialView(instance);
        }

        // GET: TranslationLanguages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TranslationLanguage translationLanguage = UnitOfWork.TranslationManager.GetTranslationLanguage(id.Value);
            if (translationLanguage == null)
            {
                return HttpNotFound();
            }
            return PartialView(new TranslationLanguageView
            {
                TranslationWords = (from c in translationLanguage.TranslationWords
                                    select new TranslationWordView
                                    {
                                        Code = c.Code,
                                        Description = c.Description,
                                        TranslationLanguageId = c.TranslationLanguageId,
                                        TranslationWordId = c.TranslationWordId
                                    }).ToList(),
                TranslationLanguageId = translationLanguage.TranslationLanguageId,
                Description = translationLanguage.Description,
                Code = translationLanguage.Code
            });
        }

        // POST: TranslationLanguages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TranslationLanguageId,Description,Code,IsDeleted")] TranslationLanguageView instance)
        {
            TranslationLanguage translationLanguage = UnitOfWork.TranslationManager.GetTranslationLanguage(instance.TranslationLanguageId);
            if (ModelState.IsValid)
            {
                translationLanguage.TranslationLanguageId = instance.TranslationLanguageId;
                translationLanguage.Description = instance.Description;
                translationLanguage.Code = instance.Code.ToUpper();
                UnitOfWork.TranslationManager.UpdateTranslationLanguage(translationLanguage);
                return PartialView(instance);
            }
            return PartialView(instance);
        }

        // GET: TranslationLanguages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TranslationLanguage translationLanguage = UnitOfWork.TranslationManager.GetTranslationLanguage(id.Value);
            if (translationLanguage == null)
            {
                return HttpNotFound();
            }
            return PartialView(new TranslationLanguageView
            {
                Code = translationLanguage.Code,
                Description = translationLanguage.Description,
                TranslationLanguageId = translationLanguage.TranslationLanguageId
            });
        }

        // POST: TranslationLanguages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TranslationLanguage translationLanguage = UnitOfWork.TranslationManager.GetTranslationLanguage(id);
            UnitOfWork.TranslationManager.DeleteTranslationLanguage(translationLanguage);
            return PartialView(new TranslationLanguageView
            {
                Code = translationLanguage.Code,
                Description = translationLanguage.Description,
                TranslationLanguageId = translationLanguage.TranslationLanguageId
            });
        }
    }
}
