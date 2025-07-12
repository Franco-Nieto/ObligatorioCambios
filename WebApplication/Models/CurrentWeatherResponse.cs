using Newtonsoft.Json;
using System.Collections.Generic;

public class CurrentWeatherResponse
{
    [JsonProperty("weather")]
    public List<Weather> Weather { get; set; }

    [JsonProperty("main")]
    public Main Main { get; set; }

    [JsonProperty("wind")]
    public Wind Wind { get; set; }

    [JsonProperty("dt")]
    public long Dt { get; set; }  // Unix timestamp

    // Puedes agregar más campos si los necesitás
}

public class Weather
{
    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("icon")]
    public string Icon { get; set; }
}

public class Main
{
    [JsonProperty("temp")]
    public float Temp { get; set; }

    [JsonProperty("humidity")]
    public int Humidity { get; set; }
}

public class Wind
{
    [JsonProperty("speed")]
    public float Speed { get; set; }
}
