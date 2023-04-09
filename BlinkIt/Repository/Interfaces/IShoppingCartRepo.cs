using BlinkIt.Models.Shoppingcart;

namespace BlinkIt.Repository.Interfaces
{
    public interface IShoppingCartRepo
    {

        public void Add(ShoppingCart cart);
        public void Update();
        public void Delete(ShoppingCart cart);
    }
}
