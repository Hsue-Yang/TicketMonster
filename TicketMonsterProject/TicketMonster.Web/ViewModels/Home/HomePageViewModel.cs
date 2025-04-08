

namespace TicketMonster.Web.ViewModels.Home
{
    public class HomePageViewModel
    {
        public WidgetPartialViewModel HomePageCategoryCard { get; set; } 
        public SearchBoxDropDownViewModel SearchBoxDropDown { get; set; }     
        public List<SliderViewModel> ShowSliderPic { get; set; }
        public List<CategoryCardViewModel> CategoryCard { get; set; }
        public List<CategoryCardViewModel> SubCategoryCard { get; set; }
    }

    public class WidgetPartialViewModel
    {
        public List<PerformerCardViewModel> PerformerCards { get; set; }
        public IEnumerable<string> CategoryName { get; set; }
        public IEnumerable<int> CategoryId { get; set; }     
    }
    public class PerformerCardViewModel
    {
        public string PerformerName { get; set; }
        public string PerformerPic { get; set; }
        public int PerformerEventCount { get; set; }
        public string CategoryName { get; set; }
        public int PerformerId { get; set; }
        public int CategoryId { get; set; }        
    }
    public class SliderViewModel
    {
        public string EventPic { get; set; }
        public int EventId { get; set; } 
    }

    public class  CategoryCardViewModel
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set;}
        public string CategoryPic { get; set; }
        public int SubCategoryId { get; set; }       
    }

    public class SearchBoxDropDownViewModel
    {
        public List<SearchBoxPartialViewModel> Category { get; set; }
    }
    public class SearchBoxPartialViewModel
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }

}
