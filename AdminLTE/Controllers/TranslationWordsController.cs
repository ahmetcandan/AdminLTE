using AdminLTE.Model;
using AdminLTE.Models;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace AdminLTE.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TranslationWordsController : BaseController
    {
        // GET: TranslationWords
        public ActionResult Index()
        {
            return PartialView();
        }

        public ActionResult List(string id)
        {
            id = id.ToLower();
            var translationWords = (from w in UnitOfWork.TranslationManager.GetTranslationWords(id)
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
                ViewBag.TranslationLanguageId = new SelectList(UnitOfWork.TranslationManager.GetTranslationLanguages(), "TranslationLanguageId", "Description");
            else
                ViewBag.TranslationLanguageId = new SelectList(UnitOfWork.TranslationManager.GetTranslationLanguages().Where(c => c.Code == languageCode), "TranslationLanguageId", "Description");
            return PartialView(new TranslationWordView());
        }

        // POST: TranslationWords/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TranslationWordId,TranslationLanguageId,Description,Code,IsDeleted")] TranslationWordView instance)
        {
            ViewBag.TranslationLanguageId = new SelectList(UnitOfWork.TranslationManager.GetTranslationLanguages(), "TranslationLanguageId", "Description", instance.TranslationLanguageId);
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
                UnitOfWork.TranslationManager.AddTranslationWord(translationWord);
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
            TranslationWord translationWord = UnitOfWork.TranslationManager.GetTranslationWord(id.Value);
            ViewBag.TranslationLanguageId = new SelectList(UnitOfWork.TranslationManager.GetTranslationLanguages().Where(c => c.TranslationLanguageId == translationWord.TranslationLanguageId), "TranslationLanguageId", "Description");
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
            ViewBag.TranslationLanguageId = new SelectList(UnitOfWork.TranslationManager.GetTranslationLanguages(), "TranslationLanguageId", "Description", instance.TranslationLanguageId);
            TranslationWord translationWord = UnitOfWork.TranslationManager.GetTranslationWord(instance.TranslationWordId);
            if (ModelState.IsValid)
            {
                translationWord.Code = instance.Code;
                translationWord.TranslationWordId = instance.TranslationWordId;
                translationWord.TranslationLanguageId = instance.TranslationLanguageId;
                translationWord.Description = instance.Description;
                UnitOfWork.TranslationManager.UpdateTranslationWork(translationWord);
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
            TranslationWord translationWord = UnitOfWork.TranslationManager.GetTranslationWord(id.Value);
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
            TranslationWord translationWord = UnitOfWork.TranslationManager.GetTranslationWord(id);
            UnitOfWork.TranslationManager.DeleteTranslationWord(translationWord);
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
                UnitOfWork.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
