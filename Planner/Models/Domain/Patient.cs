namespace Planner.Models.Domain
{
    public class Patient
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDay { get; set; }
        public DateTime RegistrationDay { get; set; }
        public double MBK { get; set; }
        public string Research { get; set; }
        public Guid WeekDayId { get; set; }
        public WeekDay WeekDays { get; set; }

    }
}
