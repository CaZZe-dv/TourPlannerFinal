using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.BL.Services;

namespace TourPlanner.Test
{
    public class RouteServiceTests
    {
        private RouteService _routeService;
        private readonly string API_KEY = "5b3ce3597851110001cf6248c8088655b02f4a88b5aded527a91fa09";
        [SetUp]
        public void SetUp()
        {
            _routeService = new RouteService(API_KEY);
        }
        [Test]
        public async Task GetGeoCodeResponse_Hollabrunn()
        {
            string longlat = await _routeService.GetGeocodeResponse("Hollabrunn");
            Assert.AreEqual(longlat, "16.079845,48.562406");
        }
        [Test]
        public async Task GetGeoCodeResponse_Wien()
        {
            string longlat = await _routeService.GetGeocodeResponse("Wien");
            Assert.AreEqual(longlat, "16.348388,48.198674");
        }
        [Test]
        public async Task GetGeoCodeResponse_Failed()
        {
            string? longlat = await _routeService.GetGeocodeResponse("Wiennnnn123");
            Assert.IsNull(longlat);
        }
    }
}
