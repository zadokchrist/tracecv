﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations.Schema;

namespace TraceCV.Models
{
    public class Expert
    {
        public int Id { get; set; }
        // Other user properties
        public string Name { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string DOB { get; set; }
        public string Speciality { get; set; }
        public string CurrentEmployer { get; set; }
        public bool WorkedWith2ML {  get; set; }
        public List<Contact> Contacts { get; set; }
        public List<Education> Educations { get; set; }

        public string Category { get; set; }

        public List<Language> Languages { get; set; }

        [BindProperty]
        [NotMapped]
        public IFormFile CvFile { get; set; }

        
        public string CvFilePath { get; set; }
        // Other navigation properties
    }
}