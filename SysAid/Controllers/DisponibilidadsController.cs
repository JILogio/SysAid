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
    public class DisponibilidadsController : Controller
    {
        private ValoracionActivosEntities db = new ValoracionActivosEntities();

        // GET: Disponibilidads
        public ActionResult Index()
        {
            return View(db.Disponibilidad.ToList());
        }

        // GET: Disponibilidads/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Disponibilidad disponibilidad = db.Disponibilidad.Find(id);
            if (disponibilidad == null)
            {
                return HttpNotFound();
            }
            return View(disponibilidad);
        }

        // GET: Disponibilidads/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Disponibilidads/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idDisponibilidad,nivelDisponibilidad,nivel,descripcion")] Disponibilidad disponibilidad)
        {
            if (ModelState.IsValid)
            {
                db.Disponibilidad.Add(disponibilidad);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(disponibilidad);
        }

        // GET: Disponibilidads/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Disponibilidad disponibilidad = db.Disponibilidad.Find(id);
            if (disponibilidad == null)
            {
                return HttpNotFound();
            }
            return View(disponibilidad);
        }

        // POST: Disponibilidads/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idDisponibilidad,nivelDisponibilidad,nivel,descripcion")] Disponibilidad disponibilidad)
        {
            if (ModelState.IsValid)
            {
                db.Entry(disponibilidad).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(disponibilidad);
        }

        // GET: Disponibilidads/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Disponibilidad disponibilidad = db.Disponibilidad.Find(id);
            if (disponibilidad == null)
            {
                return HttpNotFound();
            }
            return View(disponibilidad);
        }

        // POST: Disponibilidads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Disponibilidad disponibilidad = db.Disponibilidad.Find(id);
            db.Disponibilidad.Remove(disponibilidad);
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
