namespace TicketMonster.Web.ViewModels.Lineup
{
    public class MixViewModel
    {
        public List<EventViewModel> Events { get; set; }

        // 不需要,寫在 Layout
        //public List<CategoryViewModel> Categories { get; set; }
        public List<SubCategoryViewModel> SubCategories { get; set; }

    }
}
