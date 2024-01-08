using Microsoft.AspNetCore.Mvc;
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

      /*  [HttpPost]
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
        }*/
    }
}
