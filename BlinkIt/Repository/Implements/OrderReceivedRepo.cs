using BlinkIt.Data;
using BlinkIt.Models.Order.ReceivedOrder;
using BlinkIt.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlinkIt.Repository.Implements
{
    public class OrderReceivedRepo : IOrderReceivedRepo
    {
        private readonly ApplicationDbContext _context;
        public OrderReceivedRepo(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Add(ROrder order)
        {
            _context.OrdersReceived.Add(order);
            _context.SaveChanges();
        }

        public void Delete(ROrder order)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ROrder>> GetAll()
        {
            return await _context.OrdersReceived.ToListAsync();
        }

        public async Task<IEnumerable<ROrder>> GetAllOrderBySeller(string id)
        {
            return await _context.OrdersReceived.Where(s => s.SellerId == id).ToListAsync();
        }

        public void Update(ROrder order)
        {
            _context.OrdersReceived.Update(order);
            _context.SaveChanges();
        }
    }
}
