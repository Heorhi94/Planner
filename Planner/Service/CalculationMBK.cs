using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Planner.Models.Domain;

namespace Planner.Service
{
    public class CalculationMBK
    {

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
            {"Thuroid", new Dictionary<string, int>{ {"Min", 75 },{"Max", 185 } } },
            {"Liver", new Dictionary<string, int>{ {"Min", 75 },{"Max", 185 } } }
        };

        public Dictionary<string,int> nameResearch = new Dictionary<string, int>
        {
            {"Bones", 0},
            {"Kidney", 0},
            {"Thuroid", 0},
            {"Liver", 0}
        };

        public Dictionary<string,int> CalculateNumberOfResearches(string name)
        {
            int index = 0;
            foreach(string nameReserch in nameResearch.Keys)
            {
                int value = 1;
                if(name == nameReserch)
                {
                    nameResearch[name] += value;
                }
            }
            return nameResearch;
        }

        public double PatientMbK(WeekDay weekDay)
        {
            double mbk = 0;
            foreach (var name in weekDay.Patients)
            {
                mbk = radiationResearch[name.Research]["Min"];
            }
            return mbk;
        }

        public double RemainderMBK(WeekDay weekDay)
        {
            double iloscMBK = 0;
            if(weekDay.Patients != null)
            {
                foreach (var name in weekDay.Patients)
                {
                    iloscMBK += radiationResearch[name.Research]["Min"];
                }
                return weekDay.QuantityMbK - iloscMBK;
            }
            else
            {
                return weekDay.QuantityMbK;
            }
           
        }
        public double UpdRemainderMBK(WeekDay weekDay)
        {
            double iloscMBK = 0;
            if (weekDay.Patients != null)
            {
                foreach (var patient in weekDay.Patients)
                {
                    iloscMBK += patient.MBK;
                }
                return weekDay.QuantityMbK - iloscMBK;
            }
            else
            {
                return weekDay.QuantityMbK;
            }

        }
        public DateTime ArrivalDay(DateTime date)
        {
            if(date != new DateTime(2023, 10, 3))
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
        
        public double QuantityMbK(int day)
        {
            string key = day.ToString();

            if (RadiationsDay.ContainsKey(key))
            {
                return RadiationsDay[key];
            }
            return 0; 
        }

        public double CalculatedResult(WeekDay weekDay)
        {
            double result = 0;
            if (weekDay.QuantityMbK != 0)
            {
                var researc = weekDay.Patients.Count;
                var remainder = weekDay.QuantityMbK / researc;
                foreach (var people in weekDay.Patients)
                {

                    result = people.MBK + remainder;
                    foreach(var res in radiationResearch)
                    {
                        if(people.Research == res.Key)
                        {
                            if (people.MBK > res.Value[people.Research])

                            {
                                result = people.MBK - remainder;
                            }
                        }
                    }
                }
            }
            return result;
        }
    }
}
