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

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            if(DateTime.Now.Hour >= 0 && DateTime.Now.Hour < 12)
            { Greeting = "Good Morning!"; }
            else if (DateTime.Now.Hour > 12 && DateTime.Now.Hour < 5)
            { Greeting = "Good Afternoon!"; }
            else 
            { Greeting = "Good Evening!"; }

        }
    }
}
