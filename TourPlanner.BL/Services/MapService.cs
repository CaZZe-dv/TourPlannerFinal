using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.BL.Services
{
    public class MapService : IMapService
    {
        private const string BASE_URL = "https://tile.openstreetmap.org";
        private const string APPLICATION_NAME = "TourPlanner_Boeck_Fichtinger";

        private readonly HttpClient _httpClient;
        public MapService()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("User-Agent", APPLICATION_NAME);
        }

        private string getMapUrl(float zoom, float x, float y)
        {
            return $"{BASE_URL}/{zoom}/{x}/{y}.png";
        }

        public async Task<string?> getResponse(float zoom, float x, float y)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(getMapUrl(zoom,x,y));
                response.EnsureSuccessStatusCode();

                string policyText = await response.Content.ReadAsStringAsync();
                return policyText;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error retrieving tile usage policy: {ex.Message}");
                return null;
            }
        }
    }
}
