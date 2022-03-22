namespace DATACCESS.ViewModels
{
    public class SituationAgentValueListItemViewModel
    {
        public string id { get; set; }
        public string description { get; set; }

        public int duree_max { get; set; }
        public int duree_min { get; set; }
        public bool reduce_planning_days { get; set; }
    }
}