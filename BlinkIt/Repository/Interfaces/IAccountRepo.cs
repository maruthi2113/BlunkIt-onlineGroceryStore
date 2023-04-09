using BlinkIt.Models;

namespace BlinkIt.Repository.Interfaces
{
    public interface IAccountRepo
    {
        public Task<AppUser> GetById(string id);
        public Task<AppUser> GetByIdNotracking(string id);
        public void Update(AppUser user);
    }
}
