using Microsoft.EntityFrameworkCore;
using Planner.Data;
using Planner.Models.Domain;
using Planner.Repositories.Interface;
using Planner.Service;

namespace Planner.Repositories
{
    public class WeekDayServices : IWeekDayServices
    {
        private readonly PlannerDbContext plannerDbContext;

        public WeekDayServices(PlannerDbContext plannerDbContext)
        {
            this.plannerDbContext = plannerDbContext;
        }
        public async Task<IEnumerable<WeekDay>> GetAnalitics()
        {
            return await plannerDbContext.WeekDays
              .Where(weekDay => weekDay.Day < DateTime.Today)
              .OrderBy(weekDay => weekDay.Day)
             .Include(patient => patient.Patients)
             .ToListAsync();
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
            CalculationMBK calculationMBK = new CalculationMBK();
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
    }
}
