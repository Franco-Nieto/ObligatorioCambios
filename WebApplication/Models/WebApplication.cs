using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace WebApplication
{
    public class ForecastResponse
    {
        [JsonProperty("list")]
        public List<ForecastItem> List { get; set; }

        [JsonProperty("city")]
        public CityInfo City { get; set; }
    }

    public class ForecastItem
    {
        [JsonProperty("dt")]
        public long Dt { get; set; }

        [JsonProperty("main")]
        public MainData Main { get; set; }

        [JsonProperty("weather")]
        public List<WeatherInfo> Weather { get; set; }

        [JsonProperty("wind")]
        public WindData Wind { get; set; }

        [JsonProperty("dt_txt")]
        public string DtTxt { get; set; }
    }

    public class MainData
    {
        [JsonProperty("temp")]
        public decimal Temp { get; set; }

        [JsonProperty("humidity")]
        public int Humidity { get; set; }
    }

    public class WeatherInfo
    {
        [JsonProperty("main")]
        public string Main { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }
    }

    public class WindData
    {
        [JsonProperty("speed")]
        public decimal Speed { get; set; }
    }

    public class CityInfo
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
