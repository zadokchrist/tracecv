namespace TraceCV.Models
{
    public class WorkedCountry
    {
        public int Id { get; set; }
        public string CountryIso2 { get; set; } = string.Empty;
        public string? CountryName { get; set; }

        public int ExpertId { get; set; } // FK to Expert
    }
}