﻿using System;
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
    public class PatrocinadorsController : Controller
    {
        private VozDelEsteContext db = new VozDelEsteContext();

        // GET: Patrocinadors
        public ActionResult Index()
        {
            return View(db.Patrocinador.ToList());
        }

        // GET: Patrocinadors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patrocinador patrocinador = db.Patrocinador.Find(id);
            if (patrocinador == null)
            {
                return HttpNotFound();
            }
            return View(patrocinador);
        }

        // GET: Patrocinadors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Patrocinadors/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nombre,Descripcion,TransmisionDiaria,UrlImagen")] Patrocinador patrocinador)
        {
            if (ModelState.IsValid)
            {
                db.Patrocinador.Add(patrocinador);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(patrocinador);
        }

        // GET: Patrocinadors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patrocinador patrocinador = db.Patrocinador.Find(id);
            if (patrocinador == null)
            {
                return HttpNotFound();
            }
            return View(patrocinador);
        }

        // POST: Patrocinadors/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombre,Descripcion,TransmisionDiaria,UrlImagen")] Patrocinador patrocinador)
        {
            if (ModelState.IsValid)
            {
                db.Entry(patrocinador).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(patrocinador);
        }

        // GET: Patrocinadors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patrocinador patrocinador = db.Patrocinador.Find(id);
            if (patrocinador == null)
            {
                return HttpNotFound();
            }
            return View(patrocinador);
        }

        // POST: Patrocinadors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Patrocinador patrocinador = db.Patrocinador.Find(id);
            db.Patrocinador.Remove(patrocinador);
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
