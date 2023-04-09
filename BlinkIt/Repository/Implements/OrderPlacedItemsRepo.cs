using BlinkIt.Data;
using BlinkIt.Models.Order.PlacedOrder;
using BlinkIt.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlinkIt.Repository.Implements
{
    public class OrderPlacedItemsRepo : IOrderPlacedItemsRepo
    {

        private readonly ApplicationDbContext _context;

        public OrderPlacedItemsRepo(ApplicationDbContext context)
        {
            _context = context; 
        }
        public void Add(OrderItems item)
        {
            _context.OrderPlacedItems.Add(item);
            _context.SaveChanges();
        }

        public void Delete(OrderItems item)
        {
            _context.OrderPlacedItems.Remove(item);
            _context.SaveChanges();
        }

        public Task<IEnumerable<OrderItems>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<OrderItems>> GetAllByAppUserId()
        {
            throw new NotImplementedException();
        }

        public Task<OrderItems> GetById()
        {
            throw new NotImplementedException();
        }

       

        public async Task<IEnumerable<OrderItems>> GetByOrderId(int id)
        {
            return await _context.OrderPlacedItems.Include(p=>p.Product).Where(n => n.OrderId == id).ToListAsync();
        }

        public void Update(OrderItems item)
        {
            throw new NotImplementedException();
        }
    }
}
