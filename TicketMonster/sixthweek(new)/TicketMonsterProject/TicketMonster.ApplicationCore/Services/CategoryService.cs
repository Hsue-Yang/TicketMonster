using TicketMonster.ApplicationCore.Entities;
using TicketMonster.ApplicationCore.Interfaces;

namespace TicketMonster.ApplicationCore.Services
{
    public class CategoryService
    {
        private readonly IRepository<Category> _CategoryRepo;
        private readonly IRepository<SubCategory> _SubCategoryRepo;

        public CategoryService(IRepository<Category> categoryRepo, IRepository<SubCategory> subcategoryRepo)
        {
            _CategoryRepo = categoryRepo;
            _SubCategoryRepo = subcategoryRepo;
        }

        public async Task<List<Category>> CategoryList()
        {
            var category = await _CategoryRepo.ListAsync(x=> true);
            return category;
        }

        public async Task<List<SubCategory>> SubCategoryList()
        {
            var category = await _SubCategoryRepo.ListAsync(x => true);
            return category;
        }

    }
}
