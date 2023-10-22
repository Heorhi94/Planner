using Microsoft.AspNetCore.Mvc.Rendering;
using Planner.Models.Domain;

namespace Planner.Models.ViewModels
{
    public class EditWeekDayRequest
    {
        public Guid Id { get; set; }
        public DateTime ArrivalDay { get; set; }
        public DateTime Day { get; set; }
        public int ActivityDay { get; set; }
        public double QuantityMbK { get; set; }
        public int RegisteredPatients { get; set; }
        public int RemainingPatients { get; set; }
        public int NumberOfPatients { get; set; }
        public ICollection<Patient> Patients { get; set; }
       // public string[] SelectPatients { get; set; } = Array.Empty<string>();
    }
}
