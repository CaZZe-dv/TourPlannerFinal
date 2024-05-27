using Newtonsoft.Json;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.BL.Services
{
    public class RouteService : IRouteService
    {
        private const string BASE_URL = "https://api.openrouteservice.org/v2/directions/driving-car?api_key=";

        private readonly string API_KEY;

        public RouteService(string apiKey)
        {
            this.API_KEY = apiKey;
        }

        private string getRouteURL(string start, string end)
        {
            return $"{BASE_URL}{API_KEY}&start={start}&end={end}";
        }

        public async Task<string> getResponse(string start, string end)
        {
            var baseAddress = new Uri(getRouteURL(start,end));

            using (var httpClient = new HttpClient { BaseAddress = baseAddress })
            {
                httpClient.DefaultRequestHeaders.Clear();
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("accept", "application/json, application/geo+json, application/gpx+xml, img/png; charset=utf-8");

                using (var response = await httpClient.GetAsync("directions"))
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    //var data = JsonConvert.DeserializeObject(responseData);
                    return responseData;
                }
            }
        }
    }
}
