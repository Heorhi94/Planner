using System.Reflection.Emit;

namespace Planner.Service
{
    public class Generator
    {
        public readonly Dictionary<string, double> RadiationsDay = new Dictionary<string, double>
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
        public DateTime ArrivalDay(DateTime date)
        {
            if (date != new DateTime(2023, 10, 3))
            {
                return date;
            }
            return date;
        }
        public int ActivityDay(DateTime day)
        {
            DateTime dayStart = new DateTime(2023, 10, 3);
            DateTime targetDate = dayStart;

            int numberOfDays = 0;
            int daysToAdd = 14;
            while (targetDate < day)
            {
                targetDate = targetDate.AddDays(1);

                numberOfDays++;
                if (numberOfDays > daysToAdd) // Zresetuj numberOfDays na 1 po znalezieniu co drugiego wtorku
                {
                    numberOfDays = 1;
                    //targetDate = dayStart.AddDays(daysToAdd);
                }
            }
            return numberOfDays;
        }
        public double ValMbkForGenDay(int day)
        {
            string key = day.ToString();

            if (RadiationsDay.ContainsKey(key))
            {
                return RadiationsDay[key];
            }
            return 0;
        }


    }
}
