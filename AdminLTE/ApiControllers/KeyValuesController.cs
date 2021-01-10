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
using AdminLTE.Model;
using AdminLTE.Models;

namespace AdminLTE.ApiControllers
{
    public class KeyValuesController : ApiController
    {
        private Models.DbModelContext db = new Models.DbModelContext();

        // GET: api/KeyValues
        public IQueryable<KeyValue> GetKeyValues()
        {
            return db.KeyValues;
        }

        // GET: api/KeyValues/5
        [ResponseType(typeof(KeyValue))]
        public IHttpActionResult GetKeyValue(int id)
        {
            KeyValue keyValue = db.KeyValues.Find(id);
            if (keyValue == null)
            {
                return NotFound();
            }

            return Ok(keyValue);
        }

        // PUT: api/KeyValues/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutKeyValue(int id, KeyValue keyValue)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != keyValue.KeyValueId)
            {
                return BadRequest();
            }

            db.Entry(keyValue).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KeyValueExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/KeyValues
        [ResponseType(typeof(KeyValue))]
        public IHttpActionResult PostKeyValue(KeyValue keyValue)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.KeyValues.Add(keyValue);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = keyValue.KeyValueId }, keyValue);
        }

        // DELETE: api/KeyValues/5
        [ResponseType(typeof(KeyValue))]
        public IHttpActionResult DeleteKeyValue(int id)
        {
            KeyValue keyValue = db.KeyValues.Find(id);
            if (keyValue == null)
            {
                return NotFound();
            }

            db.KeyValues.Remove(keyValue);
            db.SaveChanges();

            return Ok(keyValue);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool KeyValueExists(int id)
        {
            return db.KeyValues.Count(e => e.KeyValueId == id) > 0;
        }
    }
}