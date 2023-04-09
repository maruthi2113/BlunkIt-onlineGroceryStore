using BlinkIt.Data;
using BlinkIt.Models;
using BlinkIt.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlinkIt.Repository.Implements
{
    public class CategoryRepo : ICategoryRepo
    {
        private readonly ApplicationDbContext _context; 
        public CategoryRepo(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Add(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges(); 
        }

        public void Delete(Category category)
        {
            _context.Categories.Remove(category);
            _context.SaveChanges();
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
           return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetById(int id)
        {
            return await _context.Categories.FirstOrDefaultAsync(n=>n.Id==id);
        }

        public async Task<Category> GetByIdNoTracking(int id)
        {
            return await _context.Categories.AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Category>> GetByName(string name)
        {
            return await _context.Categories.Where(n=>n.Name.Contains(name)).ToListAsync();
        }

        public void Update(Category category)
        {
            _context.Categories.Update(category);
            _context.SaveChanges();
        }
    }
}
