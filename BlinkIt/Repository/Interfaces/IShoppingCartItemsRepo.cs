using BlinkIt.Models.Shoppingcart;

namespace BlinkIt.Repository.Interfaces
{
    public interface IShoppingCartItemsRepo
    {
        public Task<List<ShoppingcartItems>> GetAll();
        public Task<List<ShoppingcartItems>> GetAllByAppUserId(string id);
        public Task<ShoppingcartItems> GetById(int id);
        public Task<ShoppingcartItems> GetByIdByAppUser(string id,int id2);
        public Task<ShoppingcartItems> GetByIdAsync(int id);
        public Task<ShoppingcartItems> GetByProductId(int id);
        public Task<ShoppingcartItems> GetByProductIdNoTracking(int id);
        public void Add(ShoppingcartItems item);
        public void Update(ShoppingcartItems item);
        public void Delete(ShoppingcartItems item);
        public void DeleteRange(string id);
    }
}
