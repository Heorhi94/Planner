using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Planner.Models.Domain;
using Planner.Models.ViewModels;
using Planner.Repositories;

namespace Planner.Controllers
{
    public class PatientController : Controller
    {
        private readonly IPatientRepository patientRepository;
        private readonly IWeekDayRepository weekDayRepository;

        public PatientController(IPatientRepository patientRepository, IWeekDayRepository weekDayRepository)
        {
            this.patientRepository = patientRepository;
            this.weekDayRepository = weekDayRepository;
        }

        [HttpGet]
        public IActionResult Add(DateTime registrationDay, Guid weekDayId)
        {
          //  var weekDay = await weekDayRepository.GetAsync(weekDayId);
            var addPatientRequest = new AddPatientRequest
            {
                PatientWeekDayId = weekDayId,
                RegistrationDay = registrationDay
            };

            return View(addPatientRequest);
        }

        [HttpPost]
        [ActionName("Add")]
        public async Task<IActionResult> Add(AddPatientRequest addPatientRequest)
        {       
            Guid id = new Guid();
            var patient = new Patient
            {
                Id = id,
                WeekDayId = addPatientRequest.PatientWeekDayId,
                RegistrationDay = addPatientRequest.RegistrationDay,
                Name = addPatientRequest.Name,
                Surname = addPatientRequest.Surname,
                Research = addPatientRequest.Research,
            };
            await patientRepository.AddAsync(patient);

            return RedirectToAction("List");
        }




        [HttpGet]
        [ActionName("List")]
        public async Task<IActionResult> List(Guid Id)
        {
            //use dbContext to read the tags
            var patients = await patientRepository.GetAllAsync(Id);
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
