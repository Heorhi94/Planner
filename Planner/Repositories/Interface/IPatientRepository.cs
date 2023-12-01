using Planner.Models.Domain;

namespace Planner.Repositories.Interface
{
    public interface IPatientRepository
    {
        Task<IEnumerable<Patient>> GetAllAsync(Guid Id);
        Task<Patient?> GetAsync(Guid id);
        Task<Patient> AddAsync(Patient patient);
        Task<Patient?> UpdateAsync(Patient patient);
        Task<Patient?> DeleteAsync(Guid id);

    }
}
