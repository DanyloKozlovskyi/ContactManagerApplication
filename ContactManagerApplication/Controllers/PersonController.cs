using ContactManager.DataAccess.Models;
using ContactManagerApplication.Services;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace ContactManagerApplication.Controllers
{
    [Route("[controller]")]
    public class PersonController : Controller
    {
        private readonly IPersonService personService;
        public PersonController(IPersonService personService)
        {
            this.personService = personService;
        }
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = await personService.GetAll();
            return View(users);
        }
        [Route("[action]")]
        [HttpGet]
        public ActionResult Upload()
        {
            return View();
        }
        [Route("[action]")]
        [HttpPost]
        public async Task<ActionResult> Upload(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                try
                {
                    var fileExtension = Path.GetExtension(file.FileName);
                    if (fileExtension != ".csv")
                    {
                        ViewBag.Message = "Please upload a valid CSV file.";
                        return View();
                    }
                    var persons = new List<Person>();
                    using (var reader = new StreamReader(file.OpenReadStream()))
                    {
                        bool isFirstRow = true;
                        while (!reader.EndOfStream)
                        {
                            var line = reader.ReadLine();

                            // Skip the header row
                            if (isFirstRow)
                            {
                                isFirstRow = false;
                                continue;
                            }

                            var columns = line.Split(',');

                            if (columns.Length != 5) // Validate number of columns
                                throw new FormatException("CSV format is invalid. Expected 5 columns.");

                            try
                            {
                                var person = new Person
                                {
                                    Id = Guid.NewGuid(),
                                    Name = columns[0].Trim(),
                                    DateOfBirth = DateOnly.ParseExact(columns[1].Trim(), "yyyy.MM.dd", CultureInfo.InvariantCulture),
                                    Married = bool.Parse(columns[2].Trim()),
                                    PhoneNumber = columns[3].Trim(),
                                    Salary = decimal.Parse(columns[4].Trim(), CultureInfo.InvariantCulture)
                                };
                                persons.Add(person);
                            }
                            catch (Exception ex)
                            {
                                throw new FormatException($"Error parsing line: {line}. Details: {ex.Message}");
                            }
                        }
                    }
                    await personService.AddMany(persons);
                    ViewBag.Message = "File uploaded and processed successfully.";
                    return RedirectToAction(nameof(Index), persons);
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "Error: " + ex.Message;
                    return View();
                }
            }
            else
            {
                ViewBag.Message = "No file selected.";
                return View();
            }
        }
    }
}
