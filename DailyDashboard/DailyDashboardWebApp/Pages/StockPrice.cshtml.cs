using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DailyDashboardWebApp.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net;
using Microsoft.Extensions.Logging;

namespace DailyDashboardWebApp.Pages
{
    public class StockPriceModel : PageModel
    {
        private readonly ILogger<StockPriceModel> _logger;

        private readonly HttpClient client = new HttpClient();

        private readonly string stockpriceurl = "https://clgdailydashboard.azurewebsites.net/api/getstockprices?symbol={0}";

        [BindProperty]
        public string Symbols { get; set; }

        public Dictionary<string, StockPrice> Stocks { get; set; } = new Dictionary<string, StockPrice>();

        public StockPriceModel(ILogger<StockPriceModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            
        }

        public async Task<IActionResult> OnPostAsync()
        {
            string accessToken = this.Request.Headers["X-MS-TOKEN-AAD-ACCESS-TOKEN"];

            String[] symbols = Symbols.Replace(" ","").Split(",");

            try
            {
                foreach (string s in symbols)
                {

                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
                    var result = await client.GetStringAsync(String.Format(stockpriceurl, s));

                    StockPrice stock = JsonConvert.DeserializeObject<StockPrice>(result);
                    Stocks.Add(s, stock);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error occurred when retrieving stock price",null);
            }

            //reset the input field
            Symbols = String.Empty;
            return Page();
        }

    }
}