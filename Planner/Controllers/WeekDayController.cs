using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Planner.Models.Domain;
using Planner.Models.ViewModels;
using Planner.Repositories.Interface;
using Planner.Service;

namespace Planner.Controllers
{
    public class WeekDayController : Controller
    {
        private readonly IWeekDayRepository weekDayRepository;
        private readonly IPatientRepository patientRepository;
        private readonly IWeekDayServices weekDayServices;
        public WeekDayController(IWeekDayRepository weekDayRepository, IPatientRepository patientRepository, IWeekDayServices weekDayServices)
        {
            this.weekDayRepository = weekDayRepository;
            this.patientRepository = patientRepository;
            this.weekDayServices = weekDayServices;
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
            CalculationMBK calculationMBK = new CalculationMBK();
            weekDay.ActivityDay = calculationMBK.ActivityDay(addWeekDayRequest.Day);
            weekDay.QuantityMbK = calculationMBK.QuantityMbK(weekDay.ActivityDay);
            weekDay.RemainderMBK = calculationMBK.RemainderMBK(weekDay);
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
                    ArrivalDay = weekDay.ArrivalDay,
                    ActivityDay = weekDay.ActivityDay,
                    QuantityMbK = weekDay.QuantityMbK,
                    RegisteredPatients = weekDay.RegisteredPatients,
                    Day = weekDay.Day,
                };
                var patients = await patientRepository.GetPatientForDay(weekDay.Id);
                CalculationMBK calculationMBK = new CalculationMBK();
                foreach (var patient in patients)
                {
                    patient.MBK = calculationMBK.PatientMbK(weekDay);
                }
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
                ArrivalDay = editWeekDayRequest.ArrivalDay,
                ActivityDay = editWeekDayRequest.ActivityDay,
                QuantityMbK = editWeekDayRequest.QuantityMbK,
                RegisteredPatients = editWeekDayRequest.RegisteredPatients,
                RemainingPatients = editWeekDayRequest.RemainingPatients
            };

            var patients = await patientRepository.GetPatientForDay(weekDay.Id);
            weekDay.Patients = (ICollection<Patient>)patients;
            CalculationMBK calculationMBK = new CalculationMBK();
            weekDay.ActivityDay = calculationMBK.ActivityDay(weekDay.Day);
            weekDay.QuantityMbK = calculationMBK.QuantityMbK(weekDay.ActivityDay);

            var updateWeekDay = await weekDayRepository.UpdateAsync(weekDay);
            if (updateWeekDay != null)
            {
                return RedirectToAction("List");
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
                return RedirectToAction("List");
            }
            return RedirectToAction("Edit", new { id = editWeekDayRequest.Id });
        }

        [HttpPost]
        public async Task<IActionResult> CloseDay(EditWeekDayRequest editWeekDayRequest)
        {
            var weekDay = new WeekDay
            {
                Id = editWeekDayRequest.Id,
                Day = editWeekDayRequest.Day,
                ArrivalDay = editWeekDayRequest.ArrivalDay,
                ActivityDay = editWeekDayRequest.ActivityDay,
                QuantityMbK = editWeekDayRequest.QuantityMbK,
                RegisteredPatients = editWeekDayRequest.RegisteredPatients,
                RemainingPatients = editWeekDayRequest.RemainingPatients
            };
            var patients = await patientRepository.GetPatientForDay(weekDay.Id);
            weekDay.Patients = (ICollection<Patient>)patients;
            CalculationMBK calculationMBK = new CalculationMBK();
            weekDay.ActivityDay = calculationMBK.ActivityDay(weekDay.Day);
            weekDay.QuantityMbK = calculationMBK.QuantityMbK(weekDay.ActivityDay);
            foreach(var patient in weekDay.Patients)
            {
                patient.MBK = calculationMBK.PatientMbK(weekDay);
            }
            weekDay.RemainderMBK = calculationMBK.UpdRemainderMBK(weekDay);
            var updateWeekDay = await weekDayServices.GetUpdMbK(weekDay);
            if (updateWeekDay != null)
            {
                return RedirectToAction("List");
            }
            return RedirectToAction("Edit", new { id = editWeekDayRequest.Id });
        }


    }
}
