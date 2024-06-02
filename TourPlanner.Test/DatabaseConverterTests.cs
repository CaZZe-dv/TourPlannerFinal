using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.BL.Models;
using TourPlanner.DAL.Dtos;
using TourPlanner.DAL.Services;

namespace TourPlanner.Test
{
    public class DatabaseConverterTests : DatabaseConverter
    {
        private TourDTO testTourDTO;
        private TourLogDTO testTourLogDTO;
        private Tour testTour;
        private TourLog testTourLog;
        [SetUp]
        public void SetUp()
        {
            testTourDTO = new TourDTO
            {
                Id = new Guid(),
                Name = "Test Tour",
                Description = "Test Description",
                From = "Start Location",
                To = "End Location",
                TransportType = "Walking",
                TourDistance = 10.5f,
                EstimatedTime = new TimeSpan(1, 30, 0)
            };
            testTourLogDTO = new TourLogDTO
            {
                Id = new Guid(),
                DateTime = "2023-01-01T12:00:00",
                Comment = "Great Tour",
                Difficulty = 3,
                TotalDistance = 10.5f,
                TotalTime = new TimeSpan(1, 30, 0),
                Rating = 5,
                TourId = new Guid()
            };
            testTour = new Tour(new Guid(), "Test Tour", "Test Description", "Start Location", "End Location",
                "Walking", 10.5f, new TimeSpan(1, 30, 0), null);
            testTourLog = new TourLog(new Guid(), new DateTime(2023, 1, 1, 12, 0, 0), "Great Tour", 3,
                10.5f, new TimeSpan(1, 30, 0), 5, new Guid());
        }
        [Test]
        public void ToTour_ConvertsCorrectly()
        {
            var result = ToTour(testTourDTO);
            Assert.AreEqual(testTourDTO.Id, result.Id);
            Assert.AreEqual(testTourDTO.Name, result.Name);
            Assert.AreEqual(testTourDTO.Description, result.Description);
            Assert.AreEqual(testTourDTO.From, result.From);
            Assert.AreEqual(testTourDTO.To, result.To);
            Assert.AreEqual(testTourDTO.TransportType, result.TransportType);
            Assert.AreEqual(testTourDTO.TourDistance, result.TourDistance);
            Assert.AreEqual(testTourDTO.EstimatedTime, result.EstimatedTime);
        }

        [Test]
        public void ToTourLog_ConvertsCorrectly()
        {
            var result = ToTourLog(testTourLogDTO);
            Assert.AreEqual(testTourLogDTO.Id, result.Id);
            Assert.AreEqual(DateTime.Parse(testTourLogDTO.DateTime), result.DateTime);
            Assert.AreEqual(testTourLogDTO.Comment, result.Comment);
            Assert.AreEqual(testTourLogDTO.Difficulty, result.Difficulty);
            Assert.AreEqual(testTourLogDTO.TotalDistance, result.TotalDistance);
            Assert.AreEqual(testTourLogDTO.TotalTime, result.TotalTime);
            Assert.AreEqual(testTourLogDTO.Rating, result.Rating);
            Assert.AreEqual(testTourLogDTO.TourId, result.TourId);
        }

        [Test]
        public void ToTourDTO_ConvertsCorrectly()
        {
            var result = ToTourDTO(testTour);
            Assert.AreEqual(testTour.Name, result.Name);
            Assert.AreEqual(testTour.Description, result.Description);
            Assert.AreEqual(testTour.From, result.From);
            Assert.AreEqual(testTour.To, result.To);
            Assert.AreEqual(testTour.TransportType, result.TransportType);
            Assert.AreEqual(testTour.TourDistance, result.TourDistance);
            Assert.AreEqual(testTour.EstimatedTime, result.EstimatedTime);
        }

        [Test]
        public void ToTourLogDTO_ConvertsCorrectly()
        {
            var result = ToTourLogDTO(testTourLog);
            Assert.AreEqual(testTourLog.DateTime.ToString(), result.DateTime);
            Assert.AreEqual(testTourLog.Comment, result.Comment);
            Assert.AreEqual(testTourLog.Difficulty, result.Difficulty);
            Assert.AreEqual(testTourLog.TotalDistance, result.TotalDistance);
            Assert.AreEqual(testTourLog.TotalTime, result.TotalTime);
            Assert.AreEqual(testTourLog.Rating, result.Rating);
            Assert.AreEqual(testTourLog.TourId, result.TourId);
        }
    }
}
