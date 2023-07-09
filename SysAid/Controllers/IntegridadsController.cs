using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SysAid;

namespace SysAid.Controllers
{
    public class IntegridadsController : Controller
    {
        private ValoracionActivosEntities db = new ValoracionActivosEntities();

        // GET: Integridads
        public ActionResult Index()
        {
            return View(db.Integridad.ToList());
        }

        // GET: Integridads/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Integridad integridad = db.Integridad.Find(id);
            if (integridad == null)
            {
                return HttpNotFound();
            }
            return View(integridad);
        }

        // GET: Integridads/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Integridads/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idIntegridad,nivelIntegridad,nivel,descripcion")] Integridad integridad)
        {
            if (ModelState.IsValid)
            {
                db.Integridad.Add(integridad);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(integridad);
        }

        // GET: Integridads/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Integridad integridad = db.Integridad.Find(id);
            if (integridad == null)
            {
                return HttpNotFound();
            }
            return View(integridad);
        }

        // POST: Integridads/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idIntegridad,nivelIntegridad,nivel,descripcion")] Integridad integridad)
        {
            if (ModelState.IsValid)
            {
                db.Entry(integridad).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(integridad);
        }

        // GET: Integridads/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Integridad integridad = db.Integridad.Find(id);
            if (integridad == null)
            {
                return HttpNotFound();
            }
            return View(integridad);
        }

        // POST: Integridads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Integridad integridad = db.Integridad.Find(id);
            db.Integridad.Remove(integridad);
            db.SaveChanges();
            return RedirectToAction("Index");
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
