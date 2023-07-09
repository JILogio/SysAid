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
    public class ConfidencialidadsController : Controller
    {
        private ValoracionActivosEntities db = new ValoracionActivosEntities();

        // GET: Confidencialidads
        public ActionResult Index()
        {
            return View(db.Confidencialidad.ToList());
        }

        // GET: Confidencialidads/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Confidencialidad confidencialidad = db.Confidencialidad.Find(id);
            if (confidencialidad == null)
            {
                return HttpNotFound();
            }
            return View(confidencialidad);
        }

        // GET: Confidencialidads/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Confidencialidads/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idConfidencialidad,nivelConfidencialidad,nivel,descripcion")] Confidencialidad confidencialidad)
        {
            if (ModelState.IsValid)
            {
                db.Confidencialidad.Add(confidencialidad);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(confidencialidad);
        }

        // GET: Confidencialidads/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Confidencialidad confidencialidad = db.Confidencialidad.Find(id);
            if (confidencialidad == null)
            {
                return HttpNotFound();
            }
            return View(confidencialidad);
        }

        // POST: Confidencialidads/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idConfidencialidad,nivelConfidencialidad,nivel,descripcion")] Confidencialidad confidencialidad)
        {
            if (ModelState.IsValid)
            {
                db.Entry(confidencialidad).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(confidencialidad);
        }

        // GET: Confidencialidads/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Confidencialidad confidencialidad = db.Confidencialidad.Find(id);
            if (confidencialidad == null)
            {
                return HttpNotFound();
            }
            return View(confidencialidad);
        }

        // POST: Confidencialidads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Confidencialidad confidencialidad = db.Confidencialidad.Find(id);
            db.Confidencialidad.Remove(confidencialidad);
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
