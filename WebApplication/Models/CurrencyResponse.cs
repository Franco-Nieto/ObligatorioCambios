using System.Collections.Generic;

namespace WebApplication.Models
{
    public class CurrencyLayerResponse
    {
        public bool success { get; set; }
        public long timestamp { get; set; }
        public string source { get; set; }
        public Dictionary<string, decimal> quotes { get; set; }
    }
}
