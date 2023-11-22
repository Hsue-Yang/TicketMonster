using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using TicketMonster.Web.Interfaces;
using TicketMonster.Web.Services.Home;
using TicketMonster.Web.ViewModels.Home;

namespace TicketMonster.Web.Services.CatchService
{
    public class RedisCatchHomePageViewModelService : IHomePageViewModelService
    {
        private readonly HomePageViewModelService _homePageViewModelService;
        private readonly IDistributedCache _cache;
        private static readonly string _homePage = "homePage";
        private readonly TimeSpan _catchDuration = TimeSpan.FromMinutes(15);
        public RedisCatchHomePageViewModelService(HomePageViewModelService homePageViewModelService, IDistributedCache cache)
        {
            _homePageViewModelService = homePageViewModelService;
            _cache = cache;
        }

        public async Task<HomePageViewModel> GetHomepageViewModel()
        {
            var cacheKey = "HomePage";
            var cacheItems = await _cache.GetAsync(cacheKey);
            if(cacheItems is not null)
            {
                return ByteArrayToObj<HomePageViewModel>(cacheItems);
            }
            var realItem =  await _homePageViewModelService.GetHomepageViewModel();
            var byteArrayResult = ObjectToByteArray(realItem);
            _cache.Set(cacheKey, byteArrayResult, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = _catchDuration
            });
            return realItem;
        }



        /// <summary>
		/// 將物件轉換為 Byte Array (分散式快取只支援此格式)
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		private byte[] ObjectToByteArray(object obj)
        {
            return JsonSerializer.SerializeToUtf8Bytes(obj);
        }
        /// <summary>
        /// 將Byte Array 轉成物件 （從分散式記憶體取得的ByteArray轉回物件） 
        /// </summary>
        /// <param name="byteArr"></param>
        /// <typeparam name="T">參考型別</typeparam>
        /// <returns></returns>
        private T ByteArrayToObj<T>(byte[] byteArr) where T : class
        {
            return byteArr is null ? null : JsonSerializer.Deserialize<T>(byteArr);
        }
    }
}
