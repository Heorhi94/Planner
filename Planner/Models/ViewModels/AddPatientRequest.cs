namespace Planner.Models.ViewModels
{
    public class AddPatientRequest
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime RegistrationDay { get; set; }
        public string Research { get; set; }
    }
}
