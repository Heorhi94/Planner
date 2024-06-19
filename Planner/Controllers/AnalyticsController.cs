/*using Microsoft.AspNetCore.Mvc;
using Planner.Models.ViewModels;
using Planner.Repositories;
using Planner.Repositories.Interface;
using Planner.Service;

namespace Planner.Controllers
{
    public class AnalyticsController : Controller
    {
        private readonly IAnalyticsRepository analyticsRepository;
        Research research = new Research();
        
        public AnalyticsController (IAnalyticsRepository analyticsRepository)
        {
            this.analyticsRepository = analyticsRepository;
        }
        public async Task<IActionResult> Analytics()
        {
            var patients = await analyticsRepository.GetPatient();
            var analyticsRequest = new AnalyticsRequest
            {
               StartDay = patients.Min(patient => patient.RegistrationDay),
               EndDay = patients.Max(patient => patient.RegistrationDay),
               AvgMBK = patients.Average(patient => patient.MBK)
            };
            analyticsRequest.NameResearchList = research.nameResearch.Keys.ToList();
            analyticsRequest.CountResearch = patients
             .GroupBy(patient => patient.Research)
             .ToDictionary(
                  group => group.Key,
                  group => group.Count()
             );
            return View(analyticsRequest);
        }

      *//*  [HttpPost]
        public async Task<IActionResult> YourAction(AnalyticsRequest analytics)
        {
            var patients = await analyticsRepository.GetPatient();
            var updAnalytics = new AnalyticsRequest
            {
                StartDay = analytics.StartDay,
                EndDay = analytics.EndDay,
                Research = analytics.Research,
                NameResearchList = analytics.NameResearchList,
                AvgMBK = patients.Average(patient => patient.MBK)
            };
            updAnalytics.CountResearch = patients
             .GroupBy(patient => patient.Research)
             .ToDictionary(
                  group => group.Key,
                  group => group.Count()
             );
            return Json(new { labels = updAnalytics.Research, data = updAnalytics.AvgMBK });
        }*//*
    }
}
*/


using Microsoft.AspNetCore.Mvc;
using Planner.Models.Domain;
using Planner.Models.ViewModels;
using Planner.Repositories;
using Planner.Repositories.Interface;
using Planner.Service;

namespace Planner.Controllers
{
    public class AnalyticsController : Controller
    {
        private readonly IAnalyticsRepository analyticsRepository;
        Research research = new Research();

        public AnalyticsController(IAnalyticsRepository analyticsRepository)
        {
            this.analyticsRepository = analyticsRepository;
        }

        // Ваш текущий метод действия для отображения страницы аналитики
        public async Task<IActionResult> Analytics()
        {
            var patients = await analyticsRepository.GetPatient();
            var analyticsRequest = new AnalyticsRequest
            {
                StartDay = patients.Min(patient => patient.RegistrationDay),
                EndDay = patients.Max(patient => patient.RegistrationDay),
                AvgMBK = patients.Average(patient => patient.MBK)
            };
            analyticsRequest.NameResearchList = research.nameResearch.Keys.ToList();
            analyticsRequest.CountResearch = patients
             .GroupBy(patient => patient.Research)
             .ToDictionary(
                  group => group.Key,
                  group => group.Count()
             );
            return View(analyticsRequest);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateChart(AnalyticsRequest request)
        {
            // Используйте методы из вашего репозитория для получения данных для графика
            IEnumerable<Patient> patients;

            if (!string.IsNullOrEmpty(request.Research)) // Если выбрано конкретное исследование
            {
                patients = await analyticsRepository.GetPatientResearch(request.StartDay, request.EndDay, request.Research);
            }
            else // Если не выбрано конкретное исследование
            {
                patients = await analyticsRepository.GetPatient(request.StartDay, request.EndDay);
            }

            // В этом примере я просто возвращаю средние значения MBK для всех пациентов за выбранный период времени
            // Вы должны настроить эту логику в соответствии с вашими требованиями
            var newDataForChart = patients.Select(patient => patient.MBK).ToList();

            return Json(newDataForChart);
        }
    }
}
