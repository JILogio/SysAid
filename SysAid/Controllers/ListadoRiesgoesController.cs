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
    public class ListadoRiesgoesController : Controller
    {
        private ValoracionActivosEntities db = new ValoracionActivosEntities();

        // GET: ListadoRiesgoes
        public ActionResult Index()
        {
            var listadoRiesgo = db.ListadoRiesgo.Include(l => l.Amenaza).Include(l => l.Controles).Include(l => l.ListadoActivos).Include(l => l.Vulnerabilidad);
            return View(listadoRiesgo.ToList());
        }

        // GET: ListadoRiesgoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ListadoRiesgo listadoRiesgo = db.ListadoRiesgo.Find(id);
            if (listadoRiesgo == null)
            {
                return HttpNotFound();
            }
            return View(listadoRiesgo);
        }

        // GET: ListadoRiesgoes/Create
        public ActionResult Create()
        {
            ViewBag.idAmenaza = new SelectList(db.Amenaza, "idAmenaza", "nombre");
            ViewBag.idControl = new SelectList(db.Controles, "idControl", "nombre");
            ViewBag.codigoActivo = new SelectList(db.ListadoActivos, "codigoActivo", "codigoActivo");
            ViewBag.idVulnerabilidad = new SelectList(db.Vulnerabilidad, "idVulnerabilidad", "nombre");
            return View();
        }

        // POST: ListadoRiesgoes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idListadoRiesgo,codigoActivo,idVulnerabilidad,idAmenaza,idControl,riesgo")] ListadoRiesgo listadoRiesgo)
        {
            if (ModelState.IsValid)
            {
                db.ListadoRiesgo.Add(listadoRiesgo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idAmenaza = new SelectList(db.Amenaza, "idAmenaza", "nombre", listadoRiesgo.idAmenaza);
            ViewBag.idControl = new SelectList(db.Controles, "idControl", "nombre", listadoRiesgo.idControl);
            ViewBag.codigoActivo = new SelectList(db.ListadoActivos, "codigoActivo", "codigoActivo", listadoRiesgo.codigoActivo);
            ViewBag.idVulnerabilidad = new SelectList(db.Vulnerabilidad, "idVulnerabilidad", "nombre", listadoRiesgo.idVulnerabilidad);
            return View(listadoRiesgo);
        }

        // GET: ListadoRiesgoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ListadoRiesgo listadoRiesgo = db.ListadoRiesgo.Find(id);
            if (listadoRiesgo == null)
            {
                return HttpNotFound();
            }
            ViewBag.idAmenaza = new SelectList(db.Amenaza, "idAmenaza", "origen", listadoRiesgo.idAmenaza);
            ViewBag.idControl = new SelectList(db.Controles, "idControl", "nombre", listadoRiesgo.idControl);
            ViewBag.codigoActivo = new SelectList(db.ListadoActivos, "codigoActivo", "nombreActivo", listadoRiesgo.codigoActivo);
            ViewBag.idVulnerabilidad = new SelectList(db.Vulnerabilidad, "idVulnerabilidad", "nombre", listadoRiesgo.idVulnerabilidad);
            return View(listadoRiesgo);
        }

        // POST: ListadoRiesgoes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idListadoRiesgo,codigoActivo,idVulnerabilidad,idAmenaza,idControl,riesgo")] ListadoRiesgo listadoRiesgo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(listadoRiesgo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idAmenaza = new SelectList(db.Amenaza, "idAmenaza", "origen", listadoRiesgo.idAmenaza);
            ViewBag.idControl = new SelectList(db.Controles, "idControl", "nombre", listadoRiesgo.idControl);
            ViewBag.codigoActivo = new SelectList(db.ListadoActivos, "codigoActivo", "nombreActivo", listadoRiesgo.codigoActivo);
            ViewBag.idVulnerabilidad = new SelectList(db.Vulnerabilidad, "idVulnerabilidad", "nombre", listadoRiesgo.idVulnerabilidad);
            return View(listadoRiesgo);
        }

        // GET: ListadoRiesgoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ListadoRiesgo listadoRiesgo = db.ListadoRiesgo.Find(id);
            if (listadoRiesgo == null)
            {
                return HttpNotFound();
            }
            return View(listadoRiesgo);
        }

        // POST: ListadoRiesgoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ListadoRiesgo listadoRiesgo = db.ListadoRiesgo.Find(id);
            db.ListadoRiesgo.Remove(listadoRiesgo);
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
