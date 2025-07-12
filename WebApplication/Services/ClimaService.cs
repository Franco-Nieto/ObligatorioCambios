using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using WebApplication.Models;

namespace WebApplication.Servicios
{
    public class ClimaService
    {
        private readonly string _apiKey = "f939b347c0f7440d1c1fe1c4d192e806";
        private readonly string _urlPronostico = "https://api.openweathermap.org/data/2.5/forecast";
        private readonly string _urlActual = "https://api.openweathermap.org/data/2.5/weather";

        // ----- ACTUALIZAR PRONÓSTICO -----
        public async Task ActualizarPronosticoAsync()
        {
            var client = new RestClient(_urlPronostico);
            var request = new RestRequest();
            request.AddParameter("q", "Maldonado,UY");
            request.AddParameter("appid", _apiKey);
            request.AddParameter("units", "metric");
            request.AddParameter("lang", "es");

            var response = await client.ExecuteAsync(request);
            if (!response.IsSuccessful) return;

            var forecast = JsonConvert.DeserializeObject<ForecastResponse>(response.Content);
            if (forecast?.List == null || !forecast.List.Any()) return;

            using (var context = new VozDelEsteContext())
            {
                foreach (var item in forecast.List)
                {
                    var fecha = DateTime.Parse(item.DtTxt).ToLocalTime();
                    if (fecha.Hour == 9 || fecha.Hour == 12 || fecha.Hour == 18)
                    {
                        var existente = context.Clima.FirstOrDefault(c => c.Fecha == fecha);
                        if (existente != null)
                        {
                            existente.Temperatura = item.Main.Temp;
                            existente.Humedad = item.Main.Humidity;
                            existente.Viento = (int)item.Wind.Speed;
                            existente.Condicion = item.Weather.FirstOrDefault()?.Description ?? "";
                            existente.Icono = item.Weather.FirstOrDefault()?.Icon ?? "";
                        }
                        else
                        {
                            context.Clima.Add(new Clima
                            {
                                Fecha = fecha,
                                Temperatura = item.Main.Temp,
                                Humedad = item.Main.Humidity,
                                Viento = (int)item.Wind.Speed,
                                Condicion = item.Weather.FirstOrDefault()?.Description ?? "",
                                Icono = item.Weather.FirstOrDefault()?.Icon ?? ""
                            });
                        }
                    }
                }

                context.SaveChanges();
            }
        }

        // ----- ACTUALIZAR CLIMA ACTUAL -----
        public async Task ActualizarClimaActualAsync()
        {
            var ahora = DateTime.Now;
            var horaActual = new DateTime(ahora.Year, ahora.Month, ahora.Day, ahora.Hour, 0, 0);

            // Solo guardar si no es 9, 12, 18
            if (horaActual.Hour == 9 || horaActual.Hour == 12 || horaActual.Hour == 18)
                return;

            var client = new RestClient(_urlActual);
            var request = new RestRequest();
            request.AddParameter("q", "Maldonado,UY");
            request.AddParameter("appid", _apiKey);
            request.AddParameter("units", "metric");
            request.AddParameter("lang", "es");

            var response = await client.ExecuteAsync(request);
            if (!response.IsSuccessful) return;

            var data = JsonConvert.DeserializeObject<CurrentWeatherResponse>(response.Content);
            if (data == null) return;

            using (var context = new VozDelEsteContext())
            {
                var existente = context.Clima.FirstOrDefault(c => c.Fecha == horaActual);

                if (existente != null)
                {
                    existente.Temperatura = (decimal)data.Main.Temp;
                    existente.Humedad = data.Main.Humidity;
                    existente.Viento = (int)data.Wind.Speed;
                    existente.Condicion = data.Weather.FirstOrDefault()?.Description ?? "";
                    existente.Icono = data.Weather.FirstOrDefault()?.Icon ?? "";
                }
                else
                {
                    context.Clima.Add(new Clima
                    {
                        Fecha = horaActual,
                        Temperatura = (decimal)data.Main.Temp,
                        Humedad = data.Main.Humidity,
                        Viento = (int)data.Wind.Speed,
                        Condicion = data.Weather.FirstOrDefault()?.Description ?? "",
                        Icono = data.Weather.FirstOrDefault()?.Icon ?? ""
                    });
                }

                context.SaveChanges();
            }
        }

        // ----- OBTENER PRONÓSTICO (últimos 15 con hora válida) -----
        public async Task<List<Clima>> ObtenerDatosClimaAsync()
        {
            await ActualizarClimaActualAsync();
            await ActualizarPronosticoAsync();

            using (var context = new VozDelEsteContext())
            {
                return context.Clima
                    .Where(c => c.Fecha.Hour == 9 || c.Fecha.Hour == 12 || c.Fecha.Hour == 18)
                    .OrderByDescending(c => c.Id)
                    .Take(15)
                    .OrderBy(c => c.Fecha)
                    .ToList();
            }
        }

        // ----- OBTENER CLIMA ACTUAL (no pronóstico) -----
        public async Task<List<Clima>> ObtenerDatosClimaDelDiaAsync()
        {
            await ActualizarClimaActualAsync();
            await ActualizarPronosticoAsync();

            using (var context = new VozDelEsteContext())
            {
                var hoy = DateTime.Today;
                var manana = hoy.AddDays(1);

                return context.Clima
                    .Where(c => c.Fecha >= hoy && c.Fecha < manana)
                    .OrderBy(c => c.Fecha)
                    .ToList();
            }
        }
        public async Task<List<Clima>> ObtenerPronosticoAsync()
        {
            await ActualizarPronosticoAsync();

            using (var context = new VozDelEsteContext())
            {
                return context.Clima
                    .Where(c => c.Fecha.Hour == 9 || c.Fecha.Hour == 12 || c.Fecha.Hour == 18)
                    .OrderByDescending(c => c.Id)
                    .Take(15)
                    .OrderBy(c => c.Fecha)
                    .ToList();
            }
        }

        public async Task<List<Clima>> ObtenerClimaActualDelDiaAsync()
        {
            await ActualizarClimaActualAsync();

            using (var context = new VozDelEsteContext())
            {
                var hoy = DateTime.Today;
                var manana = hoy.AddDays(1);

                return context.Clima
                    .Where(c => c.Fecha >= hoy && c.Fecha < manana)
                    .OrderBy(c => c.Fecha)
                    .ToList();
            }
        }





    }
}
