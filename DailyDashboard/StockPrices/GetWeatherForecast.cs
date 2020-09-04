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
using DailyDashboardAPIs;

namespace StockPrices
{
    public static class GetWeatherForecast
    {
        private static readonly HttpClient client = new HttpClient();

        [FunctionName("WeatherForecast")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            //ensure we have a zip code
            string zipcode = req.Query["zipcode"];
            if (String.IsNullOrEmpty(zipcode))
                return new BadRequestObjectResult("Please provide a zipcode");

            //ensure we have a token to call the api
            string apiToken = System.Environment.GetEnvironmentVariable("WeatherapiKey");
            if (string.IsNullOrEmpty(apiToken))
                return new BadRequestObjectResult("no api token provided");

            //make the api call
            client.DefaultRequestHeaders.Accept.Clear();
            var stringTask = client.GetStringAsync(String.Format("https://api.openweathermap.org/data/2.5/weather?zip={0}&units=imperial&appid={1}", zipcode, apiToken));
            var msg = await stringTask;

            WeatherResult result = null;

            try
            {
                result = new WeatherResult(msg);
            }
            catch (Exception e) 
            {
                log.LogError(e, "problem parsing weather result");
                return new BadRequestObjectResult("Could not parse result from api");
            }
            


            return result != null
                ? (ActionResult)new OkObjectResult(result)
                : new BadRequestObjectResult("Error getting price data");
        }
    }
}
