using BlinkIt.Data;
using BlinkIt.Models;
using BlinkIt.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlinkIt.Repository.Implements
{
    public class AccountRepo : IAccountRepo
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _contextAccessor;

        public AccountRepo(ApplicationDbContext context, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _contextAccessor = contextAccessor;
        }

        public async Task<AppUser> GetById(string id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<AppUser> GetByIdNotracking(string id)
        {
            return await _context.Users.Where(n => n.Id == id).AsNoTracking().FirstOrDefaultAsync();
        }

        public void Update(AppUser user)
        {
            
            _context.Users.Update(user);
            _context.SaveChanges();
        }
    }
}
