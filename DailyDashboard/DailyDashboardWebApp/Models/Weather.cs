using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DailyDashboardWebApp.Models
{
    public class Weather
    {
        public string City { get; set; }

        [DisplayFormat(DataFormatString = "{0: 0 F}")]
        public double Temp { get; set; }

        [DisplayFormat(DataFormatString = "{0: 0 F}")]
        public double FeelsLike { get; set; }

        [DisplayFormat(DataFormatString = "{0: 0 \\%}")]
        public double Humidity { get; set; }

        [DisplayFormat(DataFormatString = "{0: 0 F}")]
        public double Low { get; set; }

        [DisplayFormat(DataFormatString = "{0: 0 F}")]
        public double High { get; set; }

        [DisplayFormat(DataFormatString = "{0: 0 mph}")]
        public double Wind { get; set; }

        [DisplayFormat(DataFormatString = "{0:hh\\:mm}")]
        public TimeSpan Sunrise { get; set; }

        [DisplayFormat(DataFormatString = "{0:hh\\:mm}")]
        public TimeSpan Sunset { get; set; }


    }
}
