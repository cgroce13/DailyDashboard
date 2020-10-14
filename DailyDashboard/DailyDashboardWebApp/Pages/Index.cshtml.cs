using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace DailyDashboardWebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        
        public double StockPrice { get; set; }

        public string Greeting { get; set; }

        public string CurrentTime { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            TimeZoneInfo centralTimezone = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");

            DateTime now = TimeZoneInfo.ConvertTime(DateTime.Now, centralTimezone);

            if (now.Hour< 12)
            { Greeting = "Good Morning!"; }
            else if (now.Hour > 12 && now.Hour < 17)
            { Greeting = "Good Afternoon!"; }
            else 
            { Greeting = "Good Evening!"; }

            CurrentTime = now.ToString("h:mm tt");

        }
    }
}
