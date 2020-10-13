using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DailyDashboardWebApp.Models;
using Newtonsoft.Json;

namespace DailyDashboardWebApp.Pages
{
    public class WeatherModel : PageModel
    {
        private readonly HttpClient client = new HttpClient();

        private string[] zipcodes = new string[]{"77089", "76885"};

        private readonly string weatherurl = "https://clgdailydashboard.azurewebsites.net/api/WeatherForecast?zipcode={0}";

        public List<Weather> WeatherResults { get; set; } = new List<Weather>();

        public async Task<IActionResult> OnGetAsync()
        {
            string accessToken = this.Request.Headers["X-MS-TOKEN-AAD-ACCESS-TOKEN"];

            foreach (string zc in zipcodes)
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
                var result = await client.GetStringAsync(String.Format(weatherurl, zc));

                Weather weather = JsonConvert.DeserializeObject<Weather>(result);
                WeatherResults.Add(weather);
            }

            return Page();
        }
    }
}