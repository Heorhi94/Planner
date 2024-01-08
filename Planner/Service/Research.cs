namespace Planner.Service
{
    public class Research
    {
        public List<int> valueResearch = new List<int>
       {
           {0 },
           {0 },
           {0 },
           {0 }
       };

        public Dictionary<string, Dictionary<string, int>> radiationResearch = new Dictionary<string, Dictionary<string, int>>
        {
            {"Bones", new Dictionary<string, int>{ {"Min", 370 },{"Max", 570 } } },
            {"Kidney", new Dictionary<string, int>{ {"Min", 100 },{"Max", 200 } } },
            {"Thyroid", new Dictionary<string, int>{ {"Min", 75 },{"Max", 185 } } },
            {"Liver", new Dictionary<string, int>{ {"Min", 75 },{"Max", 185 } } }
        };

        public Dictionary<string, int> nameResearch = new Dictionary<string, int>
        {
            {"Bones", 0},
            {"Kidney", 0},
            {"Thyroid", 0},
            {"Liver", 0}
        };


        public Dictionary<string, int> CalculateNumberOfResearches(string name)
        {
            foreach (string nameReserch in nameResearch.Keys)
            {
                int value = 1;
                nameResearch[name] += name == nameReserch ? value : 0;
            }
            return nameResearch;
        }
    }
}
