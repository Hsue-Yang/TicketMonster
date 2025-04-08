using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketMonster.Admin.Controllers;

namespace DemoAdmin.Controllers
{
    public class DashboardController : Controller       
    {
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }
      
        public IActionResult ECharts()
        {
            return View();
        }       
    }
}
