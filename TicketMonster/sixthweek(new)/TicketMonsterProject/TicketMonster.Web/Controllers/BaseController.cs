using Microsoft.AspNetCore.Mvc;

namespace TicketMonster.Web.Controllers;

public class BaseController : Controller
{
    protected void SetToastrMessage()
    {
        //if (TempData.ContainsKey("ToastrMessage")) { ViewBag.ToastrMessage = TempData["ToastrMessage"]; }
        //ViewBag.ToastrMessage = TempData.ContainsKey("ToastrMessage") ? TempData["ToastrMessage"] : ViewBag.ToastrMessage;
        ViewBag.ToastrMessage = TempData["ToastrMessage"] ?? ViewBag.ToastrMessage;
    }
}