using Microsoft.AspNetCore.Mvc.Rendering;

namespace Planner.Models.ViewModels
{
    public class EditWeekDayRequest
    {
        public Guid Id { get; set; }
        public DateTime ArriviaDay { get; set; }
        public DateTime Day { get; set; }
        public int ActivityDay { get; set; }
        public double QuantityMbK { get; set; }
        public int RegisteredPatients { get; set; }
        public int RemainingPatients { get; set; }
        public int NumberOfPatients { get; set; }
        public IEnumerable<SelectListItem> Patients { get; set; }
        public string[] SelectPatients { get; set; } = Array.Empty<string>();
    }
}
