using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        [Test]
        public void GetDirectionsURL_44_44_44_44()
        {
            Assert.AreEqual(_routeService.GetDirectionsURL("Walking", "44,44", "44,44"),
                "https://api.openrouteservice.org/v2/directions/foot-walking?api_key=5b3ce3597851110001cf6248c8088655b02f4a88b5aded527a91fa09&start=44,44&end=44,44");
        }
        [Test]
        public void GetGecodeURL_Hollabrunn()
        {
            Assert.AreEqual(_routeService.GetGeocodeURL("Hollabrunn"),
                "https://api.openrouteservice.org/geocode/search?api_key=5b3ce3597851110001cf6248c8088655b02f4a88b5aded527a91fa09&text=Hollabrunn");
        }
    }
}
