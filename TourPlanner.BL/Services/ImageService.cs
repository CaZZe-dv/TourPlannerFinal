using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using TourPlanner.Utility.Logging;

namespace TourPlanner.BL.Services
{
    public class ImageService : IImageService
    {
        private readonly string _imageDirectory;
        private static readonly ILoggerWrapper logger = Utility.Logging.LoggerFactory.GetLogger();

        public ImageService(string imageDirectory)
        {
            _imageDirectory = imageDirectory;

            // Ensure the directory exists
            if (!Directory.Exists(_imageDirectory))
            {
                //Directory.CreateDirectory(_imageDirectory);
            }
        }

        private byte[] BitmapSourceToByteArray(BitmapSource bitmapSource)
        {
            byte[] data;
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmapSource));
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                data = ms.ToArray();
            }
            return data;
        }

        public async Task RemoveImage(string tourId)
        {
            string fileName = $"{tourId}.jpg";
            string filePath = Path.Combine(_imageDirectory, fileName);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                logger.Info($"Image removed successfully: {filePath}");
            }
            else
            {
                logger.Warn($"Image not found: {filePath}");
                throw new FileNotFoundException("Image not found.", filePath);
            }
        }

        public async Task<string> SaveImage(BitmapSource bitmapSource, string tourId)
        {
            // Convert BitmapSource to byte[]
            byte[] imageData = BitmapSourceToByteArray(bitmapSource);

            // Generate a unique filename
            string fileName = $"{tourId}.jpg";
            string filePath = Path.Combine(_imageDirectory, fileName);

            // Save the image data to the file
            File.WriteAllBytes(filePath, imageData);
            logger.Info($"Image saved successfully: {filePath}");

            return filePath;
        }

        public async Task<BitmapSource> GetImage(string tourId)
        {
            string fileName = $"{tourId}.jpg";
            string filePath = Path.Combine(_imageDirectory, fileName);

            if (!File.Exists(filePath))
            {
                logger.Warn($"Image not found: {filePath}");
                throw new FileNotFoundException("Image not found.", filePath);
            }

            // Read the image data from the file
            byte[] imageData = File.ReadAllBytes(filePath);
            logger.Info($"Image retrieved successfully: {filePath}");

            // Convert byte[] to BitmapSource
            return ByteArrayToBitmapSource(imageData);
        }

        public BitmapSource ByteArrayToBitmapSource(byte[] byteArray)
        {
            using (var stream = new MemoryStream(byteArray))
            {
                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.StreamSource = stream;
                bitmapImage.EndInit();
                bitmapImage.Freeze(); // Optional: Freeze to make it cross-thread accessible
                return bitmapImage;
            }
        }
    }
}
