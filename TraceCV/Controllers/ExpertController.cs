using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TraceCV.Data;
using TraceCV.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using TraceCV.Services;
using System.IO;

namespace TraceCV.Controllers
{
    public class ExpertController : Controller
    {
        private readonly DatabaseHandler _databaseHandler;
        private readonly ICountryProvider _countryProvider;

        public ExpertController(DatabaseHandler databaseHandler, ICountryProvider countryProvider)
        {
            _databaseHandler = databaseHandler ?? throw new ArgumentNullException(nameof(databaseHandler));
            _countryProvider = countryProvider;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.Countries = _countryProvider.GetAllCountries();
            return View(new Expert());
        }

        [HttpPost]
        public async Task<IActionResult> Index(Expert expert)
        {
            ViewBag.Countries = _countryProvider.GetAllCountries();
            EnsureSeed(expert);

            if (string.IsNullOrWhiteSpace(expert.Nationality))
                ModelState.AddModelError(nameof(expert.Nationality), "Nationality is required.");
            if (string.IsNullOrWhiteSpace(expert.Speciality))
                ModelState.AddModelError(nameof(expert.Speciality), "Area of speciality is required.");
            if (expert.CvFile == null)
                ModelState.AddModelError(nameof(expert.CvFile), "CV file is required.");

            MapSelections(expert);

            if (!ModelState.IsValid)
                return View(expert);

            try
            {
                if (expert.CvFile != null && expert.CvFile.Length > 0)
                {
                    var cvDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "cvs");
                    Directory.CreateDirectory(cvDir);
                    var safeName = Path.GetFileName(expert.CvFile.FileName);
                    var fileName = $"{Guid.NewGuid():N}-{safeName}";
                    using var fs = new FileStream(Path.Combine(cvDir, fileName), FileMode.Create);
                    await expert.CvFile.CopyToAsync(fs);
                    expert.CvFilePath = $"/cvs/{fileName}";
                }

                _databaseHandler.Experts.Add(expert);
                await _databaseHandler.SaveChangesAsync();
                TempData["SuccessMessage"] = "Expert saved successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error saving expert: {ex.Message}";
                return View(expert);
            }
        }

        public async Task<IActionResult> ListOfExperts(string name, string nationality, string keysector,
            string gender, string experience, string levelofeducation, string area_of_speciality)
        {
            try
            {
                var experts = _databaseHandler.Experts
                    .Include(e => e.Educations)
                    .AsQueryable();

                if (!string.IsNullOrWhiteSpace(name))
                    experts = experts.Where(e => e.Name.Contains(name));

                if (!string.IsNullOrWhiteSpace(nationality))
                    experts = experts.Where(e => e.Nationality.Contains(nationality));

                if (!string.IsNullOrWhiteSpace(gender))
                    experts = experts.Where(e => e.Gender.Contains(gender));

                if (!string.IsNullOrWhiteSpace(keysector))
                    experts = experts.Where(e => e.Sector.Contains(keysector));

                if (!string.IsNullOrWhiteSpace(levelofeducation))
                    experts = experts.Where(e => e.Educations.Any(ed => ed.Level.Contains(levelofeducation)));

                if (!string.IsNullOrWhiteSpace(area_of_speciality))
                    experts = experts.Where(e => e.Speciality.Contains(area_of_speciality));

                List<Expert> result;

                if (!string.IsNullOrWhiteSpace(experience) && int.TryParse(experience, out int minExp))
                {
                    var temp = await experts.ToListAsync();
                    result = temp
                        .Where(e => int.TryParse(e.experience, out var yrs) && yrs >= minExp)
                        .ToList();
                }
                else
                {
                    result = await experts.ToListAsync();
                }

                return View(result);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred while retrieving experts. Error: {ex.Message}";
                return View(new List<Expert>());
            }
        }

