using System.Drawing;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace TourPlanner.BL.Services
{
    public class MapService : IMapService
    {
        public async Task<BitmapSource?> GenerateMapAsync(string zoomStr, string startStr, string endStr)
        {
            try
            {
                // Parse zoom level
                if (!int.TryParse(zoomStr, out int zoom))
                {
                    // Return null if zoom level parsing fails
                    return null;
                }

                // Parse start coordinates
                string[] startCoords = startStr.Split(',');
                if (startCoords.Length != 2 ||
                    !double.TryParse(startCoords[0], NumberStyles.Float, CultureInfo.InvariantCulture, out double startLon) ||
                    !double.TryParse(startCoords[1], NumberStyles.Float, CultureInfo.InvariantCulture, out double startLat))
                {
                    // Return null if start coordinates parsing fails
                    return null;
                }

                // Parse end coordinates
                string[] endCoords = endStr.Split(',');
                if (endCoords.Length != 2 ||
                    !double.TryParse(endCoords[0], NumberStyles.Float, CultureInfo.InvariantCulture, out double endLon) ||
                    !double.TryParse(endCoords[1], NumberStyles.Float, CultureInfo.InvariantCulture, out double endLat))
                {
                    // Return null if end coordinates parsing fails
                    return null;
                }

                // Determine the minimum and maximum coordinates
                double minLon = Math.Min(startLon, endLon);
                double minLat = Math.Min(startLat, endLat);
                double maxLon = Math.Max(startLon, endLon);
                double maxLat = Math.Max(startLat, endLat);

                // Calculate the tile numbers for each corner of the bounding box
                Tile topLeftTile = LatLonToTile(maxLat, minLon, zoom);
                Tile bottomRightTile = LatLonToTile(minLat, maxLon, zoom);

                // Determine the number of tiles to fetch in each dimension
                int tilesX = bottomRightTile.X - topLeftTile.X + 1;
                int tilesY = bottomRightTile.Y - topLeftTile.Y + 1;

                using (var httpClient = new HttpClient())
                {
                    // Create a Bitmap to hold all the tiles
                    Bitmap finalImage = new Bitmap(tilesX * 256, tilesY * 256);

                    using (Graphics g = Graphics.FromImage(finalImage))
                    {
                        // Fetch and draw each tile
                        for (int x = topLeftTile.X; x <= bottomRightTile.X; x++)
                        {
                            for (int y = topLeftTile.Y; y <= bottomRightTile.Y; y++)
                            {
                                Bitmap? tileBitmap = await FetchTileAsync(zoom, x, y);

                                if (tileBitmap == null)
                                {
                                    // Return null if fetching a tile fails
                                    return null;
                                }

                                // Calculate position to draw the tile
                                int xPos = (x - topLeftTile.X) * 256;
                                int yPos = (y - topLeftTile.Y) * 256;

                                // Draw the tile onto the final image
                                g.DrawImage(tileBitmap, xPos, yPos);
                            }
                        }
                    }

                    // Convert the Bitmap to BitmapSource
                    return ConvertBitmapToBitmapSource(finalImage);
                }
            }
            catch (Exception)
            {
                // Return null if an exception occurs
                return null;
            }
        }

        private async Task<Bitmap?> FetchTileAsync(int zoom, int x, int y)
        {
            string tileUrl = $"https://tile.openstreetmap.org/{zoom}/{x}/{y}.png";

            try
            {
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.GetAsync(tileUrl);
                    response.EnsureSuccessStatusCode();

                    if (!response.Content.Headers.ContentType.MediaType.StartsWith("image"))
                    {
                        // Return null if the content type is not an image
                        return null;
                    }

                    using (var stream = await response.Content.ReadAsStreamAsync())
                    {
                        return new Bitmap(stream);
                    }
                }
            }
            catch
            {
                // Return null if an exception occurs during fetching the tile
                return null;
            }
        }

        private BitmapSource ConvertBitmapToBitmapSource(Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.Freeze();
                return bitmapImage;
            }
        }

        private record Tile(int X, int Y);

        private Tile LatLonToTile(double lat, double lon, int zoom)
        {
            double latRad = Math.PI * lat / 180.0;
            double n = Math.Pow(2, zoom);
            int xTile = (int)Math.Floor((lon + 180.0) / 360.0 * n);
            int yTile = (int)Math.Floor((1.0 - Math.Log(Math.Tan(latRad) + (1 / Math.Cos(latRad))) / Math.PI) / 2.0 * n);
            return new Tile(xTile, yTile);
        }
    }
}
