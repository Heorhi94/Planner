using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Planner.Models.Domain;
using Planner.Models.ViewModels;
using Planner.Repositories.Interface;
using Planner.Service;

namespace Planner.Controllers
{

    public class PatientController : Controller
    {
        private readonly IPatientRepository patientRepository;
        private readonly IWeekDayRepository weekDayRepository;
        private CalculationMBK calculationMBK = new CalculationMBK();
        private Research research = new Research();

        public PatientController(IPatientRepository patientRepository, IWeekDayRepository weekDayRepository)
        {
            this.patientRepository = patientRepository;
            this.weekDayRepository = weekDayRepository;
        }

        [HttpGet]
        public IActionResult Add(DateTime registrationDay, Guid weekDayId, double quantityMBK)
        {
            List <string> list = new List<string>();
            foreach (var research in research.radiationResearch)
            {
                foreach (var minMax in research.Value)
                {
                    if (minMax.Key == "Min")
                    {
                        var minValue = minMax.Value;
                        if (quantityMBK > minValue)
                        {
                            list.Add(research.Key.ToString());
                        }
                    }
                }
            }
            var addPatientRequest = new AddPatientRequest
            {
                PatientWeekDayId = weekDayId,
                RegistrationDay = registrationDay,
                NameResearchList = list,
                QuantityMBK = quantityMBK
            };
            return View(addPatientRequest);
        }

        [HttpPost]
        [ActionName("Add")]
        public async Task<IActionResult> Add(AddPatientRequest addPatientRequest)
        {    
            var patient = new Patient
            {
                Id = new Guid(),
                WeekDayId = addPatientRequest.PatientWeekDayId,
                RegistrationDay = addPatientRequest.RegistrationDay,
                Name = addPatientRequest.Name,
                Surname = addPatientRequest.Surname,
                Research = addPatientRequest.Research,
                BirthDay = addPatientRequest.BirthDay,
            };

            var updWeekDay = await weekDayRepository.GetAsync(patient.WeekDayId);
            patient.MBK = calculationMBK.PatientMbK(patient);
            await patientRepository.AddAsync(patient);
            await weekDayRepository.UpdateAsync(updWeekDay);
           
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid weekDayId)
        {
            var patient = await patientRepository.GetAsync(weekDayId);

            if (patient != null)
            {
                var editPatientRequest = new EditPatientRequest
                {
                    Id = patient.Id,
                    Name = patient.Name,
                    Surname = patient.Surname,
                    RegistrationDay = patient.RegistrationDay,
                    Research = patient.Research,
                    PatientWeekDayId = patient.WeekDayId
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
                return RedirectToAction("Index", "Home");
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
                var updWeekDay = await weekDayRepository.GetAsync(editPatientRequest.PatientWeekDayId);
                await weekDayRepository.UpdateAsync(updWeekDay);
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Edit", new { id = editPatientRequest.Id });
        }
    }
}   
