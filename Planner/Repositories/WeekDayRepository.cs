using Microsoft.EntityFrameworkCore;
using Planner.Data;
using Planner.Models.Domain;
using Planner.Repositories.Interface;
using Planner.Service;

namespace Planner.Repositories
{
    public class WeekDayRepository : IWeekDayRepository
    {
        private readonly PlannerDbContext plannerDbContext;
        private CalculationMBK calculationMBK = new CalculationMBK();
        
        public WeekDayRepository(PlannerDbContext plannerDbContext)
        {
            this.plannerDbContext = plannerDbContext;
        }

        public async Task<WeekDay> AddAsync(WeekDay weekDay)
        {
            await plannerDbContext.AddAsync(weekDay);
            await plannerDbContext.SaveChangesAsync();
            return weekDay;
        }
        public async Task<WeekDay?> DeleteAsync(Guid id)
        {
            var existingWeekDay = await plannerDbContext.WeekDays.FindAsync(id);
            
            if (existingWeekDay != null)
            {
                var patientsForDay = await plannerDbContext.Patients
              .Where(patient => patient.WeekDayId == existingWeekDay.Id)
              .ToListAsync();
                if (patientsForDay.Count > 0)
                {
                    foreach (var patient in patientsForDay)
                    {
                        plannerDbContext.Patients.Remove(patient);
                    }
                }
                plannerDbContext.WeekDays.Remove(existingWeekDay);
                await plannerDbContext.SaveChangesAsync();
                return existingWeekDay;
            }
            return null; 
        }

        public async Task<IEnumerable<WeekDay>> GetAllAsync()
        {
            return await plannerDbContext.WeekDays
                .Where(weekDay => weekDay.Day >= DateTime.Today)
                .OrderBy(weekDay => weekDay.Day)
               .Include(patient => patient.Patients) 
               .ToListAsync();
        }

        public async Task<WeekDay?> GetAsync(Guid id)
        {
            return await plannerDbContext.WeekDays.Include(x => x.Patients).FirstOrDefaultAsync(x => x.Id == id);
        }


        public async Task<WeekDay?> UpdateAsync(WeekDay weekDay)
        {
            var existingWeekDay = await plannerDbContext.WeekDays.FindAsync(weekDay.Id);
            if (existingWeekDay != null)
            {
                existingWeekDay.ArrivalDay = weekDay.ArrivalDay;
                existingWeekDay.ActivityDay = weekDay.ActivityDay;
                existingWeekDay.QuantityMbK = weekDay.QuantityMbK;
                existingWeekDay.Day = weekDay.Day;
                existingWeekDay.RemainderMBK = calculationMBK.RemainderMBK(weekDay);
                existingWeekDay.Patients = weekDay.Patients;
                await plannerDbContext.SaveChangesAsync();

                return existingWeekDay;
            }
            return null;
        }
    }
}
