using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

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

        private string GetMapUrl(string zoom, string x, string y)
        {
            return $"{BASE_URL}/{zoom}/{x}/{y}.png";
        }

        public async Task<BitmapImage?> GetMapTileAsync(string zoom, string x, string y)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(GetMapUrl(zoom, x, y));
                response.EnsureSuccessStatusCode();

                byte[] imageData = await response.Content.ReadAsByteArrayAsync();
                return ConvertToBitmapImage(imageData);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error retrieving map tile: {ex.Message}");
                return null;
            }
        }

        private BitmapImage ConvertToBitmapImage(byte[] imageData)
        {
            using (var ms = new MemoryStream(imageData))
            {
                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = ms;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.Freeze(); // To make the image cross-thread accessible
                return bitmapImage;
            }
        }
    }
}
