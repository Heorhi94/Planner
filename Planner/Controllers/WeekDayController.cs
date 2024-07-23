using Azure;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IServices services;
        private CalculationMBK calculationMBK = new CalculationMBK();
        private Generator generator = new Generator();

        public WeekDayController(IWeekDayRepository weekDayRepository, IPatientRepository patientRepository, IServices services)
        {
            this.weekDayRepository = weekDayRepository;
            this.patientRepository = patientRepository;
            this.services = services;
        }

        [HttpGet]
        public IActionResult List()
        {
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var occupiedDates = await weekDayRepository.GetAllAsync();
            var dates = occupiedDates.Select(d => d.Day.ToString("yyyy-MM-dd")).ToList();

            ViewBag.OccupiedDates = dates;
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
            weekDay.ActivityDay = generator.ActivityDay(addWeekDayRequest.Day);
            weekDay.QuantityMbK = generator.ValMbkForGenDay(weekDay.ActivityDay);
            weekDay.RemainderMBK = calculationMBK.RemainderMBK(weekDay);
            weekDay.ArrivalDay = generator.ArrivalDay(weekDay.ActivityDay, addWeekDayRequest.Day);
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
                    RemainderMBK = weekDay.RemainderMBK,
                    RegisteredPatients = weekDay.RegisteredPatients,
                    Day = weekDay.Day,
                };
                var patients = await services.GetPatientForDay(weekDay.Id);
                foreach (var patient in patients)
                {
                    patient.MBK = calculationMBK.PatientMbK(patient);
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
               // ArrivalDay = editWeekDayRequest.ArrivalDay,
                RegisteredPatients = editWeekDayRequest.RegisteredPatients,
                RemainingPatients = editWeekDayRequest.RemainingPatients
            };

            var patients = await services.GetPatientForDay(weekDay.Id);
            weekDay.Patients = (ICollection<Patient>)patients;
            weekDay.ActivityDay = generator.ActivityDay(weekDay.Day);
            weekDay.QuantityMbK = generator.ValMbkForGenDay(weekDay.ActivityDay);
           // weekDay.ArrivalDay = generator.ArrivalDay(weekDay.Day);
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
                await services.DeletePatientsForDay(editWeekDayRequest.Id);
                return RedirectToAction("List");
            }
            return RedirectToAction("Edit", new { id = editWeekDayRequest.Id });
        }
        [HttpPost]   
        public async Task<IActionResult> Calculated (EditWeekDayRequest editWeekDayRequest)
        {
            var weekDay = new WeekDay
            {
                Id = editWeekDayRequest.Id,
                Day = editWeekDayRequest.Day,
                ArrivalDay = editWeekDayRequest.ArrivalDay,
                RegisteredPatients = editWeekDayRequest.RegisteredPatients,
                RemainingPatients = editWeekDayRequest.RemainingPatients,
            };
            var patients = await services.GetPatientForDay(weekDay.Id);
            weekDay.Patients = (ICollection<Patient>)patients;
            weekDay.ActivityDay = generator.ActivityDay(weekDay.Day);
            weekDay.QuantityMbK = generator.ValMbkForGenDay(weekDay.ActivityDay);
            weekDay.RemainderMBK = calculationMBK.RemainderMBK(weekDay);
           
            weekDay.Patients = calculationMBK.CalculatedResult(weekDay);

                weekDay.RemainderMBK = calculationMBK.CalculatedRemainderMBK(weekDay);
            
           /* foreach (var patient in weekDay.Patients)
            {
                patient.MBK = calculationMBK.CalculatedResult(patient, weekDay);
                await patientRepository.UpdateAsync(patient);
                weekDay.RemainderMBK = calculationMBK.CalculatedRemainderMBK(weekDay);
            }*/
            await services.UpdateMBK(weekDay);
            return RedirectToAction("List");
        }


        [HttpPost]
        public IActionResult UpdateChart(DateTime selectedDate, string selectedResearch)
        {
            // Здесь вы можете использовать выбранную дату и исследование для получения новых данных для графика
            // Например, вы можете использовать ваш репозиторий для получения данных
            var newDataForChart = GetDataForChart(selectedDate, selectedResearch);

            // Верните данные в формате JSON
            return Json(newDataForChart);
        }

        private object GetDataForChart(DateTime selectedDate, string selectedResearch)
        {
            // Здесь должна быть логика для получения новых данных для графика на основе выбранной даты и исследования
            // Например, вы можете использовать ваш сервис или репозиторий для получения этих данных

            // В этом примере я просто возвращаю случайные данные для иллюстрации
            var random = new Random();
            var newData = new List<int>();
            for (int i = 0; i < 7; i++)
            {
                newData.Add(random.Next(10, 100)); // Просто случайные данные
            }

            return new { newData };
        }


    }
}
