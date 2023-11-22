using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using TicketMonster.Web.Services.Cms;


namespace TicketMonster.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseApiController : ControllerBase
    {
        private readonly IPurchaseVMService _ipurchaseVMService;

        public PurchaseApiController(IPurchaseVMService ipurchaseVMService)
        {
            _ipurchaseVMService = ipurchaseVMService;
        }

        [Authorize]
        [HttpPost, Route("ChangeSeatStatus")]
        public IActionResult ChangeSeatStatus([FromBody] ReserveSeatViewModel model)
        {
            //從api打回來用cookie的ticketName，找到那筆資料Containes(sectionName&&seatNum).SeatsNumViewModel.IsOrdered = true
            try
            {
                var ticket = model.ticketName.Split(',');
                var sectionName = ticket[0].Trim();
                var seatNum = Regex.Match(ticket[1], @"\d").Value;
                //var sectionName = model.ticketName.Split(',')[0].Trim();
                //var seatNum = Regex.Match(model.ticketName, @"\d").ToString();

                //現在打回來的是一個orderdata的cookie資料，裡面就有包含座位資訊，寫一個service接收這個座位資訊去找到那一筆資料後，更新狀態
                var seat = _ipurchaseVMService.GetEventSeat(model.EventId, sectionName, seatNum);


                return Ok(seat);
            }
            catch (Exception ex)
            {
                return BadRequest("Error processing cookie data: " + ex.Message);
            }
        }
        [HttpGet, Route("GetTheaterSeat")]
        public async Task<string> GetTheaterSeat()
        {
            var theaterSeat = await _ipurchaseVMService.GetTheaterSeat();

            return theaterSeat;

        }


    }
}
