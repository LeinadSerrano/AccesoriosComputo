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
using AccesoriosComputo.Models;

namespace AccesoriosComputo.Controllers
{
    public class AccesoriosController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Accesorios
        public IQueryable<Accesorio> GetAccesorios()
        {
            return db.Accesorios;
        }

        // GET: api/Accesorios/5
        [ResponseType(typeof(Accesorio))]
        public IHttpActionResult GetAccesorio(int id)
        {
            Accesorio accesorio = db.Accesorios.Find(id);
            if (accesorio == null)
            {
                return NotFound();
            }

            return Ok(accesorio);
        }

        // PUT: api/Accesorios/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAccesorio(int id, Accesorio accesorio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != accesorio.id)
            {
                return BadRequest();
            }

            db.Entry(accesorio).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccesorioExists(id))
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

        // POST: api/Accesorios
        [ResponseType(typeof(Accesorio))]
        public IHttpActionResult PostAccesorio(Accesorio accesorio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Accesorios.Add(accesorio);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = accesorio.id }, accesorio);
        }

        // DELETE: api/Accesorios/5
        [ResponseType(typeof(Accesorio))]
        public IHttpActionResult DeleteAccesorio(int id)
        {
            Accesorio accesorio = db.Accesorios.Find(id);
            if (accesorio == null)
            {
                return NotFound();
            }

            db.Accesorios.Remove(accesorio);
            db.SaveChanges();

            return Ok(accesorio);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AccesorioExists(int id)
        {
            return db.Accesorios.Count(e => e.id == id) > 0;
        }
    }
}