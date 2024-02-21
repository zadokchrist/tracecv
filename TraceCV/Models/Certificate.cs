namespace TraceCV.Models
{
    public class Certificate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ExpertId { get; set; } // Foreign key to Expert
    }
}
