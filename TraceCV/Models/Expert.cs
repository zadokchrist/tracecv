using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace TraceCV.Models
{
    public class Expert
    {
        public Expert()
        {
            Contacts = new List<Contact> { new Contact() };
            Educations = new List<Education> { new Education() };
            Languages = new List<Language> { new Language() };
            Affiliations = new List<Affiliation> { new Affiliation() };

            OtherKeyExpertises = new List<OtherKeyExpertise>();
            Certificates = new List<Certificate>();

            SelectedOtherKeyExpertises = new List<string>();
            SelectedCertificates = new List<string>();
            SelectedAfricanCountries = new List<string>();
            AfricanCountriesWorked = new List<WorkedCountry>();
        }

        public int Id { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string DOB { get; set; }
        public string Speciality { get; set; }
        public string? CurrentEmployer { get; set; }
        public bool WorkedWith2ML { get; set; }

        public List<Contact> Contacts { get; set; }
        public List<Education> Educations { get; set; }
        public List<Language> Languages { get; set; }
        public List<Affiliation>? Affiliations { get; set; }

        public string Category { get; set; }
        public string Nationality { get; set; }
        public string experience { get; set; }
        public string lastedit { get; set; }
        public string Sector { get; set; }
        public string EmploymentStatus { get; set; }

        public List<OtherKeyExpertise>? OtherKeyExpertises { get; set; }
        public List<Certificate>? Certificates { get; set; }

        // New: persisted list of African countries worked in
        public List<WorkedCountry> AfricanCountriesWorked { get; set; }

        // NotMapped selections for multi-selects
        [NotMapped] public List<string> SelectedOtherKeyExpertises { get; set; }
        [NotMapped] public List<string> SelectedCertificates { get; set; }
        [NotMapped] public List<string> SelectedAfricanCountries { get; set; }

        [NotMapped] public IFormFile CvFile { get; set; }
        public string? CvFilePath { get; set; }
    }
}
