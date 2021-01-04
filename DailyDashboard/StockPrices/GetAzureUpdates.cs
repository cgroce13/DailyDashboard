using System;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using DailyDashboardAPIs;
using System.ServiceModel.Syndication;
using System.Collections.Generic;
using System.Xml;
using System.Linq;
using System.Threading.Tasks;

namespace DailyDashboardAPIs
{
    public static class GetAzureUpdates
    {
        [FunctionName("GetAzureUpdates")]
        public static IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation($"GetAzureUpdates function executed at: {DateTime.Now}");

            string json = null;

            try
            {
                var url = "https://azurecomcdn.azureedge.net/en-us/updates/feed/";
                using var reader = XmlReader.Create(url);
                SyndicationFeed feed = SyndicationFeed.Load(reader);
                List<SyndicationItem> items = feed.Items.Take<SyndicationItem>(10).ToList();
                json = JsonConvert.SerializeObject(items);
            }
            catch (Exception e)
            {
                log.LogError(e, "problem getting azure updates");
                return new BadRequestObjectResult("Could not parse result from api");
            }

            return json != null
                ? (ActionResult)new OkObjectResult(json)
                : new BadRequestObjectResult("Error getting price data");

        }
    }
}
