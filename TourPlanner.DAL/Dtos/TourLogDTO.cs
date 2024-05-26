namespace TourPlanner.DAL.Dtos
{
    public class TourLogDTO
    {
        public Guid Id { get; set; }
        public string DateTime { get; set; }
        public string Comment { get; set; }
        public float Difficulty { get; set; }
        public float TotalDistance { get; set; }
        public TimeSpan TotalTime { get; set; }
        public float Rating { get; set; }
        public Guid TourId { get; set; }
    }
}
