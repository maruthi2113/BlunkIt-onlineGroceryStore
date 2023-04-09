using BlinkIt.Models.Order.PlacedOrder;

namespace BlinkIt.Repository.Interfaces
{
    public interface IOrderPlacedRepo
    {
        public void Add(Order order);
        public void Update(Order order);
        public void Delete(Order ordr);
        public Task<IEnumerable<Order>> GetAll();
        public Task<IEnumerable<Order>> GetAllByAppUserId(string id);
        public Task<Order> GetById(int id);
        public Task<Order> GetByIdNoTracking();
    }
}
