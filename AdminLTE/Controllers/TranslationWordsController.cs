using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AdminLTE.Model;
using AdminLTE.Models;

namespace AdminLTE.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TranslationWordsController : BaseController
    {
        private Models.DbModelContext db = new Models.DbModelContext();

        // GET: TranslationWords
        public ActionResult Index()
        {
            return PartialView();
        }

        public ActionResult List(string id)
        {
            id = id.ToLower();
            var translationWords = (from l in db.TranslationLanguages
                                    join w in db.TranslationWords on l.TranslationLanguageId equals w.TranslationLanguageId
                                    where l.Code == id
                                    select new TranslationWordView
                                    {
                                        Code = w.Code,
                                        Description = w.Description,
                                        LanguageCode = id,
                                        TranslationWordId = w.TranslationWordId
                                    });
            return PartialView(translationWords.ToList());
        }

        // GET: TranslationWords/Create
        public ActionResult Create(string languageCode)
        {
            if (string.IsNullOrEmpty(languageCode))
                ViewBag.TranslationLanguageId = new SelectList(db.TranslationLanguages, "TranslationLanguageId", "Description");
            else
                ViewBag.TranslationLanguageId = new SelectList(db.TranslationLanguages.Where(c => c.Code == languageCode), "TranslationLanguageId", "Description");
            return PartialView(new TranslationWordView());
        }

        // POST: TranslationWords/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TranslationWordId,TranslationLanguageId,Description,Code,IsDeleted")] TranslationWordView instance)
        {
            ViewBag.TranslationLanguageId = new SelectList(db.TranslationLanguages, "TranslationLanguageId", "Description", instance.TranslationLanguageId);
            TranslationWord translationWord = null;
            if (ModelState.IsValid)
            {
                translationWord = new TranslationWord
                {
                    Code = instance.Code,
                    Description = instance.Description,
                    IsDeleted = false,
                    TranslationLanguageId = instance.TranslationLanguageId
                };
                db.TranslationWords.Add(translationWord);
                db.SaveChanges();
                return PartialView(instance);
            }

            return PartialView(instance);
        }

        // GET: TranslationWords/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TranslationWord translationWord = db.TranslationWords.Find(id);
            ViewBag.TranslationLanguageId = new SelectList(db.TranslationLanguages.Where(c => c.TranslationLanguageId == translationWord.TranslationLanguageId), "TranslationLanguageId", "Description");
            if (translationWord == null)
            {
                return HttpNotFound();
            }
            return PartialView(new TranslationWordView
            {
                Code = translationWord.Code,
                Description = translationWord.Description,
                TranslationLanguageId = translationWord.TranslationLanguageId,
                TranslationWordId = translationWord.TranslationWordId,
                LanguageCode = translationWord.TranslationLanguage.Code
            });
        }

        // POST: TranslationWords/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TranslationWordId,TranslationLanguageId,Description,Code,IsDeleted")] TranslationWordView instance)
        {
            ViewBag.TranslationLanguageId = new SelectList(db.TranslationLanguages, "TranslationLanguageId", "Description", instance.TranslationLanguageId);
            TranslationWord translationWord = db.TranslationWords.Find(instance.TranslationWordId);
            if (ModelState.IsValid)
            {
                translationWord.Code = instance.Code;
                translationWord.TranslationWordId = instance.TranslationWordId;
                translationWord.TranslationLanguageId = instance.TranslationLanguageId;
                translationWord.Description = instance.Description;
                db.Entry(translationWord).State = EntityState.Modified;
                db.SaveChanges();
                return PartialView(instance);
            }
            return PartialView(instance);
        }

        // GET: TranslationWords/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TranslationWord translationWord = db.TranslationWords.Find(id);
            if (translationWord == null)
            {
                return HttpNotFound();
            }
            return PartialView(new TranslationWordView
            {
                Code = translationWord.Code,
                Description = translationWord.Description,
                TranslationLanguageId = translationWord.TranslationLanguageId,
                TranslationWordId = translationWord.TranslationWordId,
                LanguageCode = translationWord.TranslationLanguage.Code
            });
        }

        // POST: TranslationWords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TranslationWord translationWord = db.TranslationWords.Find(id);
            db.TranslationWords.Remove(translationWord);
            db.SaveChanges();
            return PartialView(new TranslationWordView
            {
                Code = translationWord.Code,
                TranslationWordId = translationWord.TranslationWordId,
                Description = translationWord.Description,
                TranslationLanguageId = translationWord.TranslationLanguageId
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
