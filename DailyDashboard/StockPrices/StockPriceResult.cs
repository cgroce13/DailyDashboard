using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace StockPrices
{
    public class StockPriceResult
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
