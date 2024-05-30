using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;


namespace TourPlanner.BL.Services
{
    public class ImageService
    {
        private readonly string _imageDirectory;

        public ImageService(string imageDirectory)
        {
            _imageDirectory = imageDirectory;

            // Ensure the directory exists
            if (!Directory.Exists(_imageDirectory))
            {
                Directory.CreateDirectory(_imageDirectory);
            }
        }

        private byte[] BitmapImageToByteArray(BitmapImage bitmapImage)
        {
            byte[] data;
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                data = ms.ToArray();
            }
            return data;
        }


        public string SaveImage(BitmapImage bitmapImage, string tourId)
        {
            // Convert BitmapImage to byte[]
            byte[] imageData = BitmapImageToByteArray(bitmapImage);

            // Generate a unique filename
            string fileName = $"{tourId}.jpg";
            string filePath = Path.Combine(_imageDirectory, fileName);

            // Save the image data to the file
            File.WriteAllBytes(filePath, imageData);

            return filePath;
        }

        public string? GetImage(string tourId)
        {
            string fileName = $"{tourId}.jpg";
            string filePath = Path.Combine(_imageDirectory, fileName);

            if (!File.Exists(filePath))
            {
                return null; // Or return a default image
            }

            return filePath;
        }

        public BitmapImage ByteArrayToBitmapImage(byte[] byteArray)
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
