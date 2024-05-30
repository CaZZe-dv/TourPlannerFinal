using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace TourPlanner.BL.Services
{
    public class RouteService : IRouteService
    {
        private const string BASE_URL = "https://api.openrouteservice.org/v2/directions/";

        private readonly string API_KEY;

        private readonly Dictionary<string, string> transportTypes = new Dictionary<string, string>();

        public RouteService(string apiKey)
        {
            this.API_KEY = apiKey;
            transportTypes.Add("Walking", "foot-walking");
            transportTypes.Add("Cycling", "cycling-road");
            transportTypes.Add("Driving", "driving-car");
        }

        private string GetRouteURL(string transportType, string start, string end)
        {
            return $"{BASE_URL}{transportTypes[transportType]}?api_key={API_KEY}&start={start}&end={end}";
        }

        public async Task<RouteResponse?> GetRouteResponse(string transportType, string start, string end)
        {
            var url = GetRouteURL(transportType, start, end);

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Clear();
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("accept", "application/json, application/geo+json, application/gpx+xml, img/png; charset=utf-8");

                using (var response = await httpClient.GetAsync(url))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string responseData = await response.Content.ReadAsStringAsync();
                        return ParseRouteResponse(responseData);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        private RouteResponse ParseRouteResponse(string responseData)
        {
            try
            {
                // Parse the JSON response
                JObject jsonResponse = JObject.Parse(responseData);

                // Extract distance and duration
                var features = jsonResponse["features"];
                if (features != null && features.Count() > 0)
                {
                    var properties = features[0]?["properties"];
                    if (properties != null)
                    {
                        double distance = (double)properties["segments"][0]["distance"];
                        double duration = (double)properties["segments"][0]["duration"];

                        // Convert distance from meters to kilometers
                        distance /= 1000;

                        // Convert duration from seconds to minutes
                        duration /= 60;

                        return new RouteResponse
                        {
                            Distance = distance,
                            Duration = duration
                        };
                    }
                }

                // Return null if distance and duration are not found
                return null;
            }
            catch (Exception ex)
            {
                // Handle JSON parsing errors
                throw new Exception("Error parsing route response", ex);
            }
        }
    }

    public class RouteResponse
    {
        public double Distance { get; set; }
        public double Duration { get; set; }
    }
}
