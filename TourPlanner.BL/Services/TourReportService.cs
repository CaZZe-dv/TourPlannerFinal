﻿using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using TourPlanner.BL.Models;


namespace TourPlanner.BL.Services
{
    public class TourReportService
    {

        
        public async Task<string> GenerateReportForTour(Tour tour)
        {
            string fileName = $"Report_{tour.Name.Replace(" ", "_")}.pdf";
            await GeneratePdfForTour(tour, fileName);
            return fileName;
        }

        public async Task<string> GenerateReportForAllTours()
        {
            string fileName = "Report_All_Tours.pdf";
            await GeneratePdfForAllTours(fileName);
            return fileName;
        }

        private async Task GeneratePdfForTour(Tour tour, string fileName)
        {
            using (var stream = new FileStream(fileName, FileMode.Create))
            {
                var writer = new PdfWriter(stream);
                var pdf = new PdfDocument(writer);
                var document = new Document(pdf);

                document.Add(new Paragraph($"Report for Tour: {tour.Name}").SetBold().SetFontSize(20));
                document.Add(new Paragraph($"Description: {tour.Description}"));
                document.Add(new Paragraph($"Distance: {tour.TourDistance} km"));
                document.Add(new Paragraph($"Estimated Time: {tour.EstimatedTime} hours"));
                document.Add(new Paragraph($"Transport Type: {tour.TransportType}"));

                // Simulate tour logs data (you can replace this with actual data later)
                var tourLogs = GetSampleTourLogs();
                if (tourLogs.Count > 0)
                {
                    document.Add(new Paragraph("\nTour Logs:").SetBold().SetFontSize(15));
                    foreach (var log in tourLogs)
                    {
                        document.Add(new Paragraph($"Date: {log.DateTime}, Duration: {log.TotalTime}, Rating: {log.Rating}, Comment: {log.Comment}"));
                    }
                }

                document.Close();
                Debug.WriteLine("einzeln");
            }
        }

        private async Task GeneratePdfForAllTours(string fileName)
        {
            using (var stream = new FileStream(fileName, FileMode.Create))
            {
                var writer = new PdfWriter(stream);
                var pdf = new PdfDocument(writer);
                var document = new Document(pdf);

                document.Add(new Paragraph("Report for All Tours").SetBold().SetFontSize(20));

                // Simulate tour data (you can replace this with actual data later)
                var tours = GetSampleTours();
                foreach (var tour in tours)
                {
                    document.Add(new Paragraph($"Tour: {tour.Name}").SetBold().SetFontSize(15));
                    document.Add(new Paragraph($"Description: {tour.Description}"));
                    document.Add(new Paragraph($"Distance: {tour.TourDistance} km"));
                    document.Add(new Paragraph($"Estimated Time: {tour.EstimatedTime} hours"));
                    document.Add(new Paragraph($"Transport Type: {tour.TransportType}"));

                    // Simulate tour logs data (you can replace this with actual data later)
                    var tourLogs = GetSampleTourLogs();
                    if (tourLogs.Count > 0)
                    {
                        document.Add(new Paragraph("Tour Logs:").SetBold());
                        foreach (var log in tourLogs)
                        {
                            document.Add(new Paragraph($"Date: {log.DateTime}, Duration: {log.TotalTime}, Rating: {log.Rating}, Comment: {log.Comment}"));
                        }
                    }

                    document.Add(new Paragraph("\n--------------------------------------------------\n\n"));
                }

                document.Close();
                Debug.WriteLine("alle");
            }
        }

        private List<Tour> GetSampleTours()
        {
            return new List<Tour>
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
        }

        // Simulated sample tour logs data
        private List<TourLog> GetSampleTourLogs()
        {
            return new List<TourLog>
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
    }
}