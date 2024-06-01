using System.Windows.Media.Imaging;
using TourPlanner.BL.Services;

namespace TourPlanner.Test
{
    public class Tests
    {
        private MapService mapService;
        private readonly string testDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestImages");
        [SetUp]
        public void Setup()
        {
            mapService = new MapService();
        }

        [Test]
        public async Task SaveImage_FileExists()
        {
            // Arrange
            var imageService = new ImageService(testDirectory);
            var bitmapSource = new BitmapImage(new Uri("pack://application:,,,/TestImages/testImage.jpg"));

            // Act
            var filePath = await imageService.SaveImage(bitmapSource, "testImage");

            // Assert
            Assert.IsTrue(File.Exists(filePath));
        }

        [Test]
        public async Task GetImage_FileNotExists()
        {
            // Arrange
            var imageService = new ImageService(testDirectory);

            // Act & Assert
            Assert.ThrowsAsync<FileNotFoundException>(async () => await imageService.GetImage("nonExistentImage"));
        }

        [Test]
        public async Task RemoveImage_FileExists()
        {
            // Arrange
            var imageService = new ImageService(testDirectory);
            var bitmapSource = new BitmapImage(new Uri("pack://application:,,,/TestImages/testImage.jpg"));
            var tourId = "testImage";
            var filePath = await imageService.SaveImage(bitmapSource, tourId);

            // Act
            await imageService.RemoveImage(tourId);

            // Assert
            Assert.IsFalse(File.Exists(filePath));
        }
        [Test]
        public async Task DownloadMapTiles_ValidInput_ReturnsBitmap()
        {
            // Arrange
            var start = (latitude: 52.52, longitude: 13.4050); // Berlin
            var end = (latitude: 48.8566, longitude: 2.3522); // Paris
            int zoom = 10;

            // Act
            var bitmap = await mapService.DownloadMapTiles(start, end, zoom);

            // Assert
            Assert.IsNotNull(bitmap);
            Assert.AreEqual(256, bitmap.Width); // Assuming standard tile size
            Assert.AreEqual(256, bitmap.Height); // Assuming standard tile size
        }

        [Test]
        public void LatLongToTile_ValidInput_ReturnsCoordinates()
        {
            // Arrange
            double latitude = 52.52; // Berlin
            double longitude = 13.4050; // Berlin
            int zoom = 10;

            // Act
            var (xTile, yTile) = mapService.LatLongToTile(latitude, longitude, zoom);

            // Assert
            Assert.AreEqual(550, xTile);
            Assert.AreEqual(338, yTile);
        }

        [Test]
        public void LatLongToPixel_ValidInput_ReturnsCoordinates()
        {
            // Arrange
            double latitude = 52.52; // Berlin
            double longitude = 13.4050; // Berlin
            int zoom = 10;

            // Act
            var (xPixel, yPixel) = mapService.LatLongToPixel(latitude, longitude, zoom);

            // Assert
            Assert.AreEqual(141934, xPixel);
            Assert.AreEqual(242101, yPixel);
        }

        [Test]
        public async Task GenerateMapAsync_ValidInput_ReturnsBitmapSource()
        {
            // Arrange
            string start = "52.52,13.4050"; // Berlin
            string end = "48.8566,2.3522"; // Paris

            // Act
            var bitmapSource = await mapService.GenerateMapAsync(start, end);

            // Assert
            Assert.IsNotNull(bitmapSource);
        }
    }
}