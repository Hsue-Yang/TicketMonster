using TicketMonster.Web.ViewModels.Home;

namespace TicketMonster.Web.ViewModels.Home
{
    public class CategoryViewModel
    {
        public string CategoryName { get; set; }
        public List<PerformerCardViewModel> PerformerCards { get; set; }
    }
}
