using Microsoft.EntityFrameworkCore;
using Planner.Models.Domain;

namespace Planner.Data
{
    public class PlannerDbContext : DbContext
    {
        public PlannerDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<WeekDay> WeekDays { get; set; }
        public DbSet<Patient> Patients { get; set; }
    }
}
