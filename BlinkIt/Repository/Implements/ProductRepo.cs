using BlinkIt.Data;
using BlinkIt.Models;
using BlinkIt.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace BlinkIt.Repository.Implements
{
    public class ProductRepo : IProductRepo
    {
        private readonly ApplicationDbContext _context;
        private readonly IPhotoService _photo;

        public ProductRepo(ApplicationDbContext context, IPhotoService photo)
        {
            _context = context;
            _photo = photo;
        }

        public void Add(Product model)
        {
            _context.Products.Add(model);
            _context.SaveChanges();
        }

        public void Delete(Product model)
        {
            _context.Products.Remove(model);
            _context.SaveChanges();
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _context.Products.Include(n=>n.Category).Include(n=>n.SubCategory).Include(n=>n.AppUser).ToListAsync();   
        }

        public async Task<IEnumerable<Product>> GetByCategory(int id)
        {
            return await _context.Products.Where(n=>n.CategoryId==id).Include(c=>c.Category).Include(c=>c.SubCategory).Include(c=>c.AppUser).ToListAsync();
        }

        public async Task<Product> GetById(int id)
        {
            return await _context.Products.Include(c=>c.Category).Include(s=>s.SubCategory).Include(s=>s.AppUser).FirstOrDefaultAsync(n=>n.Id==id);
        }

        public async Task<Product> GetByIdNoTracking(int id)
        {
            return await _context.Products.Include(c => c.Category).Include(s => s.SubCategory).Include(s=>s.AppUser).AsNoTracking().FirstOrDefaultAsync(n => n.Id == id);
        }

        public async Task<IEnumerable<Product>> GetByName(string name)
        {
            return await _context.Products.Where(n=>n.Name.Contains(name)).Include(c=>c.Category).Include(s=>s.SubCategory).Include(a=>a.AppUser).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetByPopular()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Product>> GetBySeller(string id)
        {
            return await _context.Products.Include(c => c.Category).Include(s => s.SubCategory).Include(s => s.AppUser).Where(s => s.AppUserId==id).ToListAsync();

        }

        public async Task<IEnumerable<Product>> GetBySubCategory(int id)
        {
            return await _context.Products.Include(c => c.Category).Include(s => s.SubCategory).Include(s => s.AppUser).Where(s=>s.SubCategoryId==id).ToListAsync();

        }

        public void Update(Product p)
        {
            _context.Products.Update(p);
            _context.SaveChanges();
            
        }

       
    }
}
