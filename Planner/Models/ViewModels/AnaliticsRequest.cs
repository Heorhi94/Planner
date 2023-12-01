using Planner.Models.Domain;

namespace Planner.Models.ViewModels
{
    public class AnaliticsRequest
    {
        public DateTime StartDay { get; set; }
        public DateTime EndDay { get; set; }
        public string Research { get; set; }
        public List<string> NameResearchList { get; set; }
        public double AvgMBK { get; set; }

        public Dictionary <string,int> CountResearch { get; set; }
    }
}
