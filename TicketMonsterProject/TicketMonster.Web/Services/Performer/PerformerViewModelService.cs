using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Logging;
using TicketMonster.ApplicationCore.Entities;
using TicketMonster.ApplicationCore.Interfaces.PerformerService;
using TicketMonster.ApplicationCore.Services;
using TicketMonster.Web.ViewModels.Home;
using TicketMonster.Web.ViewModels.Performer;

namespace TicketMonster.Web.Services.Performer
{
    public class PerformerViewModelService
    {
        private readonly PerformerService _performerService;
        private readonly IRepository<Venue> _venueRepo;

        public PerformerViewModelService(PerformerService performerService, IRepository<Venue> venueRepo)
        {
            _performerService = performerService;
            _venueRepo = venueRepo;
        }

        public async Task<PerformerPageViewModel> PerformerPage(int performerid, DateTime startTime, DateTime endTime)
        {
            var performer = await _performerService.GetPerformerById(performerid);
            var eventdetail = await _performerService.GetEventByPerformerIdSortByTime(performerid, startTime, endTime);
            var venue = await _venueRepo.ListAsync();
            var performerpic = await _performerService.GetPerformerPicByPerformerId(performerid);
            var performerhorizenpic = await _performerService.GetPerformerHorizenPicByPerformerId(performerid);
            var performercategory = await _performerService.GetPerformerCategory(performerid);
            var performercategoryid = await _performerService.GetPerformerCategoryId(performerid);
          

            PerformerPageViewModel viewModel = new PerformerPageViewModel()
            {
                StartTime = startTime,
                EndTime = endTime,
                PerformerId = performerid,
                PerformerName = performer.Name,
                AboutPerformer = performer.About,
                PerformerPic = performerhorizenpic.Pic,
                PerformerCategoryName = performercategory.performerCategory.CategoryName,
                PerformerDetails = new PerformerInfoViewModel
                {

                    PerformerName = performer.Name,
                    PerformerPic = performerpic.Pic,
                    PerformerCategory = performercategory.performerCategory.CategoryName,
                    PerformerSubCategory = performercategory.performerSubCategory.SubCategoryName,
                    PerformerCategoryId = performercategoryid,
                    PerformerSubCategoryId = performercategory.performerSubCategory.Id
                },
                EventDetailCards = eventdetail.Select(item => new EventDetailCardViewModel
                {
                    EventName = item.EventName,
                    Day = item.EventDate.Day,
                    Month = item.EventDate.ToString("MMM"),
                    Time = item.EventDate.ToString("ddd h:mm tt"),
                    Venue = item != null ? venue.FirstOrDefault(x => x.Id == item.VenueId)?.VenueName : string.Empty,
                    Location = item != null ? venue.FirstOrDefault(x => x.Id == item.VenueId)?.Location : string.Empty,
                    ID = item.Id
                }).ToList()
            };
            return viewModel;
        }
    }
}