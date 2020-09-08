using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DailyDashboardWebApp.Models;
using System.Net.Http;
using Newtonsoft.Json;

namespace DailyDashboardWebApp.Pages
{
    public class StockPriceModel : PageModel
    {
        private readonly HttpClient client = new HttpClient();

        private readonly string stockpriceurl = "https://clgdailydashboard.azurewebsites.net/api/getstockprices?symbol={0}";

        [BindProperty]
        public string Symbols { get; set; }

        //public StockPrice Stock { get; set; }

        public Dictionary<string, StockPrice> Stocks { get; set; } = new Dictionary<string, StockPrice>();

        public void OnGet()
        {
            
        }

        public async Task<IActionResult> OnPostAsync()
        {
            String[] symbols = Symbols.Replace(" ","").Split(",");

            try
            {

                foreach (string s in symbols)
                {
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Clear();
                    var result = await client.GetStringAsync(String.Format(stockpriceurl, s));

                    StockPrice stock = JsonConvert.DeserializeObject<StockPrice>(result);
                    Stocks.Add(s, stock);
                }
            }
            catch(Exception e)
            {
                return RedirectToPage("Error");
            }

            //reset the input field
            Symbols = String.Empty;
            return Page();
        }

    }
}