namespace TraceCV.Models
{
    public class OtherKeyExpertise
    {
        public int Id { get; set; }
        public string Expertise { get; set; }
        public int ExpertId { get; set; } // Foreign key to Expert
    }
}