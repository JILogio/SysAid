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
    public class VulnerabilidadsController : Controller
    {
        private ValoracionActivosEntities db = new ValoracionActivosEntities();

        // GET: Vulnerabilidads
        public ActionResult Index()
        {
            var vulnerabilidad = db.Vulnerabilidad.Include(v => v.TipoActivo1);
            return View(vulnerabilidad.ToList());
        }

        // GET: Vulnerabilidads/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vulnerabilidad vulnerabilidad = db.Vulnerabilidad.Find(id);
            if (vulnerabilidad == null)
            {
                return HttpNotFound();
            }
            return View(vulnerabilidad);
        }

        // GET: Vulnerabilidads/Create
        public ActionResult Create()
        {
            ViewBag.tipoActivo = new SelectList(db.TipoActivo, "idTipoActivo", "tipo");
            return View();
        }

        // POST: Vulnerabilidads/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idVulnerabilidad,tipoActivo,nombre")] Vulnerabilidad vulnerabilidad)
        {
            if (ModelState.IsValid)
            {
                db.Vulnerabilidad.Add(vulnerabilidad);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.tipoActivo = new SelectList(db.TipoActivo, "idTipoActivo", "tipo", vulnerabilidad.tipoActivo);
            return View(vulnerabilidad);
        }

        // GET: Vulnerabilidads/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vulnerabilidad vulnerabilidad = db.Vulnerabilidad.Find(id);
            if (vulnerabilidad == null)
            {
                return HttpNotFound();
            }
            ViewBag.tipoActivo = new SelectList(db.TipoActivo, "idTipoActivo", "tipo", vulnerabilidad.tipoActivo);
            return View(vulnerabilidad);
        }

        // POST: Vulnerabilidads/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idVulnerabilidad,tipoActivo,nombre")] Vulnerabilidad vulnerabilidad)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vulnerabilidad).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.tipoActivo = new SelectList(db.TipoActivo, "idTipoActivo", "tipo", vulnerabilidad.tipoActivo);
            return View(vulnerabilidad);
        }

        // GET: Vulnerabilidads/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vulnerabilidad vulnerabilidad = db.Vulnerabilidad.Find(id);
            if (vulnerabilidad == null)
            {
                return HttpNotFound();
            }
            return View(vulnerabilidad);
        }

        // POST: Vulnerabilidads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Vulnerabilidad vulnerabilidad = db.Vulnerabilidad.Find(id);
            db.Vulnerabilidad.Remove(vulnerabilidad);
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
