namespace TourPlanner.BL.Models
{
    public class TourLog
    {
        public Guid Id { get; }
        public DateTime DateTime { get; }
        public string Comment { get; }
        public float Difficulty { get; }
        public float TotalDistance { get; }
        public TimeSpan TotalTime { get; }
        public float Rating { get; }
        public Guid TourId { get; }

        public TourLog(DateTime dateTime, string comment, float difficulty, float totalDistance, TimeSpan totalTime, float rating, Guid tourId)
        {
            DateTime = dateTime;
            Comment = comment;
            Difficulty = difficulty;
            TotalDistance = totalDistance;
            TotalTime = totalTime;
            Rating = rating;
            TourId = tourId;
        }
        public TourLog(Guid id, DateTime dateTime, string comment, float difficulty, float totalDistance, TimeSpan totalTime, float rating, Guid tourId) :
            this(dateTime, comment, difficulty, totalDistance, totalTime, rating, tourId)
        {
            Id = id;
        }
    }
}