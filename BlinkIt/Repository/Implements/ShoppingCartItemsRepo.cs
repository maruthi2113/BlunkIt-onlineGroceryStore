using BlinkIt.Data;
using BlinkIt.Models.Shoppingcart;
using BlinkIt.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;

namespace BlinkIt.Repository.Implements
{
    public class ShoppingCartItemsRepo:IShoppingCartItemsRepo
    {
        public ApplicationDbContext _context;
        public ShoppingCartItemsRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ShoppingcartItems>> GetAll()
        {
            var result = await _context.ShoppingCartItems.Include(p=>p.Product).Include(s=>s.Product.Category).Include(s=>s.Product.SubCategory).ToListAsync();
            return result;
        }
        public void Add(ShoppingcartItems item)
        {
            _context.ShoppingCartItems.Add(item);
            _context.SaveChanges();
        }

        public void Delete(ShoppingcartItems item)
        {
            _context.ShoppingCartItems.Remove(item);
            _context.SaveChanges();
        }

        public IEnumerable<ShoppingcartItems> Get(string id)
        {
            var result = _context.ShoppingCartItems.Include(p => p.Product).Include(p => p.Product.Category).Include(p => p.Product.SubCategory).Where(n => n.AppUserId == id).ToList();
            return result;
        }
        public void DeleteRange(string id)
        {
            var items =  Get(id);
            foreach (var item in items)
            {
                _context.ShoppingCartItems.Remove(item);
            }
            _context.SaveChanges();

        }

        public async Task<ShoppingcartItems> GetById(int id)
        {
            return await _context.ShoppingCartItems.Include(n => n.Product).FirstOrDefaultAsync(n => n.Id == id);
        }

        public async Task<ShoppingcartItems> GetByIdByAppUser(string id, int id2)
        {
            return await _context.ShoppingCartItems.Include(n => n.Product).Where(n=>n.AppUserId==id).FirstOrDefaultAsync(n=>n.ProductId==id2);
        }
        public async Task<ShoppingcartItems> GetByIdAsync(int id)
        {
            return await _context.ShoppingCartItems.Include(n => n.Product).AsNoTracking().FirstOrDefaultAsync(n => n.Id == id);
        }

        public void Update(ShoppingcartItems item)
        {
            _context.ShoppingCartItems.Update(item);
            _context.SaveChanges();
        }

        public async Task<ShoppingcartItems> GetByProductId(int id)
        {
            return await _context.ShoppingCartItems.Include(n => n.Product).FirstOrDefaultAsync(n => n.ProductId == id);
        }

        public async Task<ShoppingcartItems> GetByProductIdNoTracking(int id)
        {
            return await _context.ShoppingCartItems.Include(n => n.Product).AsNoTracking().FirstOrDefaultAsync(n => n.Id == id);
        }

        public async Task<List<ShoppingcartItems>> GetAllByAppUserId(string id)
        {
            return await _context.ShoppingCartItems.Include(n=>n.Product).Include(n=>n.Product.Category).Include(n=>n.Product.SubCategory).Where(n=>n.AppUserId==id).ToListAsync();
        }
    }
}
