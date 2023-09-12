namespace Planner.Models.Domain
{
    public class WeekDay
    {
        public Guid Id { get; set; }
        public DateTime Day { get; set; }
        public int ActivityDay { get; set; }
        public double QuantityMbK { get; set; } 
        public int RegisteredPatients { get; set; }
        public int RemainingPatients { get; set; }
        public ICollection<Patient> Patients { get; set; }

    }
}
