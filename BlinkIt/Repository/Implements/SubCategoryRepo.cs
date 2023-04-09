using BlinkIt.Data;
using BlinkIt.Models;
using BlinkIt.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BlinkIt.Repository.Implements
{
    public class SubCategoryRepo : ISubCategoryRepo
    {
        private readonly ApplicationDbContext _context;
        public SubCategoryRepo(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Add(SubCategory sb)
        {
            _context.SubCategories.Add(sb);
            _context.SaveChanges();
        }

        public void Delete(SubCategory sb)
        {
            _context.SubCategories.Remove(sb);
            _context.SaveChanges();
        }

        public async Task<IEnumerable<SubCategory>> GetAll()
        {
            return await _context.SubCategories.Include(c=>c.Category).ToListAsync();
        }

        public async Task<SubCategory> GetById(int id)
        {
            return await _context.SubCategories.Include(c => c.Category).FirstOrDefaultAsync(n=>n.Id==id);
        }

        public async Task<SubCategory> GetByIdNoTracking(int id)
        {
            return await _context.SubCategories.Include(c => c.Category).AsNoTracking().FirstOrDefaultAsync(n => n.Id == id);
        }

        public async Task<IEnumerable<SubCategory>> GetByName(string name)
        {
            return await _context.SubCategories.Include(c=>c.Category).Where(n=>n.Name.Contains(name)).ToListAsync();
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await _context.Categories.OrderBy(n=>n.Name).ToListAsync();
        }

        public async Task<IEnumerable<SubCategory>> GetByCategory(int id)
        {
           
            return await _context.SubCategories.Include(n => n.Category).Where(n=>n.CategoryId==id).ToListAsync();
        }
        public void Update(SubCategory sb)
        {
            _context.SubCategories.Update(sb);
            _context.SaveChanges();
        }

       
    }
}
