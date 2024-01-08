using Planner.Models.Domain;

namespace Planner.Repositories.Interface
{
    public interface IServices
    {
        Task<IEnumerable<WeekDay>> GetHistoryAsync();
        Task<IEnumerable<WeekDay>> GetUpdMbK(WeekDay weekDay);
      //  Task<WeekDay> CalculatedResult (WeekDay weekDay);
        Task<IEnumerable<WeekDay>> CloseDay(WeekDay weekDay);
        Task<IEnumerable<Patient>> GetPatientForDay(Guid Id);
        Task<IEnumerable<Patient>> DeletePatientsForDay(Guid Id);
        Task<WeekDay> UpdateMBK(WeekDay weekDay);
    }
}
