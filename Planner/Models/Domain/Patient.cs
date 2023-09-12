namespace Planner.Models.Domain
{
    public class Patient
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime RegistrationDay { get; set; }
        public string Research { get; set; }
        public ICollection<WeekDay> WeekDays { get; set; }

    }
}
