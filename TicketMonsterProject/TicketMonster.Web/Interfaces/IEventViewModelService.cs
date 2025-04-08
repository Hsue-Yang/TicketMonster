using TicketMonster.Web.ViewModels.Lineup;

namespace TicketMonster.Web.Interfaces
{
    public interface IEventViewModelService
    {
        //Task<List<CategoryViewModel>> GetCategoryName();
        Task<EventViewModelBySearch> GetEventBySearch(int? categoryId, string input, DateTime startDate, DateTime endDate, int? sort, string dates, int page = 1, int pageSize = 10);
        Task<List<EventViewModel>> GetEventPageByCategoryId(int categoryId, int? subCategoryId);
        Task<MixViewModel> GetMixVM(int categoryId, int? subCategoryId);
        Task<List<SubCategoryViewModel>> GetSubCategoryByCategoryId(int categoryId);
    }
}