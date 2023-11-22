namespace TicketMonster.Web.ViewModels.Performer
{
    public class PerformerPageViewModel
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int PerformerId { get; set; }
        public string PerformerName { get; set; }
        public string AboutPerformer { get; set; }
        public string PerformerPic { get; set; }
        public List<EventDetailCardViewModel> EventDetailCards { get; set; }
        public PerformerInfoViewModel PerformerDetails { get; set; }
        public string PerformerCategoryName { get; set; }
    }
    public class EventDetailCardViewModel
    {
        public string Month { get; set; }
        public int Day { get; set; }
        public string Time { get; set; }
        public string Location { get; set; }
        public string Venue { get; set; }
        public string EventName { get; set; }
        public int ID { get; set; }
    }
    public class PerformerInfoViewModel
    {
        public string PerformerName { get; set; }
        public string PerformerPic { get; set; } 
        public string PerformerCategory { get; set; }
        public string PerformerSubCategory { get; set; }
        public int PerformerCategoryId { get; set; }
        public int PerformerSubCategoryId { get; set; }     
    }
}
