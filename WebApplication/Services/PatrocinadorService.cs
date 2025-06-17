using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication.Models;

namespace WebApplication.Services
{
   public class PatrocinadorService
   {
      private readonly VozDelEsteContext _contexto;

      public PatrocinadorService(VozDelEsteContext contexto)
      {
         _contexto = contexto;
      }

      public List<Patrocinador> ObtenerPatrocinadores()
      {
         return _contexto.Patrocinador.ToList();
      }
   }
}