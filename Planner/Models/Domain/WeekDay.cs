using Planner.Service;
using System.Reflection.Metadata.Ecma335;

namespace Planner.Models.Domain
{
    public class WeekDay
    {
        Generator generator = new();
        public Guid Id { get; set; }
        public DateTime ArriviaDay {get;set;}
        public DateTime Day { get => DateTime.Today;}
        public int ActivityDay
        {
            get
            {
                return generator.ActivityDays(ArriviaDay, Day);
            }
        }
        public double QuantityMbK 
        {
            get
            {
                if (generator.RadiationsDay.ContainsKey(ActivityDay.ToString()))
                {
                    return generator.RadiationsDay[ActivityDay.ToString()];
                }
                else
                {
                    return 0;
                }
            }

            set
            {
                generator.RadiationsDay[ActivityDay.ToString()] = value;
            } 
        } 
        public int RegisteredPatients { get; set; }
        public int RemainingPatients { get; set; }
        public int NumberOfPatients { get; set; }
        public ICollection<Patient> Patients { get; set; }

    }
}
