using Microsoft.AspNetCore.Mvc;
using Planner.Models.Domain;
using Planner.Models.ViewModels;
using Planner.Repositories;
using Planner.Repositories.Interface;
using Planner.Service;

namespace Planner.Controllers
{
    public class ServicesController : Controller
    {
        private readonly IServices weekDayServices;
        private CalculationMBK calculationMBK = new CalculationMBK();

        public ServicesController(IServices weekDayServices)
        {
            this.weekDayServices = weekDayServices;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
