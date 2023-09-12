using Planner.Models.Domain;

namespace Planner.Repositories
{
    public class WeekDayRepository : IWeekDayRepository
    {
        public Task<WeekDay> AddAsync(WeekDay weekDay)
        {
            throw new NotImplementedException();
        }

        public Task<WeekDay?> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<WeekDay>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<WeekDay?> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<WeekDay?> UpdateAsync(WeekDay weekDay)
        {
            throw new NotImplementedException();
        }
    }
}
