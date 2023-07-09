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
    public class ControlesController : Controller
    {
        private ValoracionActivosEntities db = new ValoracionActivosEntities();

        // GET: Controles
        public ActionResult Index()
        {
            return View(db.Controles.ToList());
        }

        // GET: Controles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Controles controles = db.Controles.Find(id);
            if (controles == null)
            {
                return HttpNotFound();
            }
            return View(controles);
        }

        // GET: Controles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Controles/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idControl,nombre,descripcion")] Controles controles)
        {
            if (ModelState.IsValid)
            {
                db.Controles.Add(controles);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(controles);
        }

        // GET: Controles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Controles controles = db.Controles.Find(id);
            if (controles == null)
            {
                return HttpNotFound();
            }
            return View(controles);
        }

        // POST: Controles/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idControl,nombre,descripcion")] Controles controles)
        {
            if (ModelState.IsValid)
            {
                db.Entry(controles).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(controles);
        }

        // GET: Controles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Controles controles = db.Controles.Find(id);
            if (controles == null)
            {
                return HttpNotFound();
            }
            return View(controles);
        }

        // POST: Controles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Controles controles = db.Controles.Find(id);
            db.Controles.Remove(controles);
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
