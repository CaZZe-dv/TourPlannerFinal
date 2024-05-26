using System.Drawing;

namespace TourPlanner.BL.Models
{
    public class Tour
    {
        public Guid Id { get; }
        public string Name { get; }
        public string Description { get; }
        public string From { get; }
        public string To { get; }
        public string TransportType { get; }
        //Information retreived by API will be implemented later on for now
        //Default values are initialised
        public float TourDistance { get; }
        public TimeSpan EstimatedTime { get; }
        public Bitmap RouteInformation { get; }

        public readonly List<TourLog> TourLogs;


        public Tour(Guid id, string name, string description, string from, string to,
            string transportType, float tourDistance,
            TimeSpan estimatedTime, Bitmap routeInformation, List<TourLog> tourLogs) : this(id, name, description, from, to, transportType, tourDistance, estimatedTime, routeInformation)
        {
            TourLogs = tourLogs;
        }
        public Tour(Guid id, string name, string description, string from, string to,
            string transportType, float tourDistance,
            TimeSpan estimatedTime, Bitmap routeInformation) : this(name, description, from, to, transportType, tourDistance, estimatedTime, routeInformation)
        {
            Id = id;
        }
        public Tour(string name, string description, string from, string to,
            string transportType, float tourDistance,
            TimeSpan estimatedTime, Bitmap routeInformation)
        {
            Name = name;
            Description = description;
            From = from;
            To = to;
            TransportType = transportType;
            TourDistance = tourDistance;
            EstimatedTime = estimatedTime;
            RouteInformation = routeInformation;
            TourLogs = new List<TourLog>();
        }
    }
}