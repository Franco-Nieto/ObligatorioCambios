/*using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebApplication.Models;
using WebApplication.Servicios;

namespace WebApplication.Controllers
{
    public class CotizacionMonedasController : Controller
    {
        private VozDelEsteContext db = new VozDelEsteContext();
        private CotizacionesService _service = new CotizacionesService();

        
        public async Task<ActionResult> Index()
        {
            var hoy = DateTime.Today;
            var monedasInteres = new List<string> { "USD", "ARS", "EUR", "BRL" };

            
            var cotizacionesHoy = db.CotizacionMoneda
                .Where(c => c.Fecha == hoy && monedasInteres.Contains(c.TipoMoneda))
                .ToList();

            if (cotizacionesHoy.Count == monedasInteres.Count)
            {
                return View(cotizacionesHoy);
            }

         
            await _service.GuardarCotizacionesAsync();

         
            cotizacionesHoy = db.CotizacionMoneda
                .Where(c => c.Fecha == hoy && monedasInteres.Contains(c.TipoMoneda))
                .ToList();

            return View(cotizacionesHoy);
        }




       
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            CotizacionMoneda cotizacionMoneda = db.CotizacionMoneda.Find(id);
            if (cotizacionMoneda == null)
                return HttpNotFound();

            return View(cotizacionMoneda);
        }

        public ActionResult Create()
        {
            return View();
        }

        
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

        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            CotizacionMoneda cotizacionMoneda = db.CotizacionMoneda.Find(id);
            if (cotizacionMoneda == null)
                return HttpNotFound();

            return View(cotizacionMoneda);
        }

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


        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            CotizacionMoneda cotizacionMoneda = db.CotizacionMoneda.Find(id);
            if (cotizacionMoneda == null)
                return HttpNotFound();

            return View(cotizacionMoneda);
        }

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
                db.Dispose();

            base.Dispose(disposing);
        }
    }
}*/
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebApplication.Models;
using WebApplication.Servicios;

namespace WebApplication.Controllers
{
    public class CotizacionMonedasController : Controller
    {
        private VozDelEsteContext db = new VozDelEsteContext();
        private CotizacionesService _service = new CotizacionesService();

        public async Task<ActionResult> Index()
        {
            var hoy = DateTime.Today;
            var monedasInteres = new List<string> { "USD", "ARS", "EUR", "BRL" };

            var cotizacionesHoy = db.CotizacionMoneda
                .Where(c => c.Fecha == hoy && monedasInteres.Contains(c.TipoMoneda))
                .ToList();

            if (cotizacionesHoy.Count == monedasInteres.Count)
            {
                return View(cotizacionesHoy);
            }

            await _service.GuardarCotizacionesAsync();

            cotizacionesHoy = db.CotizacionMoneda
                .Where(c => c.Fecha == hoy && monedasInteres.Contains(c.TipoMoneda))
                .ToList();

            return View(cotizacionesHoy);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            CotizacionMoneda cotizacionMoneda = db.CotizacionMoneda.Find(id);
            if (cotizacionMoneda == null)
                return HttpNotFound();

            return View(cotizacionMoneda);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Fecha,TipoMoneda,Valor")] CotizacionMoneda cotizacionMoneda)
        {
            if (ModelState.IsValid)
            {
                db.CotizacionMoneda.Add(cotizacionMoneda);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cotizacionMoneda);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            CotizacionMoneda cotizacionMoneda = db.CotizacionMoneda.Find(id);
            if (cotizacionMoneda == null)
                return HttpNotFound();

            return View(cotizacionMoneda);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Fecha,TipoMoneda,Valor")] CotizacionMoneda cotizacionMoneda)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cotizacionMoneda).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cotizacionMoneda);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            CotizacionMoneda cotizacionMoneda = db.CotizacionMoneda.Find(id);
            if (cotizacionMoneda == null)
                return HttpNotFound();

            return View(cotizacionMoneda);
        }

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
                db.Dispose();

            base.Dispose(disposing);
        }
    }
}

