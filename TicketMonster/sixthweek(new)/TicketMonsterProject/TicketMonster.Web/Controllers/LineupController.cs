using isRock.LineBot;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Drawing.Printing;
using TicketMonster.ApplicationCore.Entities;
using TicketMonster.ApplicationCore.Interfaces.EventService;
using TicketMonster.Web.Services;
using TicketMonster.Web.Services.Event;
using TicketMonster.Web.ViewModels.Lineup;
using TicketMonster.ApplicationCore.Model;
using TicketMonster.Web.Interfaces;

namespace TicketMonsterWebAppMVC.Controllers
{
    public class LineupController : Controller
    {
        private readonly IEventService _eventService;
        private readonly IEventViewModelService _eventViewModelService;
        public LineupController(IEventService eventService, IEventViewModelService eventViewModelService)
        {
            _eventService = eventService;
            _eventViewModelService = eventViewModelService;
        }

        // 看從 主頁的什麼地方近來  // 拆開  傳不同的 參數 帶不同的頁面
        // 目錄與子目錄, 判斷有沒有 子目錄 Id
        public IActionResult LineupView(int id, int? subId)
        {
            CaIdOrEventIdViewModel vm;
            if (subId.HasValue)
            {
                vm = new CaIdOrEventIdViewModel()
                {
                    CategoryId = id,
                    SubCategoryId = (int)subId
                };
            }
            else
            {
                vm = new CaIdOrEventIdViewModel() { CategoryId = id };
            }
            //MixViewModel vm;
          //vm = await _eventViewModelService.GetMixVM(id);

            return View(vm);
        }


        // 從 Category的路由近來
        [HttpPost]
        public async Task<IActionResult> GetEventByCa([FromBody] CaIdOrEventIdViewModel id)
        {
            var vm = await _eventViewModelService.GetMixVM(id.CategoryId, id.SubCategoryId);

            return Ok(vm);
        }

		// 首頁篩選
		//[Route("Search/{keyword?}/{dates?}/{id?}/{sort?}/{page?}/{pageSize?}")]
		public async Task<IActionResult> LineupViewBySearch(string keyWord, string dates, int? caId = 0,  int sort = 0,int page=1)
		{
            DateTime startDate = DateTime.Now;
            DateTime endDate = DateTime.Now.AddMonths(1);
            
            if (dates != null)
            {
                string[] dateParts = dates.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                startDate = DateTime.Parse(dateParts[0].Trim());
                endDate = DateTime.Parse(dateParts[1].Trim());
            }



			var vm = await _eventViewModelService.GetEventBySearch(caId, keyWord, startDate, endDate, sort, dates, page);
			return View(vm);
		}


		// 當從事件的路由近來
		//[HttpPost]
		//public async Task<IActionResult> GetEventByEventId([FromBody] CaIdOrEventIdViewModel id)
		//{
		//    var vm = await _eventViewModelService.GetEventPageByEventID(id.EventId);

		//    return Ok(vm);
		//}


		//public IActionResult ShowQueryString(string keyWord)
		//{
		//    CaIdOrEventIdViewModel vm;
		//    vm = new CaIdOrEventIdViewModel()
		//    {
		//        KeyWord = keyWord
		//    };

		//    return View("LineupView", vm);

		//}

		//輸入字串搜尋     廢掉
		//[HttpPost]
		//public async Task<IActionResult> GetQueryString([FromBody] CaIdOrEventIdViewModel keyWord)
		//{
		//    var vm = await _eventViewModelService.GetEventBySearch(keyWord.KeyWord);

		//    //  輸入字串為空白或查無資料邏輯, 待補 
		//    //    if (string.IsNullOrEmpty(keyword))
		//    //    {
		//    //        ViewData["Header"] = "關鍵字不得為空字串";
		//    //        ViewData["Message"] = "請提供活動關鍵字";
		//    //        return View("ShowMessage");
		//    //    }
		//    return Ok(vm);
		//}
	}
}
