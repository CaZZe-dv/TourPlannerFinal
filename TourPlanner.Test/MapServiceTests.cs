using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using TourPlanner.BL.Services;

namespace TourPlanner.Test
{
    public class MapServiceTests
    {
        private MapService _mapService;
        private RouteService _routeService;
        private readonly string API_KEY = "5b3ce3597851110001cf6248c8088655b02f4a88b5aded527a91fa09";

        [SetUp]
        public void SetUp()
        {
            _mapService = new MapService();
            _routeService = new RouteService(API_KEY);
        }
        [Test]
        public async Task FetchMapFromLocations_Hollabrunn_Wien()
        {
            string start = await _routeService.GetGeocodeResponse("Hollabrunn");
            string end = await _routeService.GetGeocodeResponse("Wien");
            BitmapSource? image = await _mapService.GenerateMapAsync(start, end);
            Assert.IsNotNull(image);
        }

        [Test]
        public void ParseCoordinates_ValidString_ReturnsExpectedCoordinates()
        {
            string coordinatesStr = "2.3522,48.8566";
            var (latitude, longitude) = _mapService.ParseCoordinates(coordinatesStr);
            Assert.AreEqual(48.8566, latitude);
            Assert.AreEqual(2.3522, longitude);
        }

        [Test]
        public void ParseCoordinates_InvalidString_ThrowsArgumentException()
        {
            string coordinatesStr = "invalid,coordinates";
            Assert.Throws<ArgumentException>(() => _mapService.ParseCoordinates(coordinatesStr));
        }

        [Test]
        public void CalculateZoomLevel_SmallDistance_ReturnsHighZoomLevel()
        {
            double distance = 0.5;
            int zoomLevel = _mapService.CalculateZoomLevel(distance);
            Assert.AreEqual(15, zoomLevel);
        }

        [Test]
        public void CalculateZoomLevel_LargeDistance_ReturnsLowZoomLevel()
        {
            double distance = 1500;
            int zoomLevel = _mapService.CalculateZoomLevel(distance);
            Assert.AreEqual(6, zoomLevel);
        }
    }
}
