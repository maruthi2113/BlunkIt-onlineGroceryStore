using BlinkIt.Models;

namespace BlinkIt.Repository.Interfaces
{
    public interface IProductRepo
    {
        public void Add(Product p);
        public void Update(Product p);
        public void Delete(Product p);   
        public Task<IEnumerable<Product>> GetAll();
        public Task<IEnumerable<Product>> GetByCategory(int id);
        public Task<IEnumerable<Product>> GetBySubCategory(int id);
        public Task<Product> GetById(int id);
        public Task<Product> GetByIdNoTracking(int id);
        public Task<IEnumerable<Product>> GetByName(string name);
        public Task<IEnumerable<Product>> GetByPopular();
        public Task<IEnumerable<Product>> GetBySeller(string s);
    }
}
