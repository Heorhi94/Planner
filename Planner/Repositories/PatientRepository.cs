using Azure;
using Microsoft.EntityFrameworkCore;
using Planner.Data;
using Planner.Models.Domain;
using Planner.Repositories.Interface;

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
            await plannerDbContext.SaveChangesAsync(); 
            return patient;
        }

        public async Task<Patient?> DeleteAsync(Guid id)
        {
            var existingPatient = await plannerDbContext.Patients.FindAsync(id);
            if(existingPatient != null)
            {
                plannerDbContext.Patients.Remove(existingPatient);
                await plannerDbContext.SaveChangesAsync();
                return existingPatient;
            }
            return null;
        }


        public async Task<IEnumerable<Patient>> DeletePatientsForDay(Guid Id)
        {
            var patientsForDay = await plannerDbContext.Patients
                .Where(patient => patient.WeekDayId == Id)
                .ToListAsync();
            if (patientsForDay.Count > 0)
            {
                foreach (var patient in patientsForDay)
                {
                    plannerDbContext.Patients.Remove(patient);
                }
                return patientsForDay;
            }
            return null;
        }

        public async Task<IEnumerable<Patient>> GetAllAsync(Guid Id)
        {
            return await plannerDbContext.Patients.ToListAsync();
        }

        public async Task<Patient?> GetAsync(Guid id)
        {
            return await plannerDbContext.Patients.FirstOrDefaultAsync(x=>x.Id == id);
        }

        public async Task<IEnumerable<Patient>> GetPatientForDay(Guid Id)
        {
            var patientForDay = await plannerDbContext.Patients.Where(patients => patients.WeekDayId == Id).ToListAsync();
            return patientForDay;
        }

        public async Task<Patient?> UpdateAsync(Patient patient)
        {
            var existingPatient = await plannerDbContext.Patients.FindAsync(patient.Id);
            if(existingPatient != null)
            {
                existingPatient.Name = patient.Name;
                existingPatient.Surname = patient.Surname;
                existingPatient.Research = patient.Research;
                existingPatient.RegistrationDay = patient.RegistrationDay;

                await plannerDbContext.SaveChangesAsync();

                return existingPatient;
            }
            return null;
        }
    }
}
