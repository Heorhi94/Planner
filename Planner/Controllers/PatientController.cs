using Azure;
using Microsoft.AspNetCore.Mvc;
using Planner.Models.Domain;
using Planner.Models.ViewModels;
using Planner.Repositories;

namespace Planner.Controllers
{
    public class PatientController : Controller
    {
        private readonly IPatientRepository patientRepository;

        public PatientController(IPatientRepository patientRepository)
        {
            this.patientRepository = patientRepository;
        }


        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Add")]
        public async Task<IActionResult> Add(AddPatientReqest addPatientReqest)
        {
            //Mapping AddTagRequest to Tag domian model
            var patient = new Patient
            {
                Name = addPatientReqest.Name,
                Surname = addPatientReqest.Surname,
                RegistrationDay = addPatientReqest.RegistrationDay,
                Research = addPatientReqest.Research
            };
            await patientRepository.AddAsync(patient);

            return RedirectToAction("List");
        }

        [HttpGet]
        [ActionName("List")]
        public async Task<IActionResult> List()
        {
            //use dbContext to read the tags
            var patients = await patientRepository.GetAllAsync();
            return View(patients);
        }
    }
}
