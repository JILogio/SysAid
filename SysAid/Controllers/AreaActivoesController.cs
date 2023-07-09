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
    public class AreaActivoesController : Controller
    {
        private ValoracionActivosEntities db = new ValoracionActivosEntities();

        // GET: AreaActivoes
        public ActionResult Index()
        {
            return View(db.AreaActivo.ToList());
        }

        // GET: AreaActivoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AreaActivo areaActivo = db.AreaActivo.Find(id);
            if (areaActivo == null)
            {
                return HttpNotFound();
            }
            return View(areaActivo);
        }

        // GET: AreaActivoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AreaActivoes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idAreaActivo,area")] AreaActivo areaActivo)
        {
            if (ModelState.IsValid)
            {
                db.AreaActivo.Add(areaActivo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(areaActivo);
        }

        // GET: AreaActivoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AreaActivo areaActivo = db.AreaActivo.Find(id);
            if (areaActivo == null)
            {
                return HttpNotFound();
            }
            return View(areaActivo);
        }

        // POST: AreaActivoes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idAreaActivo,area")] AreaActivo areaActivo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(areaActivo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(areaActivo);
        }

        // GET: AreaActivoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AreaActivo areaActivo = db.AreaActivo.Find(id);
            if (areaActivo == null)
            {
                return HttpNotFound();
            }
            return View(areaActivo);
        }

        // POST: AreaActivoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AreaActivo areaActivo = db.AreaActivo.Find(id);
            db.AreaActivo.Remove(areaActivo);
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
