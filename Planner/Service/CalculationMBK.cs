using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Planner.Models.Domain;

namespace Planner.Service
{
    public class CalculationMBK
    {
        
       private Research research = new Research();

        public double PatientMbK(Patient patient)
        {
            double mbk = 0;
            
            foreach(var res in research.nameResearch.Keys)
            {
                if(res == patient.Research)
                {
                    mbk = research.radiationResearch[res]["Min"];
                }
            }
                
            
            return mbk;
        }
        public double RemainderMBK(WeekDay weekDay)
        {
            double valueMBK = 0;
            if(weekDay.Patients != null)
            {
                foreach (var name in weekDay.Patients)
                {
                    valueMBK += research.radiationResearch[name.Research]["Min"];
                }
                return weekDay.QuantityMbK - valueMBK;
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
                    foreach(var res in research.radiationResearch)
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
