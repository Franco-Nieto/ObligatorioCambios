/*using RestSharp;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Models;


namespace WebApplication.Servicios
{
    public class CotizacionesService
    {
        private readonly string _apiKey = "a2531f8a9d6b686f248344b7c375821d"; // ← Cambiá esto por tu clave real
        private readonly string _url = "http://api.currencylayer.com/live";

        public async Task GuardarCotizacionesAsync()
        {
            var client = new RestClient(_url);
            var request = new RestRequest();

            request.Method = Method.Get;

            request.AddParameter("access_key", _apiKey);
            request.AddParameter("currencies", "USD,ARS,BRL,EUR");
            request.AddParameter("source", "UYU");
            request.AddParameter("format", "1");

            var response = await client.ExecuteAsync(request);


            if (!response.IsSuccessful)
                return;

            var datos = JsonConvert.DeserializeObject<CurrencyLayerResponse>(response.Content);

            if (datos?.quotes == null || !datos.success)
                return;

            DateTime fechaHoy = DateTime.Today;

            // Buscamos la cotización UYUUSD para usar como base
            if (!datos.quotes.TryGetValue("UYUUSD", out decimal cotizacionUYUUSD))
            {
                // Si no está la cotización de UYU a USD, no hacemos nada
                return;
            }

            using (var context = new VozDelEsteContext())
            {
                foreach (var par in datos.quotes)
                {
                    string key = par.Key; // Ejemplo: "UYUUSD", "UYUARS", etc.
                    string tipoMoneda = key.Substring(3); // Ejemplo: "USD", "ARS", etc.

                    if (tipoMoneda == "USD")
                    {
                        // Para USD guardamos la cotización directa
                        var valorConvertido = par.Value;

                        var existenteUsd = context.CotizacionMoneda
                            .FirstOrDefault(c => c.Fecha == fechaHoy && c.TipoMoneda == tipoMoneda);

                        if (existenteUsd == null)
                        {
                            context.CotizacionMoneda.Add(new CotizacionMoneda
                            {
                                Fecha = fechaHoy,
                                TipoMoneda = tipoMoneda,
                                ValorCompra = valorConvertido,
                                ValorVenta = valorConvertido
                            });
                        }
                        else
                        {
                            existenteUsd.ValorCompra = valorConvertido;
                            existenteUsd.ValorVenta = valorConvertido;
                        }
                    }
                    else if (tipoMoneda != "UYU") // No guardamos UYU (moneda base)
                    {
                        // Convertimos la cotización a partir del peso uruguayo usando la fórmula:
                        // Valor en pesos uruguayos = cotizacion de la moneda respecto a USD / cotizacion UYUUSD
                        var valorConvertido = par.Value / cotizacionUYUUSD;

                        var existente = context.CotizacionMoneda
                            .FirstOrDefault(c => c.Fecha == fechaHoy && c.TipoMoneda == tipoMoneda);

                        if (existente == null)
                        {
                            context.CotizacionMoneda.Add(new CotizacionMoneda
                            {
                                Fecha = fechaHoy,
                                TipoMoneda = tipoMoneda,
                                ValorCompra = valorConvertido,
                                ValorVenta = valorConvertido
                            });
                        }
                        else
                        {
                            existente.ValorCompra = valorConvertido;
                            existente.ValorVenta = valorConvertido;
                        }
                    }
                }

                context.SaveChanges();
            }
        }
    }
}*/
using RestSharp;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using WebApplication.Models;
using System.Linq;

namespace WebApplication.Servicios
{
    public class CotizacionesService
    {
        private readonly string _apiKey = "583f05c5547873c881272fbe27832683";
        private readonly string _url = "http://api.currencylayer.com/live";

        public async Task GuardarCotizacionesAsync()
        {
            var client = new RestClient(_url);
            var request = new RestRequest();

            request.Method = Method.Get;

            request.AddParameter("access_key", _apiKey);
            request.AddParameter("currencies", "USD,ARS,BRL,EUR");
            request.AddParameter("source", "UYU");
            request.AddParameter("format", "1");

            var response = await client.ExecuteAsync(request);

            if (!response.IsSuccessful)
                return;//Es preferoble retornar unb mensaje de error

            var datos = JsonConvert.DeserializeObject<CurrencyLayerResponse>(response.Content);

            if (datos?.quotes == null || !datos.success)
                return;//Es preferoble retornar unb mensaje de error

            DateTime fechaHoy = DateTime.Today;

            using (var context = new VozDelEsteContext())
            {

                foreach (var par in datos.quotes)
                {
                    string key = par.Key; // Ejemplo: "UYUUSD", "UYUARS", etc.
                    string tipoMoneda = key.Substring(3); // USD, ARS, etc.
                    decimal valorDesdeUYU = par.Value;

                    if (tipoMoneda != "UYU") // ignoramos la base
                    {
                        // Convertimos: cuánto vale 1 unidad de esa moneda en pesos uruguayos
                        // Ejemplo: 1 USD = 1 / 0.024834 ≈ 40.25 UYU
                        decimal valorConvertido = 1 / valorDesdeUYU;

                        var existente = context.CotizacionMoneda
                            .FirstOrDefault(c => c.Fecha == fechaHoy && c.TipoMoneda == tipoMoneda);

                        if (existente == null)
                        {
                            context.CotizacionMoneda.Add(new CotizacionMoneda
                            {
                                Fecha = fechaHoy,
                                TipoMoneda = tipoMoneda,
                                Valor = valorConvertido,
                                
                            });
                        }
                        else
                        {
                            existente.Valor = valorConvertido;
                            
                        }
                    }
                }


                context.SaveChanges();
            }
        }
    }

}

