using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Planner.Models.Domain;
using Planner.Models.ViewModels;
using Planner.Repositories;
using Planner.Service;

namespace Planner.Controllers
{
   
    public class WeekDayController : Controller
    {
        private readonly IWeekDayRepository weekDayRepository;
        private readonly IPatientRepository patientRepository;
        CalculationMBK calculationMBK = new CalculationMBK();
        public WeekDayController(IWeekDayRepository weekDayRepository, IPatientRepository patientRepository)
        {
            this.weekDayRepository = weekDayRepository;
            this.patientRepository = patientRepository;
        }

        [HttpGet]
        public IActionResult List()
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
                Day = addWeekDayRequest.Day,
            };
            var added = await weekDayRepository.GetAllAsync();
            foreach(var add in added)
            {
                if(add.Day == addWeekDayRequest.Day)
                {
                    return RedirectToAction("List");
                }
            }
            weekDay.ActivityDay = calculationMBK.ArrivalDay(addWeekDayRequest.Day);
            weekDay.QuantityMbK = calculationMBK.QuantityMbK(weekDay.ActivityDay);
            await weekDayRepository.AddAsync(weekDay);
            return RedirectToAction("List");
        }


        [HttpGet]
        public async Task<IActionResult> Edit(Guid weekDayId)
        {
            var weekDay = await weekDayRepository.GetAsync(weekDayId);

            if (weekDay != null)
            {
                var editWeekDay = new EditWeekDayRequest
                {
                    Id = weekDay.Id,
                    ArriviaDay = weekDay.ArriviaDay,
                    ActivityDay = weekDay.ActivityDay,
                    QuantityMbK = weekDay.QuantityMbK,
                    RegisteredPatients = weekDay.RegisteredPatients,
                    Day = weekDay.Day,
                };
                var patients = await patientRepository.GetPatientForDay(weekDay.Id);
                editWeekDay.Patients = (ICollection<Patient>)patients;


                return View(editWeekDay);

            }
            return View(null);
        }

        [HttpPost]
        [ActionName("Edit")]
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

            var patients = await patientRepository.GetPatientForDay(weekDay.Id);
            weekDay.Patients = (ICollection<Patient>)patients;
            weekDay.ActivityDay = calculationMBK.ArrivalDay(weekDay.Day);
            weekDay.QuantityMbK = calculationMBK.QuantityMbK(weekDay.ActivityDay);
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

        [HttpPost]
        public async Task<IActionResult> Delete(EditWeekDayRequest editWeekDayRequest)
        {
            var deleteweekDay = await weekDayRepository.DeleteAsync(editWeekDayRequest.Id);
            if (deleteweekDay != null)
            {
                await patientRepository.DeletePatientsForDay(editWeekDayRequest.Id);
                //Show success
                return RedirectToAction("List");
            }

            //Show failed
            return RedirectToAction("Edit", new { id = editWeekDayRequest.Id });
        }

    }
}
