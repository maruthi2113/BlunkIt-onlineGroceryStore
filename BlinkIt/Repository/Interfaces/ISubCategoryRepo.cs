using BlinkIt.Models;

namespace BlinkIt.Repository.Interfaces
{
    public interface ISubCategoryRepo
    {
        public void Add(SubCategory sb);
        public void Update(SubCategory sb);
        public void Delete(SubCategory sb);
        public  Task<SubCategory> GetById(int id);
        public Task<SubCategory> GetByIdNoTracking(int id);
        public Task<IEnumerable<SubCategory>> GetByName(string name);
        public Task<IEnumerable<SubCategory>> GetAll();
        public Task<IEnumerable<Category>> GetCategories();
        public Task<IEnumerable<SubCategory>> GetByCategory(int id);
    }
}
