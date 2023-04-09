using BlinkIt.Models;
using BlinkIt.Models.Order.PlacedOrder;
using BlinkIt.Models.Order.ReceivedOrder;
using BlinkIt.Models.Shoppingcart;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlinkIt.Data
{
    public class ApplicationDbContext:IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public  DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ShoppingcartItems> ShoppingCartItems { get; set; }
        public DbSet<ShoppingCart> ShoppingCart { get; set; }
        public DbSet<Order> OrdersPlaced { get; set; }
        public DbSet<OrderItems> OrderPlacedItems { get; set; }
        public DbSet<ROrder> OrdersReceived { get;set; }
        public DbSet<ROrderItems> OrdersReceiveditems { get; set; }

    }
}
