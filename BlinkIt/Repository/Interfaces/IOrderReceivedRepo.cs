using BlinkIt.Models.Order.ReceivedOrder;

namespace BlinkIt.Repository.Interfaces
{
    public interface IOrderReceivedRepo
    {
        public void Add(ROrder order);
        public void Delete(ROrder order);
        public void Update(ROrder order);
        public Task<IEnumerable<ROrder>> GetAll();
        public Task<IEnumerable<ROrder>> GetAllOrderBySeller(string id);
    }
}
