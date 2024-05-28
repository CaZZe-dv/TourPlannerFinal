using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public string SaveImage(byte[] imageData, string tourId)
        {
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
    }
}
