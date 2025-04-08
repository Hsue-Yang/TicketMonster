using Microsoft.AspNetCore.Mvc;

namespace TicketMonster.Admin.Views.Dashboard
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
