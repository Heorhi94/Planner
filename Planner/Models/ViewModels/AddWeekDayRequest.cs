using Planner.Models.Domain;

namespace Planner.Models.ViewModels
{
    public class AddWeekDayRequest
    {
        public DateTime Day { get; set; }
        public DateTime ArriviaDay { get; set; }
        public int ActivityDay { get; set; }
        public double QuantityMbK { get; set; }
        public double RemainderMBK { get; set; }
        public int RegisteredPatients { get; set; }
        public int RemainingPatients { get; set; }
        public int NumberOfPatients { get; set; }
        public ICollection<Patient> Patients { get; set; }
    }
}
