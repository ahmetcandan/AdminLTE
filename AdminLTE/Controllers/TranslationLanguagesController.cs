using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AdminLTE.Models;

namespace AdminLTE.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TranslationLanguagesController : Controller
    {
        private Models.DbModelContext db = new Models.DbModelContext();

        // GET: TranslationLanguages
        public ActionResult Index()
        {
            return PartialView();
        }

        public ActionResult List()
        {
            var result = (from c in db.TranslationLanguages
                          select new TranslationLanguageView
                          {
                              Code = c.Code,
                              Description = c.Description,
                              TranslationLanguageId = c.TranslationLanguageId
                          }).ToList();
            return PartialView(result);
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
                    Code = instance.Code,
                    Description = instance.Description,
                    IsDeleted = false,
                    TranslationLanguageId = instance.TranslationLanguageId
                };
                db.TranslationLanguages.Add(translationLanguage);
                db.SaveChanges();
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
            TranslationLanguage translationLanguage = db.TranslationLanguages.Find(id);
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
            TranslationLanguage translationLanguage = db.TranslationLanguages.Find(instance.TranslationLanguageId);
            if (ModelState.IsValid)
            {
                translationLanguage.TranslationLanguageId = instance.TranslationLanguageId;
                translationLanguage.Description = instance.Description;
                translationLanguage.Code = instance.Code;
                db.Entry(translationLanguage).State = EntityState.Modified;
                db.SaveChanges();
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
            TranslationLanguage translationLanguage = db.TranslationLanguages.Find(id);
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
            TranslationLanguage translationLanguage = db.TranslationLanguages.Find(id);
            db.TranslationLanguages.Remove(translationLanguage);
            db.SaveChanges();
            return PartialView(new TranslationLanguageView
            {
                Code = translationLanguage.Code,
                Description = translationLanguage.Description,
                TranslationLanguageId = translationLanguage.TranslationLanguageId
            });
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
