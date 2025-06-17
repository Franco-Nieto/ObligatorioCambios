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
    public class CotizacionMonedasController : Controller
    {
        private VozDelEsteContext db = new VozDelEsteContext();

        // GET: CotizacionMonedas
        public ActionResult Index()
        {
            return View(db.CotizacionMoneda.ToList());
        }

        // GET: CotizacionMonedas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CotizacionMoneda cotizacionMoneda = db.CotizacionMoneda.Find(id);
            if (cotizacionMoneda == null)
            {
                return HttpNotFound();
            }
            return View(cotizacionMoneda);
        }

        // GET: CotizacionMonedas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CotizacionMonedas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Fecha,TipoMoneda,ValorCompra,ValorVenta")] CotizacionMoneda cotizacionMoneda)
        {
            if (ModelState.IsValid)
            {
                db.CotizacionMoneda.Add(cotizacionMoneda);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cotizacionMoneda);
        }

        // GET: CotizacionMonedas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CotizacionMoneda cotizacionMoneda = db.CotizacionMoneda.Find(id);
            if (cotizacionMoneda == null)
            {
                return HttpNotFound();
            }
            return View(cotizacionMoneda);
        }

        // POST: CotizacionMonedas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Fecha,TipoMoneda,ValorCompra,ValorVenta")] CotizacionMoneda cotizacionMoneda)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cotizacionMoneda).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cotizacionMoneda);
        }

        // GET: CotizacionMonedas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CotizacionMoneda cotizacionMoneda = db.CotizacionMoneda.Find(id);
            if (cotizacionMoneda == null)
            {
                return HttpNotFound();
            }
            return View(cotizacionMoneda);
        }

        // POST: CotizacionMonedas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CotizacionMoneda cotizacionMoneda = db.CotizacionMoneda.Find(id);
            db.CotizacionMoneda.Remove(cotizacionMoneda);
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
