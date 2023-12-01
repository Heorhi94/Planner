using Planner.Models.Domain;

namespace Planner.Repositories.Interface
{
    public interface IAnaliticsRepository
    {
        Task<IEnumerable<Patient>> GetPatient (DateTime startDate, DateTime endDate);
        Task<IEnumerable<Patient>> GetPatientResearch (DateTime startDate, DateTime endDate, string research);
        Task<IEnumerable<Patient>> GetPatient();

        //Добавить методы  среднеее значение MBK на исследование за период времени два box для выбора даты и олин для выбора исследования (dateTime startDay, dateTime endDay)
        //Количество исследований за период времени аналогично комменту выше
    }
}
