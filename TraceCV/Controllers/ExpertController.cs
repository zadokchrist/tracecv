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

        public async Task<IActionResult> ListOfExperts(string name, string nationality, string keysector,string gender,string experience ) 
        {
            try
            {
                var experts = _databaseHandler.Experts.AsQueryable();
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

                if (!string.IsNullOrEmpty(experience))
                {
                    experts = experts.Where(e => int.Parse(e.experience) >= int.Parse(experience));
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
                .Include(e => e.Contacts)
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

            if (ModelState.IsValid)
            {
                try
                {
                    // Update the expert data
                    _databaseHandler.Entry(expert).State = EntityState.Modified;
                    await _databaseHandler.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    // Handle concurrency exception if needed
                    throw;
                }
                return RedirectToAction(nameof(List<Expert>));
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
