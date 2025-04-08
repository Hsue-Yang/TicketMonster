namespace TicketMonster.Admin.Models.Create
{
    public class EventAndPicDto
    {
        public string EventName { get; set; }
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public int VenueId { get; set; }
        public string EventDate { get; set; }
        public int TotalTime { get; set; }
        public bool IsDeleted { get; set; }
        public string CreateBy { get; set; }
        public string LastEditBy { get; set; }
        public string Pic { get; set; }
        public int Sort { get; set; }
        public List<PerformersList> PerformersList { get; set; }
        public List<SeatList> Sections { get; set; }
        public List<EventsPicList> Images { get; set; }
    }

    public class PerformersList
    {
        public int ID { get; set; }
    }

    public class SeatList
    {
        public string SectionName { get; set; }
        public int SectionPrice { get; set; }
        public int SectionCapacity { get; set; }
    }

    public class EventsPicList
    {
        public string Pic { get; set; }
        public int Sort { get; set; }
    }
}




