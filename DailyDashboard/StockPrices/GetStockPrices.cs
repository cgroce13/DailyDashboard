using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using System.Linq.Expressions;

namespace StockPrices
{
    public static class GetStockPrices
    {
        private static readonly HttpClient client = new HttpClient();

        [FunctionName("GetStockPrices")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            
            //ensure we have a stock symbol to look up
            string stockSymbol = req.Query["symbol"];
            if (String.IsNullOrEmpty(stockSymbol))
                return new BadRequestObjectResult("Please provide a stock symbol");

            //api only accepts uppercase
            stockSymbol = stockSymbol.ToUpper();
            
            //ensure we have a token to call the api
            string apiToken = System.Environment.GetEnvironmentVariable("FinnHubapiToken");
            if (string.IsNullOrEmpty(apiToken))
                return new BadRequestObjectResult("no api token provided");

            //make the api call
            client.DefaultRequestHeaders.Accept.Clear();
            var stringTask = client.GetStringAsync(String.Format("https://finnhub.io/api/v1/quote?symbol={0}&token={1}",stockSymbol, apiToken));
            var msg = await stringTask;
            
            StockPriceResult result = JsonConvert.DeserializeObject<StockPriceResult>(msg);

            
            return result.CurrentPrice > 0
                ? (ActionResult)new OkObjectResult(JsonConvert.SerializeObject(result))
                : new BadRequestObjectResult("Error getting price data");
        }
    }
}
