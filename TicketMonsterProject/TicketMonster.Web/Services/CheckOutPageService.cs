using TicketMonster.ApplicationCore.Services;
using TicketMonster.Infrastructure.Data;
using TicketMonster.Web.ViewModels.CheckOut;

namespace TicketMonster.Web.Services
{
    public class CheckOutPageService
    {
        private readonly TempOrderRepository _createTempOrderRepo;
        private readonly OrderRepository _createOrderRepo;
        private readonly CheckOutService _checkoutservice;
        private readonly TempOrderService _tempOrderServiceRepo;
        private readonly UserManager _userManager;

        public CheckOutPageService(TempOrderRepository createTempOrderRepo, CheckOutService checkoutservice, OrderRepository createOrderRepo, TempOrderService tempOrderServiceRepo,UserManager userManager)
        {
            _createTempOrderRepo = createTempOrderRepo;
            _createOrderRepo = createOrderRepo;
            _checkoutservice = checkoutservice;
            _tempOrderServiceRepo = tempOrderServiceRepo;  
            _userManager = userManager;
        }

        private async Task<string> GetVenueNameAsync(int eventId)
        {
            return await _checkoutservice.GetVenueNameByEventId(eventId);
        }

        private async Task<string> GetEventPicAsync(int eventId)
        {
            return await _checkoutservice.GetOneOfEventPicAsync(eventId);
        }

        private async Task<string> GetVenueLocationAsync(int eventId)
        {
            return await _checkoutservice.GetVenueLocationByEventId(eventId);
        }

        private async Task<string> GetVenueSvgAsync(int eventId)
        {
            return await _checkoutservice.GetVenueSvgAsync(eventId);
        }


        public void ProcessTempOrderData(OrderDataFromCookieModel orderData, string merchantradeno)
        {        
            if (orderData != null)
            {
                var venuename = GetVenueNameAsync(orderData.EventId).Result;
                var eventpic = GetEventPicAsync(orderData.EventId).Result;
                var locationname = GetVenueLocationAsync(orderData.EventId).Result;
                var venuesvg = GetVenueSvgAsync(orderData.EventId).Result;
                int userid = _userManager.GetCurrentUserId();

                TempOrder order = new TempOrder
                {
                    CustomerId = userid,
                    EventName = orderData.EventName,
                    EventDate = orderData.EventDate,
                    EventPic = eventpic,
                    VenueName = venuename,
                    VenueLocation = locationname,
                    VenueSvg = venuesvg,
                    MerchantTradeNo = merchantradeno,
                    BillingAmount=orderData.BillingAmount                    
                };

                List<TempOrderDetail> orderdetail = new List<TempOrderDetail>();
                for (int i = 0; i < orderData.TicketCount; i++)
                {
                    orderdetail.Add(new TempOrderDetail
                    {
                        EventSeat = orderData.TicketName,
                        Price = orderData.TicketPrice,
                        Barcode = "qw39m8rjr3u9m3r"
                    });
                }
                
                _createTempOrderRepo.CreateTempOrderAndDetail(order, orderdetail);
            }
            
        }

        public void ProcessOrderData(string MerchantTradeNo)
        {
            async Task<TempOrder> GetTempOrderAsync(string MerchantTradeNo)
            {
                return await _tempOrderServiceRepo.GetDataFromTempOrderAsync(MerchantTradeNo);
            }

            var temporderdata = GetTempOrderAsync(MerchantTradeNo).Result;

            async Task<List<TempOrderDetail>> GetTempOrderDetailAsync()
            {
                return await _tempOrderServiceRepo.GetDataFromTempOrderDetailAsync(temporderdata.Id);
            }

            //利用訂單ID從資料庫拿到TempOrderData
            var temporderdetaildata = GetTempOrderDetailAsync().Result;


            if (temporderdata != null)
            {
                Order order = new Order
                {
                    CustomerId = temporderdata.CustomerId,
                    EventName = temporderdata.EventName,
                    EventDate = temporderdata.EventDate,
                    EventPic = temporderdata.EventPic,
                    VenueName = temporderdata.VenueName,
                    VenueLocation = temporderdata.VenueLocation,
                    VenueSvg = temporderdata.VenueSvg,
                    BillingAmount = temporderdata.BillingAmount,
                    OrderDate = DateTime.Now
                };

                List<OrderDetail> orderdetail = temporderdetaildata.Select(tempOrderDetail => new OrderDetail
                {
                    EventSeat = tempOrderDetail.EventSeat,
                    Price = tempOrderDetail.Price,
                    Barcode = tempOrderDetail.Barcode
                }).ToList();

                //    List<OrderDetail> orderdetail = new List<OrderDetail>
                //{
                //    new OrderDetail
                //    {
                //        EventSeat = "還沒做",
                //        Price = 9527,
                //        Barcode = "還沒做"
                //    },
                //    new OrderDetail
                //    {
                //        EventSeat = "還沒做",
                //        Price = 9527,
                //        Barcode = "還沒做"
                //    }
                //};
                _createOrderRepo.CreateOrderAndDetail(order, orderdetail);
            }
        }

        public async Task<CheckOutPageViewModel> CheckOutPageView(int eventId)
        {
            var eventpics = await _checkoutservice.GetEventPicsAsync(eventId);
            var eventdate = GetEventDateAsync(eventId).Result;
            var eventname = GetEventNameAsync(eventId).Result;
            var venuename = GetVenueNameAsync(eventId).Result;
      

            CheckOutPageViewModel viewModel = new CheckOutPageViewModel
            {
                Pic = eventpics,
                EventDate = eventdate,
                EventName = eventname,
                Venue = venuename,              
            };
            return viewModel;         
        }

        private async Task<string> GetEventNameAsync(int eventId)
        {
            return await _checkoutservice.GetEventNameByEventId(eventId);
        }

        private async Task<DateTime> GetEventDateAsync(int eventId)
        {
            return await _checkoutservice.GetEventDateByEventId(eventId);
        }
    }
}




