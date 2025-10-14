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

            OtherKeyExpertises = new List<OtherKeyExpertise>(); // mapped from SelectedOtherKeyExpertises
            Certificates = new List<Certificate>();             // mapped from SelectedCertificates
            SelectedOtherKeyExpertises = new List<string>();
            SelectedCertificates = new List<string>();
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

        [NotMapped]
        public List<string> SelectedOtherKeyExpertises { get; set; }

        [NotMapped]
        public List<string> SelectedCertificates { get; set; }

        [NotMapped]
        public IFormFile CvFile { get; set; }
        public string? CvFilePath { get; set; }
    }
}
