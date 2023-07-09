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
    public class AmenazasController : Controller
    {
        private ValoracionActivosEntities db = new ValoracionActivosEntities();

        // GET: Amenazas
        public ActionResult Index()
        {
            return View(db.Amenaza.ToList());
        }

        // GET: Amenazas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Amenaza amenaza = db.Amenaza.Find(id);
            if (amenaza == null)
            {
                return HttpNotFound();
            }
            return View(amenaza);
        }

        // GET: Amenazas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Amenazas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idAmenaza,origen,nombre,probabilidad,impacto")] Amenaza amenaza)
        {
            if (ModelState.IsValid)
            {
                db.Amenaza.Add(amenaza);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(amenaza);
        }

        // GET: Amenazas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Amenaza amenaza = db.Amenaza.Find(id);
            if (amenaza == null)
            {
                return HttpNotFound();
            }
            return View(amenaza);
        }

        // POST: Amenazas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idAmenaza,origen,nombre,probabilidad,impacto")] Amenaza amenaza)
        {
            if (ModelState.IsValid)
            {
                db.Entry(amenaza).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(amenaza);
        }

        // GET: Amenazas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Amenaza amenaza = db.Amenaza.Find(id);
            if (amenaza == null)
            {
                return HttpNotFound();
            }
            return View(amenaza);
        }

        // POST: Amenazas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Amenaza amenaza = db.Amenaza.Find(id);
            db.Amenaza.Remove(amenaza);
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
