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
using Microsoft.AspNet.SignalR;

namespace AdminLTE.Controllers
{
    [System.Web.Mvc.Authorize(Roles = "Admin,User")]
    public class KeyTypesController : BaseController
    {
        private DbModelContext db = new DbModelContext();

        public ActionResult Index()
        {
            return PartialView();
        }

        // GET: KeyTypes
        public ActionResult List()
        {
            var result = (from c in UnitOfWork.KeyTypes.GetAll()
                          select new KeyTypeView
                          {
                              Code = c.Code,
                              Description = c.Description,
                              KeyTypeId = c.KeyTypeId
                          }).ToList();
            return PartialView(result);
        }

        // GET: KeyTypes/Create
        public ActionResult Create()
        {
            return PartialView(new KeyTypeView());
        }

        // POST: KeyTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "KeyTypeId,Description,Code,IsDeleted")] KeyTypeView instance)
        {
            if (ModelState.IsValid)
            {
                KeyType keyType = new KeyType();
                keyType.Code = instance.Code;
                keyType.Description = instance.Description;
                keyType.IsDeleted = false;
                db.KeyTypes.Add(keyType);
                db.SaveChanges();
                return PartialView(keyType);
            }

            return PartialView(instance);
        }

        // GET: KeyTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KeyType keyType = db.KeyTypes.Find(id);
            if (keyType == null)
            {
                return HttpNotFound();
            }
            return PartialView(new KeyTypeView
            {
                Code = keyType.Code,
                KeyTypeId = keyType.KeyTypeId,
                Description = keyType.Description,
                KeyValues = (from c in keyType.KeyValues
                             select new KeyValueView
                             {
                                 Description = c.Description,
                                 EndDate = c.EndDate,
                                 IsActive = c.IsActive,
                                 Key = c.Key,
                                 KeyTypeId = c.KeyTypeId,
                                 KeyValueId = c.KeyValueId,
                                 StartDate = c.StartDate,
                                 Value = c.Value
                             }).ToList()
            });
        }

        // POST: KeyTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "KeyTypeId,Description,Code,IsDeleted")] KeyTypeView instance)
        {
            if (ModelState.IsValid)
            {
                var keyType = db.KeyTypes.Find(instance.KeyTypeId);
                keyType.Code = instance.Code;
                keyType.Description = instance.Description;
                db.SaveChanges();
                return PartialView(instance);
            }
            return PartialView(instance);
        }

        // GET: KeyTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KeyType keyType = db.KeyTypes.Find(id);
            if (keyType == null)
            {
                return HttpNotFound();
            }
            return PartialView(new KeyTypeView
            {
                Code = keyType.Code,
                Description = keyType.Description,
                KeyTypeId = keyType.KeyTypeId
            });
        }

        // POST: KeyTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            KeyType keyType = db.KeyTypes.Find(id);
            db.KeyTypes.Remove(keyType);
            db.SaveChanges();
            return PartialView(new KeyTypeView
            {
                Code = keyType.Code,
                Description = keyType.Description,
                KeyTypeId = keyType.KeyTypeId
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
