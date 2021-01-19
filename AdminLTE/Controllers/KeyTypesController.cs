using AdminLTE.Core;
using AdminLTE.Model;
using AdminLTE.Models;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace AdminLTE.Controllers
{
    [System.Web.Mvc.Authorize(Roles = "Admin,User")]
    public class KeyTypesController : BaseController
    {
        public KeyTypesController(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }

        public ActionResult Index()
        {
            return PartialView();
        }

        // GET: KeyTypes
        public ActionResult List()
        {
            var result = (from c in UnitOfWork.KeyManager.GetKeyTypes()
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
                UnitOfWork.KeyManager.AddKeyType(keyType);
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
            KeyType keyType = UnitOfWork.KeyManager.GetKeyType(id.Value);
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
                var keyType = UnitOfWork.KeyManager.GetKeyType(instance.KeyTypeId);
                keyType.Code = instance.Code;
                keyType.Description = instance.Description;
                UnitOfWork.KeyManager.UpdateKeyType(keyType);
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
            KeyType keyType = UnitOfWork.KeyManager.GetKeyType(id.Value);
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
            KeyType keyType = UnitOfWork.KeyManager.GetKeyType(id);
            UnitOfWork.KeyManager.DeleteKeyType(keyType);
            return PartialView(new KeyTypeView
            {
                Code = keyType.Code,
                Description = keyType.Description,
                KeyTypeId = keyType.KeyTypeId
            });
        }
    }
}
