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


        public List<Patient> CalculatedResult(WeekDay weekDay)
        {
            var patients = weekDay.Patients.ToList();
            int researchesCount = patients.Count;
            double addMBK = weekDay.RemainderMBK / researchesCount;
            double restMBK = 0;

            // Первоначальное распределение MBK
            foreach (var patient in patients)
            {
                patient.MBK += addMBK;
                patient.MBK = Math.Round(patient.MBK,2, MidpointRounding.AwayFromZero);
            }

            // Корректировка MBK, если оно превышает Max значение
            foreach (var patient in patients)
            {
                if (research.radiationResearch.TryGetValue(patient.Research, out var res))
                {
                    double maxMBK = res["Max"];
                    if (patient.MBK > maxMBK)
                    {
                       restMBK = restMBK + patient.MBK - maxMBK;
                       patient.MBK = maxMBK;
                    }
                }
            }

            // Повторное распределение оставшегося MBK
            if (restMBK > 0)
            {
                addMBK = restMBK / researchesCount;
                restMBK = 0;
                foreach (var patient in patients)
                {
                    if (research.radiationResearch.TryGetValue(patient.Research, out var res))
                    {
                        double maxMBK = res["Max"];
                        if (patient.MBK < maxMBK)
                        {
                            patient.MBK += addMBK;
                            patient.MBK = Math.Round(patient.MBK,2, MidpointRounding.AwayFromZero);
                            if (patient.MBK > maxMBK)
                            {
                                restMBK += patient.MBK - maxMBK;
                                patient.MBK = maxMBK;
                            }
                        }
                    }
                }
            }

            return patients;
        }

        /*
                public double CalculatedResult(Patient patient, WeekDay weekDay)
                {
                    double result = patient.MBK;
                    int researches = weekDay.Patients.Count;
                    double addMBK = weekDay.RemainderMBK / researches;
                    foreach (var res in research.radiationResearch)
                    {
                        if (patient.Research == res.Key)
                        {
                            while (weekDay.RemainderMBK > 0)
                            {
                                if (result == res.Value["Max"])
                                {
                                    result = res.Value["Max"];
                                    break;
                                }
                                weekDay.RemainderMBK--;
                                result++;
                            }
                        }
                    }
                    return Math.Round(result, 1, MidpointRounding.AwayFromZero);
                }*/


    }
}
