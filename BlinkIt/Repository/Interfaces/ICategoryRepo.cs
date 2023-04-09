using BlinkIt.Models;

namespace BlinkIt.Repository.Interfaces
{
    public interface ICategoryRepo
    {
        public void Add(Category category);
        public void Update(Category category);
        public void Delete (Category category);
        public Task<IEnumerable<Category>> GetAll();
        public Task<Category> GetById(int id);
        public Task<Category> GetByIdNoTracking(int id);
        public Task<IEnumerable<Category>> GetByName(string name);


    }
}
