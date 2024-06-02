using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;

namespace TourPlanner.BL.Services
{
    public class MapService : IMapService
    {
        private const string TileUrl = "https://tile.openstreetmap.org/{0}/{1}/{2}.png";
        private const int TileSize = 256;
        private readonly HttpClient httpClient;

        public MapService()
        {
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("User-Agent", "CarSharingTest/1.0 Demo application for the SAM-course");
        }

        public async Task<Bitmap> DownloadMapTiles((double latitude, double longitude) start, (double latitude, double longitude) end, int zoom)
        {
            int xStart, yStart, xEnd, yEnd;
            (xStart, yStart) = LatLongToTile(start.latitude, start.longitude, zoom);
            (xEnd, yEnd) = LatLongToTile(end.latitude, end.longitude, zoom);

            int minX = Math.Min(xStart, xEnd);
            int maxX = Math.Max(xStart, xEnd);
            int minY = Math.Min(yStart, yEnd);
            int maxY = Math.Max(yStart, yEnd);

            int width = (maxX - minX + 1) * TileSize;
            int height = (maxY - minY + 1) * TileSize;

            Bitmap mapBitmap = new Bitmap(width, height);

            for (int x = minX; x <= maxX; x++)
            {
                for (int y = minY; y <= maxY; y++)
                {
                    string url = string.Format(TileUrl, zoom, x, y);
                    using (HttpResponseMessage response = await httpClient.GetAsync(url))
                    using (Stream stream = await response.Content.ReadAsStreamAsync())
                    using (Bitmap tileBitmap = new Bitmap(stream))
                    {
                        using (Graphics g = Graphics.FromImage(mapBitmap))
                        {
                            int offsetX = (x - minX) * TileSize;
                            int offsetY = (y - minY) * TileSize;
                            g.DrawImage(tileBitmap, offsetX, offsetY, TileSize, TileSize);
                        }
                    }
                }
            }

            return mapBitmap;
        }

        public void DrawMarker(Graphics g, double latitude, double longitude, System.Drawing.Color color, int zoom, int minX, int minY)
        {
            const int markerSize = 10;
            var (x, y) = LatLongToPixel(latitude, longitude, zoom);
            int pixelX = x - minX * TileSize;
            int pixelY = y - minY * TileSize;

            using (System.Drawing.Brush brush = new SolidBrush(color))
            {
                g.FillEllipse(brush, pixelX - markerSize / 2, pixelY - markerSize / 2, markerSize, markerSize);
            }
        }

        public void DrawRoute(Graphics g, (double latitude, double longitude) start, (double latitude, double longitude) end, System.Drawing.Color color, int zoom, int minX, int minY)
        {
            var (xStart, yStart) = LatLongToPixel(start.latitude, start.longitude, zoom);
            var (xEnd, yEnd) = LatLongToPixel(end.latitude, end.longitude, zoom);

            int pixelXStart = xStart - minX * TileSize;
            int pixelYStart = yStart - minY * TileSize;
            int pixelXEnd = xEnd - minX * TileSize;
            int pixelYEnd = yEnd - minY * TileSize;

            using (System.Drawing.Pen pen = new System.Drawing.Pen(color, 2))
            {
                g.DrawLine(pen, pixelXStart, pixelYStart, pixelXEnd, pixelYEnd);
            }
        }

        public (int, int) LatLongToTile(double latitude, double longitude, int zoom)
        {
            double latRad = Math.PI * latitude / 180.0;
            double n = Math.Pow(2, zoom);
            int xTile = (int)Math.Floor((longitude + 180.0) / 360.0 * n);
            int yTile = (int)Math.Floor((1.0 - Math.Log(Math.Tan(latRad) + (1 / Math.Cos(latRad))) / Math.PI) / 2.0 * n);
            return (xTile, yTile);
        }

        public (int, int) LatLongToPixel(double latitude, double longitude, int zoom)
        {
            double latRad = Math.PI * latitude / 180.0;
            double n = Math.Pow(2, zoom) * TileSize;
            int xPixel = (int)Math.Floor((longitude + 180.0) / 360.0 * n);
            int yPixel = (int)Math.Floor((1.0 - Math.Log(Math.Tan(latRad) + (1 / Math.Cos(latRad))) / Math.PI) / 2.0 * n);
            return (xPixel, yPixel);
        }

        public double HaversineDistance((double latitude, double longitude) point1, (double latitude, double longitude) point2)
        {
            const double R = 6371; // Radius of the Earth in km
            double dLat = (point2.latitude - point1.latitude) * Math.PI / 180.0;
            double dLon = (point2.longitude - point1.longitude) * Math.PI / 180.0;

            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                       Math.Cos(point1.latitude * Math.PI / 180.0) * Math.Cos(point2.latitude * Math.PI / 180.0) *
                       Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double distance = R * c;

            return distance;
        }

        public int CalculateZoomLevel(double distance)
        {
            if (distance < 1) return 15; // For distances less than 1 km
            else if (distance < 5) return 14;
            else if (distance < 10) return 13;
            else if (distance < 20) return 12;
            else if (distance < 50) return 11;
            else if (distance < 100) return 10;
            else if (distance < 200) return 9;
            else if (distance < 500) return 8;
            else if (distance < 1000) return 7;
            else return 6; // For distances more than 1000 km
        }

        public async Task<BitmapSource?> GenerateMapAsync(string startStr, string endStr)
        {
            var startCoords = ParseCoordinates(startStr);
            var endCoords = ParseCoordinates(endStr);

            double distance = HaversineDistance(startCoords, endCoords);
            int zoom = CalculateZoomLevel(distance);

            // Download tiles and draw route
            Bitmap mapBitmap = await DownloadMapTiles(startCoords, endCoords, zoom);
            using (Graphics g = Graphics.FromImage(mapBitmap))
            {
                (int xStart, int yStart) = LatLongToTile(startCoords.latitude, startCoords.longitude, zoom);
                (int xEnd, int yEnd) = LatLongToTile(endCoords.latitude, endCoords.longitude, zoom);
                int minX = Math.Min(xStart, xEnd);
                int minY = Math.Min(yStart, yEnd);

                DrawMarker(g, startCoords.latitude, startCoords.longitude, System.Drawing.Color.Red, zoom, minX, minY);
                DrawMarker(g, endCoords.latitude, endCoords.longitude, System.Drawing.Color.Green, zoom, minX, minY);
                DrawRoute(g, startCoords, endCoords, System.Drawing.Color.Blue, zoom, minX, minY);
            }
            return ConvertBitmapToBitmapSource(mapBitmap);
        }

        public (double latitude, double longitude) ParseCoordinates(string coordinatesStr)
        {
            var parts = coordinatesStr.Split(',');
            if (parts.Length != 2)
                throw new ArgumentException("Invalid coordinates format. Expected format: 'latitude,longitude'.");

            if (double.TryParse(parts[0], NumberStyles.Float, CultureInfo.InvariantCulture, out double longitude) &&
                double.TryParse(parts[1], NumberStyles.Float, CultureInfo.InvariantCulture, out double latitude))
            {
                return (latitude, longitude);
            }
            else
            {
                throw new ArgumentException("Invalid coordinates values.");
            }
        }

        private BitmapSource ConvertBitmapToBitmapSource(Bitmap bitmap)
        {
            using (var memoryStream = new MemoryStream())
            {
                bitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
                memoryStream.Seek(0, SeekOrigin.Begin);

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.StreamSource = memoryStream;
                bitmapImage.EndInit();
                bitmapImage.Freeze();

                return bitmapImage;
            }
        }
    }
}