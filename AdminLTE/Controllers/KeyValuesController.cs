using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.OleDb;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using AdminLTE.Model;
using AdminLTE.Models;

namespace AdminLTE.Controllers
{
    [Authorize(Roles = "Admin,User")]
    public class KeyValuesController : BaseController
    {
        private DbModelContext db = new DbModelContext();

        // GET: Index
        public ActionResult Index()
        {
            return PartialView();
        }

        // GET: KeyValues
        public ActionResult List(int keyTypeId)
        {
            var keyValues = (from c in UnitOfWork.KeyManager.GetKeyValues(keyTypeId)
                             select new KeyValueView
                             {
                                 Description = c.Description,
                                 KeyTypeId = c.KeyTypeId,
                                 EndDate = c.EndDate,
                                 IsActive = c.IsActive,
                                 Key = c.Key,
                                 KeyValueId = c.KeyValueId,
                                 StartDate = c.StartDate,
                                 Value = c.Value,
                                 KeyType = new KeyTypeView
                                 {
                                     Code = c.KeyType.Code,
                                     Description = c.Description
                                 }
                             });
            return PartialView(keyValues.ToList());
        }

        // GET: KeyValues/Create
        public ActionResult Create(int? keyTypeId)
        {
            if (keyTypeId.HasValue)
                ViewBag.KeyTypeId = new SelectList(db.KeyTypes.Where(c => c.KeyTypeId == keyTypeId.Value), "KeyTypeId", "Description");
            else
                ViewBag.KeyTypeId = new SelectList(db.KeyTypes, "KeyTypeId", "Description");
            return PartialView(new KeyValueView());
        }

        // POST: KeyValues/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "KeyValueId,KeyTypeId,Key,Value,Description,StartDate,EndDate")] KeyValueView instance)
        {
            if (ModelState.IsValid)
            {
                var keyValue = new KeyValue();
                keyValue.CreatedDate = DateTime.Now;
                keyValue.CreatedUser = User.Identity.Name;
                keyValue.Description = instance.Description;
                keyValue.EndDate = instance.EndDate;
                keyValue.IsActive = true;
                keyValue.IsDeleted = false;
                keyValue.Key = instance.Key;
                keyValue.KeyTypeId = instance.KeyTypeId;
                keyValue.StartDate = instance.StartDate;
                keyValue.Value = instance.Value;
                db.KeyValues.Add(keyValue);
                db.SaveChanges();
                return PartialView(instance);
            }

            ViewBag.KeyTypeId = new SelectList(db.KeyTypes, "KeyTypeId", "Description", instance.KeyTypeId);
            return PartialView(instance);
        }

        // GET: KeyValues/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KeyValue keyValue = db.KeyValues.Find(id);
            if (keyValue == null)
            {
                return HttpNotFound();
            }
            ViewBag.KeyTypeId = new SelectList(db.KeyTypes, "KeyTypeId", "Description", keyValue.KeyTypeId);
            return PartialView(new KeyValueView
            {
                Description = keyValue.Description,
                KeyTypeId = keyValue.KeyTypeId,
                EndDate = keyValue.EndDate,
                IsActive = keyValue.IsActive,
                Key = keyValue.Key,
                KeyValueId = keyValue.KeyValueId,
                StartDate = keyValue.StartDate,
                Value = keyValue.Value,
                KeyType = new KeyTypeView
                {
                    Code = keyValue.KeyType.Code,
                    Description = keyValue.Description
                }
            });
        }

        // POST: KeyValues/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "KeyValueId,KeyTypeId,Key,Value,Description,StartDate,EndDate,IsActive")] KeyValueView instance)
        {
            if (ModelState.IsValid)
            {
                var keyValue = db.KeyValues.Find(instance.KeyValueId);
                keyValue.ModifiedDate = DateTime.Now;
                keyValue.ModifiedUser = User.Identity.Name;
                keyValue.Description = instance.Description;
                keyValue.EndDate = instance.EndDate;
                keyValue.IsActive = true;
                keyValue.IsDeleted = false;
                keyValue.Key = instance.Key;
                keyValue.KeyTypeId = instance.KeyTypeId;
                keyValue.StartDate = instance.StartDate;
                keyValue.Value = instance.Value;
                db.SaveChanges();
                return PartialView(keyValue);
            }
            ViewBag.KeyTypeId = new SelectList(db.KeyTypes, "KeyTypeId", "Description", instance.KeyTypeId);
            return PartialView(instance);
        }

        // GET: KeyValues/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KeyValue keyValue = db.KeyValues.Find(id);
            if (keyValue == null)
            {
                return HttpNotFound();
            }
            return PartialView(new KeyValueView
            {
                Description = keyValue.Description,
                KeyTypeId = keyValue.KeyTypeId,
                EndDate = keyValue.EndDate,
                IsActive = keyValue.IsActive,
                Key = keyValue.Key,
                KeyValueId = keyValue.KeyValueId,
                StartDate = keyValue.StartDate,
                Value = keyValue.Value,
                KeyType = new KeyTypeView
                {
                    Code = keyValue.KeyType.Code,
                    Description = keyValue.Description
                }
            });
        }

        // POST: KeyValues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            KeyValue keyValue = db.KeyValues.Find(id);
            db.KeyValues.Remove(keyValue);
            db.SaveChanges();
            return PartialView(new KeyValueView
            {
                Description = keyValue.Description,
                KeyTypeId = keyValue.KeyTypeId,
                EndDate = keyValue.EndDate,
                IsActive = keyValue.IsActive,
                Key = keyValue.Key,
                KeyValueId = keyValue.KeyValueId,
                StartDate = keyValue.StartDate,
                Value = keyValue.Value,
                KeyType = new KeyTypeView
                {
                    Code = keyValue.KeyType.Code,
                    Description = keyValue.Description
                }
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
