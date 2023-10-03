using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Planner.Models.Domain;
using Planner.Models.ViewModels;
using Planner.Repositories;

namespace Planner.Controllers
{
    public class WeekDayController : Controller
    {
        private readonly IWeekDayRepository weekDayRepository;
        private readonly IPatientRepository patientRepository;

        public WeekDayController(IWeekDayRepository weekDayRepository, IPatientRepository patientRepository)
        {
            this.weekDayRepository = weekDayRepository;
            this.patientRepository = patientRepository;
        }

        [HttpGet]
        /*  public async Task<IActionResult> List()
          {
              var weekDay = await weekDayRepository.GetAllAsync();
              return View(weekDay);
          }*/
        public async Task<IActionResult> List()
        {
            return RedirectToAction("Index", "Home");
        }



        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Add")]
        public async Task<IActionResult> Add(AddWeekDayRequest addWeekDayRequest)
        {
            var weekDay = new WeekDay
            {
                Day = addWeekDayRequest.Day
            };
            await weekDayRepository.AddAsync(weekDay);
            return RedirectToAction("List");
        }

        [HttpGet]
        public async Task<IActionResult> Edit (Guid id)
        {
            var patients = await patientRepository.GetAllAsync();
            var weekDay = await weekDayRepository.GetAsync(id);
            if (weekDay != null)
            {
                var editWeekDay = new EditWeekDayRequest
                {
                    ArriviaDay = weekDay.ArriviaDay,
                    ActivityDay = weekDay.ActivityDay,
                    QuantityMbK = weekDay.QuantityMbK,
                    RegisteredPatients = weekDay.RegisteredPatients,
                    Patients = patients.Select(x => new SelectListItem { Text = x.Name + " " + x.Surname + " " + x.Research, Value = x.Id.ToString() })
                };

               
                return View(editWeekDay);
              
            }
            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditWeekDayRequest editWeekDayRequest)
        {

            var weekDay = new WeekDay
            {
                Id = editWeekDayRequest.Id,
                Day = editWeekDayRequest.Day,
                ArriviaDay = editWeekDayRequest.ArriviaDay,
                ActivityDay = editWeekDayRequest.ActivityDay,
                QuantityMbK = editWeekDayRequest.QuantityMbK,
                RegisteredPatients = editWeekDayRequest.RegisteredPatients,
                RemainingPatients = editWeekDayRequest.RemainingPatients
            };

            //Map Tags from Selected tags
            var selectedPtients = new List<Patient>();
            foreach (var selectedPatientId in editWeekDayRequest.SelectPatients)
            {
                var selectedPatientIdAsGuid = Guid.Parse(selectedPatientId);
                var existingPatient = await patientRepository.GetAsync(selectedPatientIdAsGuid);
                if (existingPatient != null)
                {
                    selectedPtients.Remove(existingPatient);
                }
            }
            var updateWeekDay = await weekDayRepository.UpdateAsync(weekDay);
            if (updateWeekDay != null)
            {
                //Show success
                return RedirectToAction("List");
            }
            else
            {

                // Show failed
            }

            return RedirectToAction("Edit", new { id = editWeekDayRequest.Id });
        }

    }
}
