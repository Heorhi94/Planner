using Microsoft.AspNetCore.Mvc;
using Planner.Models;
using Planner.Repositories.Interface;
using System.Diagnostics;

namespace Planner.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWeekDayRepository weekDayRepository;
        private readonly IWeekDayServices weekDayServices;

        public HomeController(ILogger<HomeController> logger, IWeekDayRepository weekDayRepository, IWeekDayServices weekDayServices)
        {
            _logger = logger;
            this.weekDayRepository = weekDayRepository;
            this.weekDayServices = weekDayServices;
        }
        
        public async Task<IActionResult> Index()
        {
            var weekDay = await weekDayRepository.GetAllAsync();
            return View(weekDay);
        }

        public async Task<IActionResult> History()
        {
            var weekDay = await weekDayServices.GetHistoryAsync();
            return View(weekDay);
        }

        public async Task<IActionResult> Analitics()
        {
            var analitics =await weekDayServices.GetAnalitics();

            return View(analitics);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}