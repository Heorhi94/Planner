using System.Reflection.Emit;

namespace Planner.Service
{
    public class Generator
    {
        public DateTime ArrivalDate { get; set; }
        public Dictionary<string, double> RadiationsDay = new Dictionary<string, double>
        {
            {"0", 10000 },
            {"1", 7772 },
            {"2", 6040 },
            {"3", 4695 },
            {"4", 3649 },
            {"5", 2836 },
            {"6", 2204 },
            {"7", 1713 },
            {"8", 1331 },
            {"9", 1035 },
            {"10", 804 },
            {"11", 625 },
            {"12", 486 },
            {"13", 378 },
            {"14", 293 }
        };

        public int ActivityDays(DateTime startDate, DateTime endDate)
        {
            int workingDays = 0;
            for (var date = startDate; date <= endDate; date = date.AddDays(1))
            {
                if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)
                {
                    workingDays++;
                }
            }
            return workingDays;
        }
    }
}
