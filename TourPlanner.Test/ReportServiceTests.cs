using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.BL.Models;
using TourPlanner.DAL.Services;


namespace TourPlanner.Test
{
    internal class ReportServiceTests
    {
        private DbTourReportService _service;
        private List<Tour> _testTour;
        private List<TourLog> _testTourLogs;


        [SetUp]
        public void SetUp()
        {
            _service = new DbTourReportService();
            _testTour = new List<Tour>
            {
                new Tour(
                    Guid.NewGuid(),
                    "Tour 1",
                    "Description for Tour 1",
                    "From Location 1",
                    "To Location 1",
                    "Car",
                    100,
                    TimeSpan.FromHours(2),
                    null
                ),
                new Tour(
                    Guid.NewGuid(),
                    "Tour 2",
                    "Description for Tour 2",
                    "From Location 2",
                    "To Location 2",
                    "Bike",
                    150,
                    TimeSpan.FromHours(3),
                    null
                )
            };
            _testTourLogs = new List<TourLog>
            {
                new TourLog(
                    Guid.NewGuid(),
                    DateTime.Now,
                    "Good tour experience",
                    3.5f,
                    75,
                    TimeSpan.FromHours(2),
                    4,
                    Guid.NewGuid()
                ),
                new TourLog(
                    Guid.NewGuid(),
                    DateTime.Now.AddDays(-1),
                    "Nice scenery",
                    4.0f,
                    100,
                    TimeSpan.FromHours(3),
                    5,
                    Guid.NewGuid()
                )
            };
        }

        [Test]
        public async Task ReportFileExitsTour1()
        {
            string fileName = $"{_service.filePath}Report_{_testTour[0].Name.Replace(" ", "_")}.pdf";
            await _service.GenerateReportTest(_testTour[0]);

            Assert.IsTrue(File.Exists(fileName));
            File.Delete(fileName);
        }

        [Test]
        public async Task ReportFileExitsTour2()
        {
            string fileName = $"{_service.filePath}Report_{_testTour[1].Name.Replace(" ", "_")}.pdf";
            await _service.GenerateReportTest(_testTour[1]);

            Assert.IsTrue(File.Exists(fileName));
            File.Delete(fileName);
        }

        [Test]
        public async Task FileExistsAllTours()
        {
            string fileName = $"{_service.filePath}Report_All_Tours.pdf";
            await _service.GenerateReportTest(fileName);

            Assert.IsTrue(File.Exists(fileName));
            File.Delete(fileName);
        }

        [Test]
        public async Task GeneratePdfForTour_ShouldIncludeTourDetails()
        {
            string fileName = $"{_service.filePath}Report_{_testTour[0].Name.Replace(" ", "_")}.pdf";
            await _service.GenerateReportTest(_testTour[0]);

            Assert.IsTrue(File.Exists(fileName));
            File.Delete(fileName);
        }

        [Test]
        public async Task GeneratePdfForAllTours_ShouldIncludeAllTourDetails()
        {
            string fileName = $"{_service.filePath}Report_All_Tours.pdf";
            await _service.GenerateReportTest(fileName);

            Assert.IsTrue(File.Exists(fileName));
            File.Delete(fileName);
        }
    }
}
