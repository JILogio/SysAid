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
    public class ListadoActivosController : Controller
    {
        private ValoracionActivosEntities db = new ValoracionActivosEntities();

        // GET: ListadoActivos
        public ActionResult Index()
        {
            var listadoActivos = db.ListadoActivos.Include(l => l.AreaActivo).Include(l => l.Confidencialidad1).Include(l => l.Disponibilidad1).Include(l => l.Integridad1).Include(l => l.TipoActivo1);
            return View(listadoActivos.ToList());
        }

        // GET: ListadoActivos/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ListadoActivos listadoActivos = db.ListadoActivos.Find(id);
            if (listadoActivos == null)
            {
                return HttpNotFound();
            }
            return View(listadoActivos);
        }

        // GET: ListadoActivos/Create
        public ActionResult Create()
        {
            ViewBag.area = new SelectList(db.AreaActivo, "idAreaActivo", "area");
            ViewBag.confidencialidad = new SelectList(db.Confidencialidad, "idConfidencialidad", "nivel");
            ViewBag.disponibilidad = new SelectList(db.Disponibilidad, "idDisponibilidad", "nivel");
            ViewBag.integridad = new SelectList(db.Integridad, "idIntegridad", "nivel");
            ViewBag.tipoActivo = new SelectList(db.TipoActivo, "idTipoActivo", "tipo");
            return View();
        }

        // POST: ListadoActivos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "codigoActivo,nombreActivo,descripcionActivo,propietario,protector,tipoActivo,area,integridad,disponibilidad,confidencialidad,justificacion,valoracion")] ListadoActivos listadoActivos)
        {
            if (ModelState.IsValid)
            {
                db.ListadoActivos.Add(listadoActivos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.area = new SelectList(db.AreaActivo, "idAreaActivo", "area", listadoActivos.area);
            ViewBag.confidencialidad = new SelectList(db.Confidencialidad, "idConfidencialidad", "nivel", listadoActivos.confidencialidad);
            ViewBag.disponibilidad = new SelectList(db.Disponibilidad, "idDisponibilidad", "nivel", listadoActivos.disponibilidad);
            ViewBag.integridad = new SelectList(db.Integridad, "idIntegridad", "nivel", listadoActivos.integridad);
            ViewBag.tipoActivo = new SelectList(db.TipoActivo, "idTipoActivo", "tipo", listadoActivos.tipoActivo);
            return View(listadoActivos);
        }

        // GET: ListadoActivos/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ListadoActivos listadoActivos = db.ListadoActivos.Find(id);
            if (listadoActivos == null)
            {
                return HttpNotFound();
            }
            ViewBag.area = new SelectList(db.AreaActivo, "idAreaActivo", "area", listadoActivos.area);
            ViewBag.confidencialidad = new SelectList(db.Confidencialidad, "idConfidencialidad", "nivel", listadoActivos.confidencialidad);
            ViewBag.disponibilidad = new SelectList(db.Disponibilidad, "idDisponibilidad", "nivel", listadoActivos.disponibilidad);
            ViewBag.integridad = new SelectList(db.Integridad, "idIntegridad", "nivel", listadoActivos.integridad);
            ViewBag.tipoActivo = new SelectList(db.TipoActivo, "idTipoActivo", "tipo", listadoActivos.tipoActivo);
            return View(listadoActivos);
        }

        // POST: ListadoActivos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "codigoActivo,nombreActivo,descripcionActivo,propietario,protector,tipoActivo,area,integridad,disponibilidad,confidencialidad,justificacion,valoracion")] ListadoActivos listadoActivos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(listadoActivos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.area = new SelectList(db.AreaActivo, "idAreaActivo", "area", listadoActivos.area);
            ViewBag.confidencialidad = new SelectList(db.Confidencialidad, "idConfidencialidad", "nivel", listadoActivos.confidencialidad);
            ViewBag.disponibilidad = new SelectList(db.Disponibilidad, "idDisponibilidad", "nivel", listadoActivos.disponibilidad);
            ViewBag.integridad = new SelectList(db.Integridad, "idIntegridad", "nivel", listadoActivos.integridad);
            ViewBag.tipoActivo = new SelectList(db.TipoActivo, "idTipoActivo", "tipo", listadoActivos.tipoActivo);
            return View(listadoActivos);
        }

        // GET: ListadoActivos/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ListadoActivos listadoActivos = db.ListadoActivos.Find(id);
            if (listadoActivos == null)
            {
                return HttpNotFound();
            }
            return View(listadoActivos);
        }

        // POST: ListadoActivos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ListadoActivos listadoActivos = db.ListadoActivos.Find(id);
            db.ListadoActivos.Remove(listadoActivos);
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