        public IActionResult Edit(int id)
        {
            var expert = _databaseHandler.Experts
                .Include(e => e.Languages)
                .Include(e => e.Educations)
                .Include(e => e.Contacts)
                .Include(e => e.Affiliations)
                .Include(e => e.OtherKeyExpertises)
                .Include(e => e.Certificates)
                .FirstOrDefault(e => e.Id == id);

            if (expert == null)
                return NotFound();

            EnsureSeed(expert);

            expert.SelectedOtherKeyExpertises = expert.OtherKeyExpertises?
                .Where(o => !string.IsNullOrWhiteSpace(o.Expertise))
                .Select(o => o.Expertise)
                .ToList() ?? new List<string>();

            expert.SelectedCertificates = expert.Certificates?
                .Where(c => !string.IsNullOrWhiteSpace(c.Name))
                .Select(c => c.Name!)
                .ToList() ?? new List<string>();

            ViewBag.Countries = _countryProvider.GetAllCountries();
            ViewBag.GenderList = new SelectList(new[] { "Male", "Female" }, expert.Gender);
            return View(expert);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Expert expert)
        {
            if (id != expert.Id)
                return NotFound();

            ViewBag.Countries = _countryProvider.GetAllCountries();
            EnsureSeed(expert);
            MapSelections(expert);

            if (string.IsNullOrWhiteSpace(expert.Nationality))
                ModelState.AddModelError(nameof(expert.Nationality), "Nationality is required.");
            if (expert.CvFile == null)
                ModelState.AddModelError(nameof(expert.CvFile), "Updated CV is required.");

            if (!ModelState.IsValid)
                return View(expert);

            try
            {
                var existing = await _databaseHandler.Experts
                    .Include(e => e.Educations)
                    .Include(e => e.OtherKeyExpertises)
                    .Include(e => e.Certificates)
                    .FirstOrDefaultAsync(e => e.Id == id);

                if (existing == null)
                    return NotFound();

                _databaseHandler.Entry(existing).CurrentValues.SetValues(expert);

                existing.OtherKeyExpertises = expert.OtherKeyExpertises;
                existing.Certificates = expert.Certificates;

                if (expert.CvFile != null && expert.CvFile.Length > 0)
                {
                    var cvDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "cvs");
                    Directory.CreateDirectory(cvDir);
                    var safeName = Path.GetFileName(expert.CvFile.FileName);
                    var fileName = $"{Guid.NewGuid():N}-{safeName}";
                    using var fs = new FileStream(Path.Combine(cvDir, fileName), FileMode.Create);
                    await expert.CvFile.CopyToAsync(fs);
                    existing.CvFilePath = $"/cvs/{fileName}";
                }

                if (existing.Educations != null && existing.Educations.Count > 0 &&
                    expert.Educations != null && expert.Educations.Count > 0)
                {
                    existing.Educations[0].Level = expert.Educations[0].Level;
                }

                await _databaseHandler.SaveChangesAsync();
                TempData["SuccessMessage"] = "Expert details updated.";
                return RedirectToAction(nameof(ListOfExperts));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error updating expert: {ex.Message}";
                return View(expert);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var expert = await _databaseHandler.Experts.FindAsync(id);
            if (expert == null)
                return NotFound();

            _databaseHandler.Experts.Remove(expert);
            await _databaseHandler.SaveChangesAsync();
            return RedirectToAction(nameof(ListOfExperts));
        }

        private static void EnsureSeed(Expert expert)
        {
            if (expert.Contacts == null || expert.Contacts.Count == 0)
                expert.Contacts = new List<Contact> { new Contact() };
            if (expert.Languages == null || expert.Languages.Count == 0)
                expert.Languages = new List<Language> { new Language() };
            if (expert.Educations == null || expert.Educations.Count == 0)
                expert.Educations = new List<Education> { new Education() };
            if (expert.Affiliations == null || expert.Affiliations.Count == 0)
                expert.Affiliations = new List<Affiliation> { new Affiliation() };
            if (expert.SelectedOtherKeyExpertises == null)
                expert.SelectedOtherKeyExpertises = new List<string>();
            if (expert.SelectedCertificates == null)
                expert.SelectedCertificates = new List<string>();
        }

        private static void MapSelections(Expert expert)
        {
            expert.OtherKeyExpertises = expert.SelectedOtherKeyExpertises?
                .Distinct()
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .Select(s => new OtherKeyExpertise { Expertise = s })
                .ToList() ?? new List<OtherKeyExpertise>();

            expert.Certificates = expert.SelectedCertificates?
                .Distinct()
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .Select(s => new Certificate { Name = s })
                .ToList() ?? new List<Certificate>();

        }
    }
}