using Microsoft.EntityFrameworkCore;
using Planner.Data;
using Planner.Models.Domain;

namespace Planner.Repositories
{
    public class WeekDayRepository : IWeekDayRepository
    {
        private readonly PlannerDbContext plannerDbContext;

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

        public Task<WeekDay?> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<WeekDay>> GetAllAsync()
        {
            return await plannerDbContext.WeekDays.Include(x=>x.Patients).ToListAsync();
        }

        public Task<WeekDay?> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<WeekDay?> UpdateAsync(WeekDay weekDay)
        {
            var existingWeekDay = await plannerDbContext.WeekDays.FindAsync(weekDay.Id);
            if (existingWeekDay != null)
            {
                existingWeekDay.ArriviaDay = weekDay.ArriviaDay;
                existingWeekDay.ActivityDay = weekDay.ActivityDay;
                existingWeekDay.QuantityMbK = weekDay.QuantityMbK;
                await plannerDbContext.SaveChangesAsync();

                return existingWeekDay;
            }
            return null;
        }
    }
}
