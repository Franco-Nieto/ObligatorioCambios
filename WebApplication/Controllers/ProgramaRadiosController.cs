using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class ProgramaRadiosController : Controller
    {
        private VozDelEsteContext db = new VozDelEsteContext();

        // GET: ProgramaRadios
        public ActionResult Index()
        {
            return View(db.ProgramaRadio.ToList());
        }

        // GET: ProgramaRadios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProgramaRadio programaRadio = db.ProgramaRadio.Find(id);
            if (programaRadio == null)
            {
                return HttpNotFound();
            }
            return View(programaRadio);
        }

        // GET: ProgramaRadios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProgramaRadios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nombre,ImagenUrl,Descripcion,Duracion")] ProgramaRadio programaRadio)
        {
            if (ModelState.IsValid)
            {
                db.ProgramaRadio.Add(programaRadio);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(programaRadio);
        }

        // GET: ProgramaRadios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProgramaRadio programaRadio = db.ProgramaRadio.Find(id);
            if (programaRadio == null)
            {
                return HttpNotFound();
            }
            return View(programaRadio);
        }

        // POST: ProgramaRadios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombre,ImagenUrl,Descripcion,Duracion")] ProgramaRadio programaRadio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(programaRadio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(programaRadio);
        }

        // GET: ProgramaRadios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProgramaRadio programaRadio = db.ProgramaRadio.Find(id);
            if (programaRadio == null)
            {
                return HttpNotFound();
            }
            return View(programaRadio);
        }

        // POST: ProgramaRadios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProgramaRadio programaRadio = db.ProgramaRadio.Find(id);
            db.ProgramaRadio.Remove(programaRadio);
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
