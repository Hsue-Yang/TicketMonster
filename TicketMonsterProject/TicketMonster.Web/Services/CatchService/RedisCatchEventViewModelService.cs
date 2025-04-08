using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using System.Text.Json.Serialization;
using TicketMonster.Web.Interfaces;
using TicketMonster.Web.Services.Event;
using TicketMonster.Web.ViewModels.Lineup;

namespace TicketMonster.Web.Services.CatchService
{
    public class RedisCatchEventViewModelService : IEventViewModelService
    {
        private readonly EventViewModelService _eventViewModelService;
        private readonly IDistributedCache _cache; // 分散式快取
        //private static readonly string _categoryKey = "categoryKey";
        private readonly TimeSpan _catchDuration = TimeSpan.FromMinutes(10);

        public RedisCatchEventViewModelService(EventViewModelService eventViewModelService, IDistributedCache cache)
        {
            _eventViewModelService = eventViewModelService;
            _cache = cache;
        }


        public Task<EventViewModelBySearch> GetEventBySearch(int? categoryId, string input, DateTime startDate, DateTime endDate, int? sort, string dates, int page = 1, int pageSize = 10)
        {
            return _eventViewModelService.GetEventBySearch(categoryId, input, startDate, endDate, sort, dates, page, pageSize);
        }

        public async Task<List<EventViewModel>> GetEventPageByCategoryId(int categoryId, int? subCategoryId)
        {
            var cacheKey = $"categoryId-{categoryId}-subCategoryId-{subCategoryId}";
            var cacheItems = await _cache.GetAsync(cacheKey);
            if(cacheItems is not null)
            {
                return ByteArrayToObj<List<EventViewModel>>(cacheItems);
            }

            var realItems = await _eventViewModelService.GetEventPageByCategoryId(categoryId, subCategoryId);
            var byteArrayResult = ObjectToByteArray(realItems);
            _cache.Set(cacheKey, byteArrayResult, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
            });
            return realItems;
        }

        public async  Task<MixViewModel> GetMixVM(int categoryId, int? subCategoryId)
        {
            var vm = new MixViewModel
            {
                // 不需要, 從 Layout拿
                //Categories = await GetCategoryName(),
                SubCategories = await GetSubCategoryByCategoryId(categoryId),
                Events = await GetEventPageByCategoryId(categoryId, subCategoryId)

            };


            return vm;
        }

        public Task<List<SubCategoryViewModel>> GetSubCategoryByCategoryId(int categoryId)
        {
            return _eventViewModelService.GetSubCategoryByCategoryId(categoryId);
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
