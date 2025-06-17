using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using WebApplication.Models;
using WebApplication.ViewModels.DTO;

namespace WebApplication.Services
{
   public class ProgramacionService
   {
      private readonly VozDelEsteContext _contexto;

      public ProgramacionService(VozDelEsteContext contexto)
      {
         _contexto = contexto;
      }

      public List<ProgramaDTO> ObtenerResumen(int cantidad)
      {
         var ahora = DateTime.Now;

         var programas = _contexto.Programacion
             .Where(p => p.FechaHorario > ahora)
             .OrderBy(p => p.FechaHorario)
             .Take(cantidad)
             .Select(p => new ProgramaDTO
             {
                ProgramacionId = p.Id,
                FechaHorario = p.FechaHorario,
                Nombre = p.ProgramaRadio.Nombre,
                Descripcion = p.ProgramaRadio.Descripcion,
                ImagenUrl = p.ProgramaRadio.ImagenUrl
             })
             .ToList();

         return programas;
      }

      public List<ProgramaDTO> ObtenerProgramasDelDia()
      {
         var hoy = DateTime.Today;
         var mañana = hoy.AddDays(1);

         var programas = _contexto.Programacion
             .Where(p => p.FechaHorario >= hoy && p.FechaHorario < mañana)
             .OrderBy(p => p.FechaHorario)
             .Select(p => new ProgramaDTO
             {
                ProgramacionId = p.Id,
                FechaHorario = p.FechaHorario,
                Nombre = p.ProgramaRadio.Nombre,
                Descripcion = p.ProgramaRadio.Descripcion,
                ImagenUrl = p.ProgramaRadio.ImagenUrl
             })
             .ToList();

         return programas;
      }

      public List<ProgramaDTO> ObtenerProgramacionSemanal()
      {
         var ahora = DateTime.Now;
         var inicioSemana = ahora.Date.AddDays(-(int)ahora.DayOfWeek); // Domingo
         var finSemana = inicioSemana.AddDays(7);                      // Próximo domingo

         var programas = _contexto.Programacion
             .Where(p => p.FechaHorario >= inicioSemana && p.FechaHorario < finSemana)
             .OrderBy(p => p.FechaHorario)
             .Select(p => new ProgramaDTO
             {
                ProgramacionId = p.Id,
                FechaHorario = p.FechaHorario,
                Duracion = p.ProgramaRadio.Duracion,
                Nombre = p.ProgramaRadio.Nombre,
                Descripcion = p.ProgramaRadio.Descripcion,
                ImagenUrl = p.ProgramaRadio.ImagenUrl
             })
             .ToList();

         return programas;
      }



   }
}