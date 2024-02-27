using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TraceCV.Data;
using TraceCV.Models;
using System.IO;
using System;
using System.Collections.Generic;
using System.Collections;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TraceCV.Controllers
{
    public class ExpertController : Controller
    {
        private readonly DatabaseHandler _databaseHandler;
        public ExpertController(DatabaseHandler databaseHandler)
        {
            _databaseHandler = databaseHandler ?? throw new ArgumentNullException(nameof(databaseHandler));
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(Expert expert) 
        {
            try
            {
                if (string.IsNullOrEmpty(expert.Nationality))
                {
                    TempData["ErrorMessage"] = "Please select Nationality of the expert";
                }
                else if (expert.CvFile == null)
                {
                    TempData["ErrorMessage"] = "Please upload Expert Cv file";
                }
                else if (string.IsNullOrEmpty(expert.Speciality))
                {
                    TempData["ErrorMessage"] = "Please select area of speciality";
                }
                else
                {
                    if (ModelState.IsValid)
                    {

                        //handle file upload
                        if (expert.CvFile != null && expert.CvFile.Length > 0)
                        {
                            var cvFileName = $"{Guid.NewGuid().ToString()}-{Path.GetFileName(expert.CvFile.FileName)}";
                            var cvFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/cvs", cvFileName);
                            using (var stream = new FileStream(cvFilePath, FileMode.Create))
                            {
                                await expert.CvFile.CopyToAsync(stream);
                            }

                            // Save the file path to the user model
                            expert.CvFilePath = $"/cvs/{cvFileName}";
                        }
                        // Save the user data to the database
                        _databaseHandler.Experts.Add(expert);
                        await _databaseHandler.SaveChangesAsync();
                        // Provide a success message
                        TempData["SuccessMessage"] = "Expert saved successfully.";
                    }
                    else
                    {
                        LogValidationErrors();
                    }
                }
                    
            }
            catch (Exception ex)
            {
                // Log the exception (you can use a logging framework like Serilog or ILogger)
                Console.Error.WriteLine($"Error saving expert: {ex.Message}");

                // Provide an error message to the user
                TempData["ErrorMessage"] = $"An error occurred while saving the expert. Please try again. Error :{ex.Message}";

                return View(expert);
            }
            

            return View();
        }

        public async Task<IActionResult> ListOfExperts(string name, string nationality, string keysector,string gender,string experience,string levelofeducation,string area_of_speciality) 
        {
            try
            {
                var experts = _databaseHandler.Experts.Include(e => e.Educations).AsQueryable();
                if (!string.IsNullOrEmpty(name))
                {
                    experts = experts.Where(e => e.Name.Contains(name));
                }

                if (!string.IsNullOrEmpty(nationality))
                {
                    experts = experts.Where(e => e.Nationality.Contains(nationality));
                }

                if (!string.IsNullOrEmpty(gender))
                {
                    experts = experts.Where(e => e.Gender.Contains(gender));
                }

                if (!string.IsNullOrEmpty(keysector)) 
                {
                    experts = experts.Where(e => e.Sector.Contains(keysector));
                }

                if (!string.IsNullOrEmpty(experience) && int.TryParse(experience, out int minExperience))
                {
                    experts = experts.Where(e => Convert.ToInt32(e.experience) >= minExperience);
                }

                if (!string.IsNullOrEmpty(levelofeducation))
                {
                    //experts = experts.Where(e => e.Educations[0].Level.Contains(levelofeducation));
                    experts = experts.Where(e => e.Educations.Any(edu => edu.Level.Contains(levelofeducation)));
                }

                if (!string.IsNullOrEmpty(area_of_speciality))
                {
                    experts = experts.Where(e => e.Speciality.Contains(area_of_speciality));
                }


                var filteredexperts = experts.ToList();
                return View(filteredexperts);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred while retrieving experts. Error :{ex.Message}";
                return View();
            }
            
            
        }


        public IActionResult Edit(int id)
        {
            var expert = _databaseHandler.Experts
                .Include(e => e.Languages)
                .Include(e=>e.Educations)
                .Include(e => e.Contacts)
                .Include(e => e.Affiliations)
                .FirstOrDefault(e => e.Id == id); ;
            if (expert == null)
            {
                return NotFound();
            }

            ViewBag.GenderList = new SelectList(new List<string> { "Male", "Female" }, expert.Gender);
            return View(expert);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Expert expert)
        {
            if (id != expert.Id)
            {
                return NotFound();
            }
            if (string.IsNullOrEmpty(expert.Nationality))
            {
                // Provide an error message to the user
                TempData["ErrorMessage"] = "Nationality of expert not selected";
            }
            else if (expert.CvFile==null)
            {
                TempData["ErrorMessage"] = "Please Upload updated cv of the expert";
            }
            else
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        // Fetch the existing expert data including education details
                        var existingExpert = await _databaseHandler.Experts
                            .Include(e => e.Educations) // Include education details
                            .FirstOrDefaultAsync(e => e.Id == id);
                        if (existingExpert != null)
                        {
                            // Update the non-collection properties of the existing expert
                            _databaseHandler.Entry(existingExpert).CurrentValues.SetValues(expert);

                            existingExpert.Educations[0].Level = expert.Educations[0].Level;
                            // Save changes to the database
                            await _databaseHandler.SaveChangesAsync();

                            TempData["SuccessMessage"] = "Expert Details have been updated successfully.";
                        }
                        else
                        {
                            TempData["ErrorMessage"] = "Expert not found.";
                        }
                    }
                    catch (DbUpdateConcurrencyException ex)
                    {
                        // Handle concurrency exception if needed
                        TempData["ErrorMessage"] = $"Error Occurred : {ex.InnerException}";
                    }
                    TempData["SuccessMessage"] = "Expert Details have been updated successfully.";
                    //return RedirectToAction(nameof(ListOfExperts));
                }

            }

            return View(expert);
        }


        public async Task<IActionResult> Delete(int id)
        {
            var expert = await _databaseHandler.Experts.FindAsync(id);
            if (expert == null)
            {
                return NotFound();
            }

            _databaseHandler.Experts.Remove(expert);
            await _databaseHandler.SaveChangesAsync();

            return RedirectToAction(nameof(ListOfExperts));
        }


        private void LogValidationErrors()
        {
            foreach (var modelStateKey in ModelState.Keys)
            {
                var modelStateVal = ModelState[modelStateKey];
                foreach (var error in modelStateVal.Errors)
                {
                    // Log or output the validation error details
                    Console.Error.WriteLine($"Validation error in {modelStateKey}: {error.ErrorMessage}");
                }
            }
        }

        private void CheckDirectoryExists(string directory) 
        {
            if (!Directory.Exists(directory)) 
            {
                Directory.CreateDirectory(directory);
            }
        }
    }
}
