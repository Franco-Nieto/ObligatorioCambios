using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebApplication.Models;
using WebApplication.Servicios;

namespace WebApplication.Controllers
{
    public class ClimasController : Controller
    {
        private VozDelEsteContext db = new VozDelEsteContext();
        private ClimaService _service = new ClimaService();

        // INDEX: muestra clima actual y pronóstico (últimos 15)
        public async Task<ActionResult> Index()
        {
            await _service.ActualizarClimaActualAsync();
            await _service.ActualizarPronosticoAsync();

            using (var context = new VozDelEsteContext())
            {
                var hoy = DateTime.Today;
                var manana = hoy.AddDays(1);

                var climaDelDia = context.Clima
                    .Where(c => c.Fecha >= hoy && c.Fecha < manana)
                    .OrderBy(c => c.Fecha)
                    .ToList();

                var pronosticos = context.Clima
                    .Where(c => c.Fecha.Hour == 9 || c.Fecha.Hour == 12 || c.Fecha.Hour == 18)
                    .OrderByDescending(c => c.Id)
                    .Take(15)
                    .OrderBy(c => c.Fecha)
                    .ToList();

                return View(Tuple.Create(climaDelDia, pronosticos));
            }
        }





        // DETALLES
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Clima clima = db.Clima.Find(id);
            if (clima == null)
                return HttpNotFound();

            return View(clima);
        }

        // CREAR GET
        public ActionResult Create()
        {
            return View();
        }

        // CREAR POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Fecha,Temperatura,Icono,Humedad,Viento,Condicion")] Clima clima)
        {
            if (ModelState.IsValid)
            {
                db.Clima.Add(clima);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(clima);
        }

        // EDITAR GET
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Clima clima = db.Clima.Find(id);
            if (clima == null)
                return HttpNotFound();

            return View(clima);
        }

        // EDITAR POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Fecha,Temperatura,Icono,Humedad,Viento,Condicion")] Clima clima)
        {
            if (ModelState.IsValid)
            {
                db.Entry(clima).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(clima);
        }

        // BORRAR GET
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Clima clima = db.Clima.Find(id);
            if (clima == null)
                return HttpNotFound();

            return View(clima);
        }

        // BORRAR POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Clima clima = db.Clima.Find(id);
            db.Clima.Remove(clima);
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
