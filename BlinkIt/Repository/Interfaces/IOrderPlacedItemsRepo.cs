using BlinkIt.Models.Order.PlacedOrder;

namespace BlinkIt.Repository.Interfaces
{
    public interface IOrderPlacedItemsRepo
    {
        public void Add(OrderItems item);
        public void Update(OrderItems item);
        public void Delete(OrderItems item);
        public Task<IEnumerable<OrderItems>> GetAll();
        public Task<IEnumerable<OrderItems>> GetAllByAppUserId();
        public Task<OrderItems> GetById();
        public Task<IEnumerable<OrderItems>> GetByOrderId(int id);
        
    }
}
