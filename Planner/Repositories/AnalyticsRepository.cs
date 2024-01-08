
using Microsoft.EntityFrameworkCore;
using Planner.Data;
using Planner.Models.Domain;
using Planner.Repositories.Interface;

namespace Planner.Repositories
{
    public class AnalyticsRepository : IAnalyticsRepository
    {
        private readonly PlannerDbContext plannerDbContext;

        public AnalyticsRepository(PlannerDbContext plannerDbContext)
        {
            this.plannerDbContext = plannerDbContext;
        }

        public async Task<IEnumerable<Patient>> GetPatientResearch (DateTime startDate, DateTime endDate, string research)
        {
            return await plannerDbContext.Patients
            .Where(patient => patient.RegistrationDay >= startDate && patient.RegistrationDay <= endDate && patient.Research == research)
             .ToListAsync();
        }

        public async Task<IEnumerable<Patient>> GetPatient (DateTime startDate, DateTime endDate)
        {
            return await plannerDbContext.Patients
                .Where(patient => patient.RegistrationDay >= startDate && patient.RegistrationDay <= endDate)
                 .ToListAsync();
        }
        public async Task<IEnumerable<Patient>> GetPatient()
        {
            return await plannerDbContext.Patients.ToListAsync();
        }

     
    }
}
