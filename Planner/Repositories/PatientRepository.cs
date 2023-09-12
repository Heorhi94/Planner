using Azure;
using Microsoft.EntityFrameworkCore;
using Planner.Data;
using Planner.Models.Domain;

namespace Planner.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly PlannerDbContext plannerDbContext;

        public PatientRepository (PlannerDbContext plannerDbContext)
        {
            this.plannerDbContext = plannerDbContext;
        }

        public async Task<Patient> AddAsync(Patient patient)
        {
            await plannerDbContext.Patients.AddAsync(patient);
            await plannerDbContext.SaveChangesAsync(); // без этой команды не будут сохраняться изменеия в базе данных
            return patient;
        }

        public Task<Patient?> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Patient>> GetAllAsync()
        {
            return await plannerDbContext.Patients.ToListAsync();
        }

        public Task<Patient?> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Patient?> UpdateAsync(Patient patient)
        {
            throw new NotImplementedException();
        }
    }
}
