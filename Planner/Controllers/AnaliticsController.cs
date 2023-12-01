using Microsoft.AspNetCore.Mvc;
using Planner.Models.ViewModels;
using Planner.Repositories;
using Planner.Repositories.Interface;
using Planner.Service;

namespace Planner.Controllers
{
    public class AnaliticsController : Controller
    {
        private readonly IAnaliticsRepository analiticsRepository;
        Research research = new Research();
        
        public AnaliticsController (IAnaliticsRepository analiticsRepository)
        {
            this.analiticsRepository = analiticsRepository;
        }
        public async Task<IActionResult> Analitics()
        {
            var patients = await analiticsRepository.GetPatient();
            var analiticsRequest = new AnaliticsRequest
            {
               StartDay = patients.Min(patient => patient.RegistrationDay),
               EndDay = patients.Max(patient => patient.RegistrationDay),
               AvgMBK = patients.Average(patient => patient.MBK)
            };
            analiticsRequest.NameResearchList = research.nameResearch.Keys.ToList();
            analiticsRequest.CountResearch = patients
             .GroupBy(patient => patient.Research)
             .ToDictionary(
                  group => group.Key,
                  group => group.Count()
             );
            return View(analiticsRequest);
        }
    }
}
