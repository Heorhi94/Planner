using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Planner.Models.Domain;

namespace Planner.Service
{
    public class CalculationMBK
    {
      //  Patient patient;
      //  Research research;
        WeekDay weekDay = new WeekDay();

        // private readonly DateTime dayStart = new DateTime(03-10-2023);

        private readonly Dictionary<string, double> RadiationsDay = new Dictionary<string, double>
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

        private List<int> valueResearch = new List<int>
       {
           {0 },
           {0 },
           {0 },
           {0 }
       };


        public Dictionary<string, Dictionary<string, int>> radiationResearch = new Dictionary<string, Dictionary<string, int>>
        {
            {"Bones", new Dictionary<string, int>{ {"Min", 370 },{"Max", 570 } } },
            {"Kidney", new Dictionary<string, int>{ {"Min", 100 },{"Max", 200 } } },
            {"Thyroid", new Dictionary<string, int>{ {"Min", 75 },{"Max", 185 } } },
            {"Liver", new Dictionary<string, int>{ {"Min", 75 },{"Max", 185 } } }
        };
        public List<string> nameResearch = new List<string>
        {
            {"Bones" },
            {"Kidney" },
            {"Thuroid" },
            {"Liver" }
        };

        public int CalculateNumberOfResearches(string name)
        {
            int index = 0;

            foreach(string nameReserch in nameResearch)
            {
                int value = 1;
                if(name == nameReserch)
                {
                    valueResearch[index] += value;
                }
            }
            return valueResearch[index];
        }

        public double RemainderMBK(double value)
        {
            double iloscMBK = 0;
            foreach(var name in weekDay.Patients)
            {
                iloscMBK += CalculateNumberOfResearches(name.Research) * radiationResearch[name.Research]["Min"];
            }
          return iloscMBK;
        }
        //Zrobione
        public int ArrivalDay(DateTime day)
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

        //Zrobione
        public double QuantityMbK(int day)
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
