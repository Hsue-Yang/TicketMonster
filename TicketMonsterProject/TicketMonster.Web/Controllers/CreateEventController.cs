using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketMonster.Infrastructure.Data;
using TicketMonster.Web.ViewModels.Purchase;

namespace TicketMonster.Web.Controllers
{

    public class CreateEventController : Controller
    {
        protected readonly TicketMonsterContext DbContext;

        public CreateEventController(TicketMonsterContext dbContext)
        {
            DbContext = dbContext;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            var purchasePageViewModel = new PurchasePageViewModel();

            return View(purchasePageViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PurchasePageViewModel purchasePageViewModel)
        {
            if (!ModelState.IsValid)
            {

                //創建場館

                var newVenue = new Venue
                {

                    Capacity = purchasePageViewModel.VenuesViewModel.Capacity,
                    Location = purchasePageViewModel.VenuesViewModel.Location,
                    VenueName = purchasePageViewModel.VenuesViewModel.VenueName,
                    Pic = purchasePageViewModel.VenuesViewModel.Pic,
                    SectionNum = purchasePageViewModel.VenuesViewModel.SectionNum
                };
                DbContext.Venues.Add(newVenue);
                await DbContext.SaveChangesAsync();
                //創建活動
                var newEvent = new Event
                {
                    VenueId = newVenue.Id,
                    EventName = purchasePageViewModel.EventsViewModel.EventName,
                    EventDate = purchasePageViewModel.EventsViewModel.EventDate,
                    CategoryId = purchasePageViewModel.EventsViewModel.CategoryID,
                    SubCategoryId = purchasePageViewModel.EventsViewModel.SubCategoryID,
                    TotalTime = purchasePageViewModel.EventsViewModel.TotalTime,
                };
                DbContext.Events.Add(newEvent);
                await DbContext.SaveChangesAsync();

                //var num = addViewModel.VenueViewModel.SectionNum;
                //自動生成區域和座位資料
                for (int i = 1; i <= purchasePageViewModel.VenuesViewModel.SectionNum; i++)
                {
                    //關聯區塊與場館
                    //addViewModel.SeatSectionsViewModel.VenueID = newEvent.VenueId;
                    var sectionName = $"Section {i}";
                    var seatSec = new SeatSection
                    {
                        //Id = i,
                        EventId = newEvent.Id,
                        VenueId = newEvent.VenueId,
                        SectionName = sectionName,
                        SectionPrice = purchasePageViewModel.SeatSectionsViewModel[i - 1].SectionPrice,
                        SectionCapacity = purchasePageViewModel.SeatSectionsViewModel[i - 1].SectionCapacity,
                    };
                    DbContext.SeatSections.Add(seatSec);
                    await DbContext.SaveChangesAsync();

                    //自動生成座位數量
                    for (int z = 1; z <= purchasePageViewModel.SeatSectionsViewModel[i - 1].SectionCapacity; z++)
                    {
                        var seatNum = new SeatNum
                        {
                            VenueId = newEvent.VenueId,
                            SeatSectionId = seatSec.Id,
                            SeatNum1 = z.ToString(),
                            IsOrdered = false,
                            
                            RetainTime = DateTime.Now,
                        };
                        DbContext.SeatNums.Add(seatNum);
                    }
                    await DbContext.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Create));
            }
            return View(purchasePageViewModel);
        }
    }
}
