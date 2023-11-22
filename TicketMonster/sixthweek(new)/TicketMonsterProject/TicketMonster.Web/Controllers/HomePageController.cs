using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using TicketMonster.ApplicationCore.Entities;
using TicketMonster.Web.Interfaces;
using TicketMonster.Web.Services.Cms;
using TicketMonster.Web.Services.Home;

namespace TicketMonsterWebAppMVC.Controllers
{
    public class HomePageController : Controller
    {
     
        private readonly IHomePageViewModelService _homepageViewModelService;     
    
        public HomePageController(IHomePageViewModelService homepageViewModelService)
        {                 
            _homepageViewModelService = homepageViewModelService;           
        }  
      
        public async Task<IActionResult> HomePageView()
        {
            var vm = await _homepageViewModelService.GetHomepageViewModel();
            if (TempData.ContainsKey("ToastrMessage")) { ViewBag.ToastrMessage = TempData["ToastrMessage"]; }
            return View(vm);
        }       
    }

}  
