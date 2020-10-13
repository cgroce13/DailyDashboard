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

namespace DailyDashboardWebApp.Pages
{
    public class StockPriceModel : PageModel
    {
        private readonly HttpClient client = new HttpClient();

        private readonly string stockpriceurl = "https://clgdailydashboard.azurewebsites.net/api/getstockprices?symbol={0}";

        [BindProperty]
        public string Symbols { get; set; }

        public Dictionary<string, StockPrice> Stocks { get; set; } = new Dictionary<string, StockPrice>();

        public void OnGet()
        {
            
        }

        public async Task<IActionResult> OnPostAsync()
        {
            string accessToken = this.Request.Headers["X-MS-TOKEN-AAD-ACCESS-TOKEN"];

            String[] symbols = Symbols.Replace(" ","").Split(",");

            foreach (string s in symbols)
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
                var result = await client.GetStringAsync(String.Format(stockpriceurl, s));

                StockPrice stock = JsonConvert.DeserializeObject<StockPrice>(result);
                Stocks.Add(s, stock);
            }

            //reset the input field
            Symbols = String.Empty;
            return Page();
        }

    }
}