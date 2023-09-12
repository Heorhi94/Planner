using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Planner.Models.Domain;

namespace Planner.Service
{
    public class CalculationMBK
    {
        Patient patient;
        Research research;
        WeekDay weekDay;
        
        public List<int> valueResearch = new List<int>
       {
           {0 },
           {0 },
           {0 },
           {0 }
       };
        public int CalculateNumberOfResearches(string name)
        {

            int index = 0;

            foreach(string nameReserch in research.nameResearch)
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
                iloscMBK += CalculateNumberOfResearches(name.Research) * research.radiationResearch[name.Research]["Max"];
            }
          return iloscMBK;
            
            
        } 

    }
}
