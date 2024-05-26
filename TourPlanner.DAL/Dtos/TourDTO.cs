namespace TourPlanner.DAL.Dtos
{
    public class TourDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string TransportType { get; set; }
        public float TourDistance { get; set; }
        public TimeSpan EstimatedTime { get; set; }
    }
}
