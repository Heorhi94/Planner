using Planner.Models.Domain;

namespace Planner.Repositories.Interface
{
    public interface IWeekDayServices
    {
        Task<IEnumerable<WeekDay>> GetHistoryAsync();
        Task<IEnumerable<WeekDay>> GetAnalitics();
        Task<IEnumerable<WeekDay>> GetUpdMbK(WeekDay weekDay);
    }
}
