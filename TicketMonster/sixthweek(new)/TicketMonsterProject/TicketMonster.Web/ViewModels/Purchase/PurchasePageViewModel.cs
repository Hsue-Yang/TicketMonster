namespace TicketMonster.Web.ViewModels.Purchase
{
#nullable disable
	public class PurchasePageViewModel
	{
		public EventsViewModel EventsViewModel { get; set; }
		public VenuesViewModel VenuesViewModel { get; set; }
		public List<SeatSectionsViewModel> SeatSectionsViewModel { get; set; }
		public List<SeatNumsViewModel> SeatNumsViewModel { get; set; }
		public PerformersViewModel PerformersViewModel { get; set; }
		public Customer customer { get; set; }
	}
	public class EventsViewModel
	{
		public int ID { get; set; }
		public string EventName { get; set; }
		public DateTime EventDate { get; set; }
		public decimal TotalTime { get; set; }
		public int CategoryID { get; set; }
		public int VenueID { get; set; }
		public int SubCategoryID { get; set; }
		public string EventPic { get; set; }
	}
	public class VenuesViewModel
	{
		public int ID { get; set; }
		public string VenueName { get; set; }
		public string Location { get; set; }
		//這邊有修改
		public string Capacity { get; set; }
		public string Pic { get; set; }
		public int SectionNum { get; set; }
	}

	public class PerformersViewModel
	{
		public List<int> ID { get; set; }
		public List<string> Name { get; set; }
		public List<string> About { get; set; }
		public string PerformerPic { get; set; }
	}

	public class SeatSectionsViewModel
	{
		public int ID { get; set; }
		public int VenueID { get; set; }
		public string SectionName { get; set; }
		public decimal SectionPrice { get; set; }
		public int SectionCapacity { get; set; }
	}
	public class SeatNumsViewModel
	{
		public int SeatSectionID { get; set; }
		public string SeatNum { get; set; }
		public DateTime RetainTime { get; set; }
		public bool IsOrdered { get; set; }

	}
}
