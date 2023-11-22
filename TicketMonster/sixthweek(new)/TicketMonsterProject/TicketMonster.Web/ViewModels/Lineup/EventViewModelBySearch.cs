using TicketMonster.Infrastructure.Data;

namespace TicketMonster.Web.ViewModels.Lineup
{
	public class EventViewModelBySearch
	{
		public List<EventViewModel> Events { get; set; }
        public int? CategoryId { get; set; }
		public int? CategoryName { get; set; }
		public string KeyWord { get; set; }
		//public string? DateTime { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? SortType { get; set; }
        public int CurrentPage { get; set; }
		public int TotalPages { get; set; }
        public int PageSize { get; set; }
		public string Dates { get; set; }
    }


}
