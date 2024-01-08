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
                if(res == patient.Research)
                {
                    if(patient.MBK > research.radiationResearch[res]["Min"])
                    {
                        mbk = patient.MBK;
                    }
                    else
                    {
                        mbk = research.radiationResearch[res]["Min"];
                    }
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
                    if(name.MBK > research.radiationResearch[name.Research]["Min"])
                    {
                        valueMBK = name.MBK;
                    }
                    else
                    {
                        valueMBK += research.radiationResearch[name.Research]["Min"];

                    }
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
       
        public double CalculatedResult(Patient patient, WeekDay weekDay)
        {
            double result = patient.MBK;
            foreach (var res in research.radiationResearch)
            {
                if (patient.Research == res.Key)
                {
                    while(weekDay.RemainderMBK > 0)
                    {
                        if(result == res.Value["Max"])
                        {
                            result = res.Value["Max"];
                            break;
                        }
                        --weekDay.RemainderMBK;
                        result++;
                    }
                }
            }
            return Math.Round(result, 1, MidpointRounding.AwayFromZero);
        }
    }
}
