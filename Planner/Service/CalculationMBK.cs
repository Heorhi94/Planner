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

            foreach (var res in research.nameResearch.Keys)
            {
                if (res == patient.Research)
                {
                    mbk = research.radiationResearch[res]["Min"];
                }
            }


            return mbk;
        }
        public double RemainderMBK(WeekDay weekDay)
        {
            double valueMBK = 0;
            if (weekDay.Patients != null)
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
        public double CalculatedRemainderMBK(WeekDay weekDay)
        {
            double valueMBK = 0;
            if (weekDay.Patients != null)
            {
                foreach (var name in weekDay.Patients)
                {
                    valueMBK += name.MBK;
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
                return Math.Round(weekDay.QuantityMbK - iloscMBK, 1, MidpointRounding.AwayFromZero);
            }
            else
            {
                return Math.Round(weekDay.QuantityMbK, 1, MidpointRounding.AwayFromZero);
            }

        }
        /* public double CalculatedResult(Patient patient)
         {
             double result = 0;
             if (weekDay.RemainderMBK != 0)
             {
                 var countResearcs = weekDay.Patients.Count;
                 var remainder = weekDay.RemainderMBK / countResearcs;
                 foreach (var people in weekDay.Patients)
                 {
                   //  result = people.MBK + remainder;
                     foreach(var res in research.radiationResearch)
                     {
                         if(people.Research == res.Key)
                         {
                             result = people.MBK + remainder;
                             weekDay.RemainderMBK -= remainder;
                             if (result > res.Value["Max"])
                             {
                                 result = people.MBK - remainder;
                                 return result;
                             }
                             return result;
                         }
                     }
                 }
             }
             return result;
         }*/
        public double CalculatedResult(Patient patient, WeekDay weekDay)
        {
            double result = 0;
            double remainder = weekDay.RemainderMBK / weekDay.Patients.Count;  
            foreach (var res in research.radiationResearch)
            {
                if (patient.Research == res.Key)
                {
                    result = patient.MBK + remainder;
                    weekDay.RemainderMBK -= remainder;
                    if(weekDay.RemainderMBK < 0)
                    {
                        weekDay.RemainderMBK += remainder;
                        result = patient.MBK - remainder;
                        return Math.Round(result, 1, MidpointRounding.AwayFromZero);
                    }
                    if (result > res.Value["Max"])
                    {
                        result = patient.MBK - remainder;
                        return Math.Round(result, 1, MidpointRounding.AwayFromZero);
                    }
                    return Math.Round(result, 1, MidpointRounding.AwayFromZero);
                }
            }
            return Math.Round(result, 1, MidpointRounding.AwayFromZero);
        }
    }
}
