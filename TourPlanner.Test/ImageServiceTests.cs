using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using TourPlanner.BL.Services;

namespace TourPlanner.Test
{
    public class ImageServiceTests
    {
        private readonly string testDirectory = "C:/Users/matth/OneDrive/Bilder/TourPlannerImagesTest";
        private ImageService imageService;
        [SetUp]
        public void SetUp()
        {
            imageService = new ImageService(testDirectory);
        }
        //Check if test image can be safed in test directory
        [Test]
        public async Task SaveImage_FileExists()
        {
            var bitmapSource = new BitmapImage(new Uri("C:/Users/matth/OneDrive/Bilder/dd464677-0759-469a-8ccc-447b09dcff78.jpg"));
            var filePath = await imageService.SaveImage(bitmapSource, "dd464677-0759-469a-8ccc-447b09dcff78");
            Assert.IsTrue(File.Exists(filePath));
        }
        //Check if throws Exception, when trying to load image that doesnt exist
        [Test]
        public async Task GetImage_FileNotExists()
        {
            Assert.ThrowsAsync<FileNotFoundException>(async () => await imageService.GetImage("nonExistentImage"));
        }
        //Check if removing of test image in test directory works
        [Test]
        public async Task RemoveImage_FileExists()
        {
            var bitmapSource = new BitmapImage(new Uri("C:/Users/matth/OneDrive/Bilder/dd464677-0759-469a-8ccc-447b09dcff78.jpg"));
            var tourId = "dd464677-0759-469a-8ccc-447b09dcff78";
            var filePath = await imageService.SaveImage(bitmapSource, tourId);
            await imageService.RemoveImage(tourId);
            Assert.IsFalse(File.Exists(filePath));
        }
    }
}
