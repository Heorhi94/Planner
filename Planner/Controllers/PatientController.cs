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
        public async Task<IActionResult> Add(AddPatientRequest addPatientReqest)
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

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var patient = await patientRepository.GetAsync(id);

            if (patient != null)
            {
                var editPatientRequest = new EditPatientRequest
                {
                    Id = patient.Id,
                    Name = patient.Name,
                    Surname = patient.Surname,
                    RegistrationDay = patient.RegistrationDay,
                    Research = patient.Research
                };

                return View(editPatientRequest);
            }

            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditPatientRequest editPatientRequest)
        {
            var patient = new Patient
            {
                Id = editPatientRequest.Id,
                Name = editPatientRequest.Name,
                Surname = editPatientRequest.Surname,
                RegistrationDay = editPatientRequest.RegistrationDay,
                Research = editPatientRequest.Research,
            };

            var updatePatient = await patientRepository.UpdateAsync(patient);
            if (updatePatient != null)
            {
                //Show success
                return RedirectToAction("List");
            }
            else
            {

                // Show failed
            }

            return RedirectToAction("Edit", new { id = editPatientRequest.Id });
        }

        [HttpPost]

        public async Task<IActionResult> Delete(EditPatientRequest editPatientRequest)
        {
            var deletePatient = await patientRepository.DeleteAsync(editPatientRequest.Id);

            if (deletePatient != null)
            {
                //Show success
                return RedirectToAction("List");
            }

            //Show failed
            return RedirectToAction("Edit", new { id = editPatientRequest.Id });
        }
    }
}
