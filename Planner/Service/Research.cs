namespace Planner.Service
{
    public class Research
    {
        public Dictionary<string, Dictionary<string, int>> radiationResearch = new Dictionary<string, Dictionary<string, int>>
        {
            {"Bones", new Dictionary<string, int>{ {"Min", 370 },{"Max", 570 } } },
            {"Kidney", new Dictionary<string, int>{ {"Min", 100 },{"Max", 200 } } },
            {"Thyroid", new Dictionary<string, int>{ {"Min", 75 },{"Max", 185 } } },
            {"Liver", new Dictionary<string, int>{ {"Min", 75 },{"Max", 185 } } }
        };
        public List<string> nameResearch = new List<string>
        {
            {"Bones" },
            {"Kidney" },
            {"Thuroid" },
            {"Liver" }
        };
    }
}
