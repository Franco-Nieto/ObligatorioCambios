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
    public class ComentarioProgramasController : Controller
    {
        private VozDelEsteContext db = new VozDelEsteContext();

        // GET: ComentarioProgramas
        public ActionResult Index()
        {
            var comentarioPrograma = db.ComentarioPrograma.Include(c => c.ProgramaRadio).Include(c => c.Usuario);
            return View(comentarioPrograma.ToList());
        }

        // GET: ComentarioProgramas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ComentarioPrograma comentarioPrograma = db.ComentarioPrograma.Find(id);
            if (comentarioPrograma == null)
            {
                return HttpNotFound();
            }
            return View(comentarioPrograma);
        }

        // GET: ComentarioProgramas/Create
        public ActionResult Create()
        {
            ViewBag.ProgramaID = new SelectList(db.ProgramaRadio, "Id", "Nombre");
            ViewBag.UsuarioID = new SelectList(db.Usuario, "Id", "NombreUsuario");
            return View();
        }

        // POST: ComentarioProgramas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UsuarioID,ProgramaID,Comentario")] ComentarioPrograma comentarioPrograma)
        {
            if (ModelState.IsValid)
            {
                db.ComentarioPrograma.Add(comentarioPrograma);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProgramaID = new SelectList(db.ProgramaRadio, "Id", "Nombre", comentarioPrograma.ProgramaID);
            ViewBag.UsuarioID = new SelectList(db.Usuario, "Id", "NombreUsuario", comentarioPrograma.UsuarioID);
            return View(comentarioPrograma);
        }

        // GET: ComentarioProgramas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ComentarioPrograma comentarioPrograma = db.ComentarioPrograma.Find(id);
            if (comentarioPrograma == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProgramaID = new SelectList(db.ProgramaRadio, "Id", "Nombre", comentarioPrograma.ProgramaID);
            ViewBag.UsuarioID = new SelectList(db.Usuario, "Id", "NombreUsuario", comentarioPrograma.UsuarioID);
            return View(comentarioPrograma);
        }

        // POST: ComentarioProgramas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UsuarioID,ProgramaID,Comentario")] ComentarioPrograma comentarioPrograma)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comentarioPrograma).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProgramaID = new SelectList(db.ProgramaRadio, "Id", "Nombre", comentarioPrograma.ProgramaID);
            ViewBag.UsuarioID = new SelectList(db.Usuario, "Id", "NombreUsuario", comentarioPrograma.UsuarioID);
            return View(comentarioPrograma);
        }

        // GET: ComentarioProgramas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ComentarioPrograma comentarioPrograma = db.ComentarioPrograma.Find(id);
            if (comentarioPrograma == null)
            {
                return HttpNotFound();
            }
            return View(comentarioPrograma);
        }

        // POST: ComentarioProgramas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ComentarioPrograma comentarioPrograma = db.ComentarioPrograma.Find(id);
            db.ComentarioPrograma.Remove(comentarioPrograma);
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
