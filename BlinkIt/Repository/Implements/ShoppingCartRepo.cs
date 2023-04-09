using BlinkIt.Data;
using BlinkIt.Models.Shoppingcart;
using BlinkIt.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlinkIt.Repository.Implements
{
    public class ShoppingCartRepo : IShoppingCartRepo
    {
        public ApplicationDbContext _context;
        public ShoppingCartRepo(ApplicationDbContext context)
        {
            _context=context;
        }

        public void Add(ShoppingCart cart)
        {
          
        }

        

        public void Delete(ShoppingCart cart)
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}
