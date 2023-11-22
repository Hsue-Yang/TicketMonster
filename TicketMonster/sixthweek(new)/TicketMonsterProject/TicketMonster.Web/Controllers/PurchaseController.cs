using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.DependencyResolver;
using System.Security.Policy;
using TicketMonster.ApplicationCore.Entities;
using TicketMonster.ApplicationCore.Interfaces.EventService;
using TicketMonster.ApplicationCore.Interfaces.PurchaseService;
using TicketMonster.Infrastructure.Data;
using TicketMonster.Web.Services;
using TicketMonster.Web.Services.Cms;
using TicketMonster.Web.ViewModels.Purchase;

namespace TicketMonster.Web.Controllers
{

    public class PurchaseController : Controller
    {



        private readonly IPurchaseVMService _ipurchaseVMService;


        public PurchaseController(IPurchaseVMService ipurchaseVMService, IEventService eventService)
        {
            _ipurchaseVMService = ipurchaseVMService;

        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Purchase(int id)
        {
            var GetEvent = await _ipurchaseVMService.GetPurchasePageViewModel(id);
            var seatSectionJson = await _ipurchaseVMService.GetSeatSectionList(id);
            var seatNumJson = await _ipurchaseVMService.GetSeatNumList(id);
            //只拿到那個活動的區塊Json
            ViewData["seatSection"] = seatSectionJson;
            //只拿到那個活動的座位Json
            ViewData["seatNumJson"] = seatNumJson;
            return View(GetEvent);
        }

    }
}
