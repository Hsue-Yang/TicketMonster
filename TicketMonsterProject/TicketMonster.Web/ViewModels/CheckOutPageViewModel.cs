using Microsoft.Identity.Client;

namespace TicketMonster.Web.ViewModels
{
    public class CheckOutPageViewModel
    {
        public List<string> Pic { get; set; }//在EventPic表格內的Pic
        public string EventName { get; set; }//在Events表格內的EventName
        public DateTime EventDate { get; set; }//在Events表格內的EventDate
        public string Venue { get; set; }//在Venues表格中的VenueName     
    }
}