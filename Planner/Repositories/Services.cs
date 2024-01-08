using Microsoft.EntityFrameworkCore;
using Planner.Data;
using Planner.Models.Domain;
using Planner.Repositories.Interface;
using Planner.Service;

namespace Planner.Repositories
{
    public class Services : IServices
    {
        private readonly PlannerDbContext plannerDbContext;

        private CalculationMBK calculationMBK = new CalculationMBK();
        public Services(PlannerDbContext plannerDbContext)
        {
            this.plannerDbContext = plannerDbContext;
        }

      /*  public async Task<WeekDay> CalculatedResult(WeekDay weekDay)
        {
            var existingWeekDay = await plannerDbContext.WeekDays.FindAsync(weekDay.Id);
            if(existingWeekDay != null)
            {
                foreach(var patient in existingWeekDay.Patients)
                {
                    patient.MBK = calculationMBK.CalculatedResult(existingWeekDay);
                }
                await plannerDbContext.SaveChangesAsync();
                return existingWeekDay;
            }
            return null;
        }*/

        public Task<IEnumerable<WeekDay>> CloseDay(WeekDay weekDay)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<WeekDay>> GetHistoryAsync()
        {
            return await plannerDbContext.WeekDays
               .Where(weekDay => weekDay.Day < DateTime.Today)
               .OrderBy(weekDay => weekDay.Day)
              .Include(patient => patient.Patients)
              .ToListAsync();
        }

        public async Task<IEnumerable<WeekDay>> GetUpdMbK(WeekDay weekDay)
        {
            var existingWeekDay = await plannerDbContext.WeekDays.FindAsync(weekDay.Id);
            if (existingWeekDay != null)
            {
                existingWeekDay.ArrivalDay = weekDay.ArrivalDay;
                existingWeekDay.ActivityDay = weekDay.ActivityDay;
                existingWeekDay.QuantityMbK = weekDay.QuantityMbK;
                existingWeekDay.Day = weekDay.Day;
                existingWeekDay.RemainderMBK = calculationMBK.UpdRemainderMBK(weekDay);
                existingWeekDay.Patients = weekDay.Patients;
                await plannerDbContext.SaveChangesAsync();

                return (IEnumerable<WeekDay>)existingWeekDay;
            }
            return null;
        }

        public async Task<IEnumerable<Patient>> GetPatientForDay(Guid Id)
        {
            var patientForDay = await plannerDbContext.Patients.Where(patients => patients.WeekDayId == Id).ToListAsync();
            return patientForDay;
        }
        public async Task<IEnumerable<Patient>> DeletePatientsForDay(Guid Id)
        {
            var patientsForDay = await plannerDbContext.Patients
                .Where(patient => patient.WeekDayId == Id)
                .ToListAsync();
            if (patientsForDay.Count > 0)
            {
                foreach (var patient in patientsForDay)
                {
                    plannerDbContext.Patients.Remove(patient);
                }
                return patientsForDay;
            }
            return null;
        }

        public async Task<WeekDay> UpdateMBK (WeekDay weekDay)
        {
           
            var existingWeekDay = await plannerDbContext.WeekDays.FindAsync(weekDay.Id);
            if (existingWeekDay != null)
            {
                existingWeekDay.ArrivalDay = weekDay.ArrivalDay;
                existingWeekDay.ActivityDay = weekDay.ActivityDay;
                existingWeekDay.QuantityMbK = weekDay.QuantityMbK;
                existingWeekDay.Day = weekDay.Day;
                existingWeekDay.RemainderMBK = calculationMBK.UpdRemainderMBK(weekDay);
                existingWeekDay.Patients = weekDay.Patients;
                await plannerDbContext.SaveChangesAsync();

                return existingWeekDay;
            }
            return null;
            
        }

    }
}
