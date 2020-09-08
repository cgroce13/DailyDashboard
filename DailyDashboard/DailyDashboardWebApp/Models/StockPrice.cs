using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DailyDashboardWebApp.Models
{
    public class StockPrice
    {
        [JsonProperty("c")]
        public double CurrentPrice { get; set; }

        [JsonProperty("h")]
        public double HighPrice { get; set; }

        [JsonProperty("l")]
        public double LowPrice { get; set; }

        [JsonProperty("o")]
        public double OpeningPrice { get; set; }

        [JsonProperty("pc")]
        public double PreviousClose { get; set; }

    }
}
