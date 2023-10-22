using Planner.Models.Domain;

namespace Planner.Models.ViewModels
{
    public class AddPatientRequest
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime RegistrationDay { get; set; }
        public string Research { get; set; }
        public Guid PatientWeekDayId { get; set; }
        public List<string> NameResearchList { get; set; }
        public double QuantityMBK { get; set; }


    }
}
