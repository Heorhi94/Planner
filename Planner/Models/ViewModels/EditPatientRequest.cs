namespace Planner.Models.ViewModels
{
    public class EditPatientRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDay { get; set; }
        public DateTime RegistrationDay { get; set; }
        public string Research { get; set; }
        public Guid PatientWeekDayId { get; set; }
        public List<string> NameResearchList { get; set; }
    }
}
