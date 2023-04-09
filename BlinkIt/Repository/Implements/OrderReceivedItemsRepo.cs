using BlinkIt.Data;
using BlinkIt.Models.Order.ReceivedOrder;
using BlinkIt.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlinkIt.Repository.Implements
{
    public class OrderReceivedItemsRepo : IOrderreceiveditemsRepo
    {
        public readonly ApplicationDbContext _context;
        public OrderReceivedItemsRepo(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Add(ROrderItems item)
        {
            _context.OrdersReceiveditems.Add(item);
            _context.SaveChanges(); 
        }

        public void Delete(ROrderItems item)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ROrderItems>> GetAll()
        {
           return await  _context.OrdersReceiveditems.ToListAsync();
        }

        public async Task<IEnumerable<ROrderItems>> GetAllBySellerId(string id)
        {
            return await _context.OrdersReceiveditems.Where(s=>s.SellerId==id).ToListAsync();   
        }

        public async Task<IEnumerable<ROrderItems>> GetByOrder(int id)
        {
            return await _context.OrdersReceiveditems.Include(p=>p.Product).Where(s => s.ROrderId == id).ToListAsync();

        }

        public void Update(ROrderItems item)
        {
            _context.Update(item);
            _context.SaveChanges();
        }
    }
}
