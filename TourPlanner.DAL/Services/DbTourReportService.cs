using iText.Kernel.Pdf;
using iText.Layout.Element;
using iText.Layout;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.BL.Models;

namespace TourPlanner.DAL.Services
{
    public class DbTourReportService
    {

        private readonly TourPlannerRepository tourPlannerRepository;
        private readonly TourPlannerRepository _tourPlannerRepository;




        private string filePath = "C:\\Users\\nicib\\Documents\\FHTW\\4\\swen\\";

        public DbTourReportService()
        {
            //_tourPlannerRepository = new TourPlannerRepository(tourLogHandler);
        }


        public async Task<string> GenerateReportForTour(Tour tour, TourPlannerRepository tourPlanner)
        {
            string fileName = $"{filePath}Report_{tour.Name.Replace(" ", "_")}.pdf";
            await GeneratePdfForTour(tour, fileName, tourPlanner);
            return fileName;
        }
        public async Task<string> GenerateReportTest(Tour tour)
        {
            string fileName = $"{filePath}Report_{tour.Name.Replace(" ", "_")}.pdf";
            Debug.WriteLine(fileName);
            await GeneratePdfTest(tour, fileName);
            return fileName;
        }

        public async Task<string> GenerateReportForAllTours(TourPlannerRepository tourPlanner)
        {
            string fileName = $"{filePath}Report_All_Tours.pdf";
            await GeneratePdfForAllTours(fileName, tourPlanner);
            return fileName;
        }

        private async Task GeneratePdfForTour(Tour tour, string fileName, TourPlannerRepository tourPlanner)
        {

            PdfWriter writer = new PdfWriter(fileName);
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf);

            document.Add(new Paragraph($"Report for Tour: {tour.Name}").SetBold().SetFontSize(20));
            document.Add(new Paragraph($"Description: {tour.Description}"));
            document.Add(new Paragraph($"Distance: {tour.TourDistance} km"));
            document.Add(new Paragraph($"Estimated Time: {tour.EstimatedTime} hours"));
            document.Add(new Paragraph($"Transport Type: {tour.TransportType}"));

            //var tourLogs = GetSampleTourLogs();
            var tourLogs = await tourPlanner.GetAllTourLogs(tour);
            if (tourLogs != null)
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
        private async Task GeneratePdfTest(Tour tour, string fileName)
        {

            PdfWriter writer = new PdfWriter(fileName);
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf);

            document.Add(new Paragraph("Report for Tour"));

            document.Close();
            Debug.WriteLine("testpdf");

        }

        private async Task GeneratePdfForAllTours(string fileName, TourPlannerRepository tourPlanner)
        {
            PdfWriter writer = new PdfWriter(fileName);
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf);
            document.Add(new Paragraph("Report for All Tours").SetBold().SetFontSize(20));

            var tours = await tourPlanner.GetAllTours();
            foreach (var tour in tours)
            {
                document.Add(new Paragraph($"Tour: {tour.Name}").SetBold().SetFontSize(15));
                document.Add(new Paragraph($"Description: {tour.Description}"));
                document.Add(new Paragraph($"Distance: {tour.TourDistance} km"));
                document.Add(new Paragraph($"Estimated Time: {tour.EstimatedTime} hours"));
                document.Add(new Paragraph($"Transport Type: {tour.TransportType}"));

                var tourLogs = await tourPlanner.GetAllTourLogs(tour);
                if (tourLogs.Any())
                {
                    double averageTime = tourLogs.Average(log => log.TotalTime.TotalHours);
                    double averageDistance = tourLogs.Average(log => log.TotalDistance);
                    double averageRating = tourLogs.Average(log => log.Rating);

                    document.Add(new Paragraph($"Average Time: {averageTime:F2} hours"));
                    document.Add(new Paragraph($"Average Distance: {averageDistance:F2} km"));
                    document.Add(new Paragraph($"Average Rating: {averageRating:F2}"));
                }

                document.Add(new Paragraph("\n--------------------------------------------------\n\n"));
            }

            document.Close();
            Debug.WriteLine("alle");
        }


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
