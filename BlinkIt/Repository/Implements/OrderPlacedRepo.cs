using BlinkIt.Data;
using BlinkIt.Models.Order.PlacedOrder;
using BlinkIt.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlinkIt.Repository.Implements
{
    public class OrderPlacedRepo : IOrderPlacedRepo
    {
        private readonly ApplicationDbContext _context;

        public OrderPlacedRepo(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Add(Order order)
        {
            _context.OrdersPlaced.Add(order);
            _context.SaveChanges();
        }

        public void Delete(Order ordr)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Order>> GetAll()
        {
            return await _context.OrdersPlaced.ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetAllByAppUserId(string id)
        {
            return await _context.OrdersPlaced.Where(n=>n.AppUserId==id).ToListAsync();

        }

        public Task<Order> GetById(int id)
        {
            throw new NotImplementedException();
        }


        public Task<Order> GetByIdNoTracking()
        {
            throw new NotImplementedException();
        }

        public void Update(Order order)
        {
            _context.OrdersPlaced.Update(order);
            _context.SaveChanges();
        }
    }
}
