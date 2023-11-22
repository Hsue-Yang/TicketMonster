using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using TicketMonster.ApplicationCore.Extensions;
using TicketMonster.ApplicationCore.Services;
using TicketMonster.Web.Services;
using TicketMonster.Web.Services.Cms;
using TicketMonster.Web.ViewModels.CheckOut;

namespace TicketMonsterWebAppMVC.Controllers
{
    public class CheckOutPageController : Controller
    {
        private readonly CheckOutPageService _chceckoutservice;
        private readonly EcPayService _ecPayService;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly CountOrderTotalMoneyService _countOrderTotalMoney;
        private readonly IPurchaseVMService _ipurchaseVMService;     
        

        public CheckOutPageController(
            CheckOutPageService chceckoutservice,
            EcPayService ecPayService,
            IWebHostEnvironment hostingEnvironment,
            CountOrderTotalMoneyService countOrderTotalMoney,
            IPurchaseVMService ipurchaseVMService)
        {
            _chceckoutservice = chceckoutservice;
            _ecPayService = ecPayService;
            _hostingEnvironment = hostingEnvironment;
            _countOrderTotalMoney = countOrderTotalMoney;
            _ipurchaseVMService = ipurchaseVMService;
        }

        [Authorize]
        public async Task<IActionResult> CheckOutPageView(int id)
        {
            ViewBag.HideNavbar = true;
            var vm = await _chceckoutservice.CheckOutPageView(id);
            return View(vm);
        }

        [Authorize]
        [HttpPost]
        public IActionResult GetOrderCookie([FromBody] JsonDataModel jsonDataModel)
        {
            try
            {
                var merchantradeno = $"T{DateTime.UtcNow.Ticks}";
                string jsonData = jsonDataModel.OrderData;
                OrderDataFromCookieModel orderData = JsonConvert.DeserializeObject<OrderDataFromCookieModel>(jsonData);
                _chceckoutservice.ProcessTempOrderData(orderData, merchantradeno);

                var ticket = orderData.TicketName.Split(',');
                var sectionName = ticket[0].Trim();

                var ticketPrice = _countOrderTotalMoney.GetTicketPrice(orderData.EventId, sectionName);

                var result = _countOrderTotalMoney.CalculateOrderDetails(ticketPrice, orderData.TicketCount);
                return Json(new { nextAction = $"/CheckOutPage/CheckOut?totalprice={result.OrderTotalMoney}&unitprice={orderData.TicketPrice}&count={orderData.TicketCount}&name={orderData.EventName}&ticketname={orderData.TicketName}&merchantradeno={merchantradeno}" });
            }
            catch (Exception ex)
            {
                return BadRequest("Error processing cookie data: " + ex.Message);
            }
        }

        [Authorize]
        [HttpPost]
        public IActionResult CountOrderTotalMoney([FromBody] JsonDataModel jsonDataModel)
        {
            string jsonData = jsonDataModel.OrderData;
            OrderDataFromCookieModel orderData = JsonConvert.DeserializeObject<OrderDataFromCookieModel>(jsonData);

            var ticket = orderData.TicketName.Split(',');
            var sectionName = ticket[0].Trim();

            var ticketPrice =  _countOrderTotalMoney.GetTicketPrice(orderData.EventId, sectionName);        

            var result = _countOrderTotalMoney.CalculateOrderDetails(ticketPrice, orderData.TicketCount);
            var resultreturn = new
            {
                result.OrderTotalMoney,
                result.ServiceFee,
                result.OrderProcessingFee,
                result.Tax,
                OrderOriginMoney = orderData.TicketPrice,
                orderData.TicketName,
                orderData.TicketCount,
                orderData.TicketPrice,
            };
            return new JsonResult(resultreturn);
        }

        [Authorize]
        public IActionResult CheckOut(int totalprice, int unitprice, int count, string name, string ticketname, string merchantradeno)
        {
            var model = _ecPayService.CreatePaymentOrder(totalprice, unitprice, count, name, ticketname, merchantradeno);
            return View(model);
        }

        [HttpPost] //沒設定NGORK這邊一定不行
        public IActionResult ReturnUrl(IFormCollection input)
        {
            string RtnCode = input["RtnCode"];
            string MerchantTradeNo = input["MerchantTradeNo"];
            int number = int.Parse(RtnCode);
            if (number == 1)
            {
                _chceckoutservice.ProcessOrderData(MerchantTradeNo);
                return Ok("1/OK");
            }
            else
            {
                return null;
            }

        }

        public ActionResult GenerateQRCode(string text)
        {

            var rootPath = _hostingEnvironment.ContentRootPath;

            // 指定要保存的文件路径
            string filePath = Path.Combine(rootPath, "wwwroot", "QRCode.png");

            // 调用生成并保存 QRCode 图像的方法
            QRcodeHelper.GenerateAndSaveQRCodeImage(text, filePath);

            // 返回生成的图像路径
            return File(filePath, "image/png");
        }
    }
}
