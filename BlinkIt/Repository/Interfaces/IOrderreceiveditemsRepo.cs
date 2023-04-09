using BlinkIt.Models.Order.ReceivedOrder;

namespace BlinkIt.Repository.Interfaces
{
    public interface IOrderreceiveditemsRepo
    {

        public void Add(ROrderItems item);
        public void Delete(ROrderItems item);
        public void Update(ROrderItems item);
        public Task<IEnumerable<ROrderItems>> GetByOrder(int id);
        public Task<IEnumerable<ROrderItems>> GetAll();
        public Task<IEnumerable<ROrderItems>> GetAllBySellerId(string id);
    }
}
