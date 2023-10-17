using Azure;
using Planner.Models.Domain;

namespace Planner.Repositories
{
    public interface IPatientRepository
    {
        Task<IEnumerable<Patient>> GetAllAsync(Guid Id);
        Task<Patient?> GetAsync(Guid id);
        Task<Patient> AddAsync(Patient patient);
        Task<Patient?> UpdateAsync(Patient patient);
        Task<Patient?> DeleteAsync(Guid id);
        Task<IEnumerable<Patient>> GetPatientForDay(Guid Id);
        Task<IEnumerable<Patient>> DeletePatientsForDay(Guid Id);
    }
}
