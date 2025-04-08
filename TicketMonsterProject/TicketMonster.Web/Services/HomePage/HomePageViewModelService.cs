using TicketMonster.ApplicationCore.Interfaces.PerformerService;
using TicketMonster.Web.ViewModels.Home;
using TicketMonster.ApplicationCore.Services;
using TicketMonster.ApplicationCore.Services.Lineup;
using TicketMonster.Web.Interfaces;

namespace TicketMonster.Web.Services.Home
{
    public class HomePageViewModelService : IHomePageViewModelService
    {
        private readonly IPerformerService _performerService;
        private readonly CategoryService _categoryService;
        private readonly EventService _eventService;

        public HomePageViewModelService(EventService eventService, IPerformerService performerService, CategoryService categoryService)
        {
            _performerService = performerService;
            _categoryService = categoryService;
            _eventService = eventService;
        }

        public async Task<HomePageViewModel> GetHomepageViewModel()
        {
            var performerdata = await _performerService.GetPerformerData();
            var category = await _categoryService.CategoryList();
            var subcategory = await _categoryService.SubCategoryList();
            var categoryname = category.Select(c => c.CategoryName).ToList();

            var categorys = new List<CategoryCardViewModel>();
            var subcategorys = new List<CategoryCardViewModel>();
            var slider = new List<SliderViewModel>();

            var eventperformer = await _eventService.GetRandomEventPic(8);

            foreach (var item in eventperformer)
            {
                var vm = new SliderViewModel
                {
                    EventId = item.EventId,
                    EventPic = item.Pic,
                };
                slider.Add(vm);
            };

            var performerCard = performerdata.PerformerList.Select(p =>
            {
                var performerPic = performerdata.PerformerPic.FirstOrDefault(pp => pp.PerfomerId == p.Id);
                var eventCount = performerdata.EventPerformList.Count(e => e.PerfomerId == p.Id);
                var performerCate = performerdata.CategoryList.FirstOrDefault(c => c.Id == p.CategoryId)?.CategoryName;
                var performerCateId = performerdata.CategoryList.FirstOrDefault(c => c.Id == p.CategoryId).Id;

                return new PerformerCardViewModel
                {
                    PerformerName = p.Name,
                    PerformerPic = performerPic?.Pic ?? null,
                    PerformerEventCount = eventCount,
                    CategoryName = performerCate,
                    CategoryId = performerCateId,
                    PerformerId = p.Id
                };

            }).ToList();

            var categoryViewModels = performerdata.CategoryList.Select(c =>
            {
                return new SearchBoxPartialViewModel
                {
                    CategoryId = c.Id,
                    CategoryName = c.CategoryName
                };
            }).ToList();


            foreach (var item in category)
            {
                var vm = new CategoryCardViewModel
                {
                    CategoryId = item.Id,
                    CategoryName = item.CategoryName,
                    CategoryPic = item.Pic
                };
                categorys.Add(vm);
            };

            foreach (var item in subcategory)
            {
                var vm = new CategoryCardViewModel
                {
                    CategoryId = item.CatagoryId,
                    CategoryName = item.SubCategoryName,
                    CategoryPic = item.Pic,
                    SubCategoryId = item.Id

                };
                subcategorys.Add(vm);
            };

            HomePageViewModel viewModel = new HomePageViewModel
            {
                HomePageCategoryCard = new WidgetPartialViewModel
                {
                    PerformerCards = performerCard,
                    CategoryName = performerCard.Select(p => p.CategoryName),
                    CategoryId = performerCard.Select(p => p.CategoryId)
                },
                SearchBoxDropDown = new SearchBoxDropDownViewModel
                {
                    Category = categoryViewModels
                },
                CategoryCard = categorys,
                SubCategoryCard = subcategorys,
                ShowSliderPic = slider
            };
            return viewModel;
        }
    }
};

