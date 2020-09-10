using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace DailyDashboardAPIs
{
    public class WeatherResult
    {
        public string City { get; set; }

        public double Temp { get; set; }

        
        public double FeelsLike { get; set; }

        
        public double Humidity { get; set; }

        
        public double Low { get; set; }

        
        public double High { get; set; }

        
        public double Wind { get; set; }

        
        public TimeSpan Sunrise { get; set; }

        
        public TimeSpan Sunset { get; set; }


        public WeatherResult(string msg)
        {
            dynamic data = JObject.Parse(msg);
            Temp = data.main.temp;
            FeelsLike = data.main.feels_like;
            Humidity = data.main.humidity;
            Low = data.main.temp_min;
            High = data.main.temp_max;
            Wind = data.wind.speed;
            City = data.name;

            long sunriseUTC = data.sys.sunrise;
            DateTimeOffset sunriseOffset = DateTimeOffset.FromUnixTimeSeconds(sunriseUTC);
            Sunrise = sunriseOffset.LocalDateTime.TimeOfDay;
            
            long sunsetUTC = data.sys.sunrise;
            DateTimeOffset sunsetOffset = DateTimeOffset.FromUnixTimeSeconds(sunsetUTC);
            Sunset = sunsetOffset.LocalDateTime.TimeOfDay;
        }

    }
}
