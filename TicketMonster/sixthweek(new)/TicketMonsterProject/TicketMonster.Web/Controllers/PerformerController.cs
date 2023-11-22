using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System.Linq;
using TicketMonster.ApplicationCore.Entities;
using TicketMonster.ApplicationCore.Interfaces.PerformerService;
using TicketMonster.ApplicationCore.Services;
//using TicketMonster.ApplicationCore.Serviecs;
using TicketMonster.Infrastructure.Data;
using TicketMonster.Web.Services;
using TicketMonster.Web.Services.Performer;
using TicketMonster.Web.ViewModels.CheckOut;
using TicketMonster.Web.ViewModels.Home;
using TicketMonster.Web.ViewModels.Performer;

namespace TicketMonsterWebAppMVC.Controllers
{
    public class PerformerController : Controller
    {

        private readonly PerformerViewModelService _performerViewModelService;

        public PerformerController(PerformerViewModelService performerViewModelService)
        {
            _performerViewModelService = performerViewModelService;
        }       
        
        public async Task<IActionResult> PerformerView(int id, string start, string end)
        {
            DateTime startt;
            DateTime endd;

            if (string.IsNullOrEmpty(start))
            {
                startt = DateTime.Now;
            }
            else
            {
                startt = DateTime.Parse(start);
            }

            if (string.IsNullOrEmpty(end))
            {          
                DateTime currentTime = DateTime.Now;              
                endd = currentTime.AddDays(30);
            }
            else
            {
                endd = DateTime.Parse(end);
            }
            var vm = await _performerViewModelService.PerformerPage(id, startt, endd);
            return View(vm);
        }

        //[HttpPost]
        //public IActionResult PerformerView2( [FromBody] DateRangeFromAjax dateTime)
        //{
        //    string daterangeajax = dateTime.daterange;
        //    DateRangeModel daterange = JsonConvert.DeserializeObject<DateRangeModel>(daterangeajax);
        //    var start = daterange.start;
        //    var end = daterange.end;

        //    // 使用RedirectToAction重定向到PerformerView，并传递参数
        //    return RedirectToAction("PerformerView", new { start = start, end = end });
        //}


        //private static int totalRows = -1;
        //private readonly NorthwindContext _ctx;
        //private readonly IConfiguration _config;
        //public ProductController(NorthwindContext ctx, IConfiguration config)
        //{
        //    _ctx = ctx;

        //    _config = config;

        //    if (totalRows == -1)
        //    {
        //        totalRows = _ctx.Products.Count();   //計算總筆數
        //    }

        //}

        //public IActionResult Index(int id = 1)
        //{
        //    int activePage = id; //目前所在頁
        //    int pageRows = 10;   //每頁幾筆資料
        //    //int totalRows = _ctx.Clothing.Count();   //計算總筆數

        //    //計算Page頁數
        //    int Pages = 0;
        //    if (totalRows % pageRows == 0)
        //    {
        //        Pages = totalRows / pageRows;
        //    }
        //    else
        //    {
        //        Pages = (totalRows / pageRows) + 1;
        //    }

        //    int startRow = (activePage - 1) * pageRows;  //起始記錄Index
        //    List<Product> products = _ctx.Products.OrderBy(x => x.ProductId).Skip(startRow).Take(pageRows).ToList();


        //    ViewData["Active"] = 1;    //SidebarActive頁碼
        //    ViewData["ActivePage"] = id;    //Activec分頁碼
        //    ViewData["Pages"] = Pages;  //頁數

        //    return View(products);
        //}


    }

}
