using BlinkIt.Data;
using BlinkIt.Helpers;
using BlinkIt.Models;
using BlinkIt.Models.Order.PlacedOrder;
using BlinkIt.Models.Order.ReceivedOrder;
using BlinkIt.Models.Shoppingcart;
using BlinkIt.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlinkIt.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartItemsRepo _repo;
        private readonly IAccountRepo _repoAccount;
        private readonly IProductRepo _repoproduct;
        private readonly IShoppingCartRepo _reposhop;
        private readonly IOrderPlacedRepo _repoOrderplaced;
        private readonly IOrderPlacedItemsRepo _repoOrderPlaceditems;
        private readonly IOrderReceivedRepo _repoReceivedorder;
        private readonly IOrderreceiveditemsRepo _repoReceiveditems;
        private readonly IHttpContextAccessor _contextAccessor;
        public ShoppingCartController(IShoppingCartItemsRepo repo, IProductRepo repoproduct,IAccountRepo repoAccount, IHttpContextAccessor contextAccessor
            ,IOrderReceivedRepo orderReceivedRepo ,IOrderPlacedRepo repoOrderplaced, IOrderPlacedItemsRepo repoOrderPlaceditems, IOrderreceiveditemsRepo repoReceiveditems)
        {
            _repo = repo;
            _repoproduct = repoproduct;
            _contextAccessor = contextAccessor;
            _repoAccount = repoAccount;
            _repoOrderplaced = repoOrderplaced;
            _repoOrderPlaceditems = repoOrderPlaceditems;
            _repoReceiveditems = repoReceiveditems;
            _repoReceivedorder = orderReceivedRepo;
        }


        [Authorize]
        public async Task<IActionResult> AddToCart(int id)
        {
            var currentuserid = _contextAccessor.HttpContext.User.GetUserId();
            var shoppingcartitem = await _repo.GetByIdByAppUser(currentuserid,id);
            var product = await _repoproduct.GetById(id);
            if (shoppingcartitem == null)
            {
                var addingcart = new ShoppingcartItems
                {
                   
                    Price = product.Price,
                    count = 1,
                    Total = product.Price,
                    ProductId = product.Id,
                    AppUserId= currentuserid
                };
                _repo.Add(addingcart);
                return RedirectToAction("CartView", "ShoppingCart");
            }
            else
            {
                shoppingcartitem.count++;
                shoppingcartitem.Total = product.Price + shoppingcartitem.Total;
                _repo.Update(shoppingcartitem);
            }
            return RedirectToAction("CartView", "ShoppingCart");

        }

      

        public async Task<IActionResult> RemovefromCart(int id)
        {
            var shoppingcartitem = await _repo.GetByProductId(id);
            var product = await _repoproduct.GetById(id);

            if (shoppingcartitem.count == 1)
            {
                _repo.Delete(shoppingcartitem);
            }
            else
            {
                shoppingcartitem.count--;
                shoppingcartitem.Total = shoppingcartitem.Total - shoppingcartitem.Price;
                _repo.Update(shoppingcartitem);
            }
            return RedirectToAction("CartView","ShoppingCart");
        }

        public async Task<IActionResult> DeleteItem(int id)
        {
            var item= await _repo.GetById(id);
            _repo.Delete(item);
            return RedirectToAction("CartView","ShoppingCart");
        }

        [Authorize]
        public async Task<IActionResult> CartView()
        {

            var currentuserid = _contextAccessor.HttpContext.User.GetUserId();
            var items = await _repo.GetAllByAppUserId(currentuserid);
            
                return View(items);
        }
        public async Task<IActionResult> Order()
        {
            var currentuserId=  _contextAccessor.HttpContext?.User.GetUserId();
            var currentuser = await _repoAccount.GetById(currentuserId);

            if(currentuser.City==null||currentuser.Street==null||currentuser.Country==null||currentuser.PhoneNumber==null)
            {
                return RedirectToAction("DetailsError","ShoppingCart");
            }
            List<ShoppingcartItems> shoppingcartitemslist= await _repo.GetAllByAppUserId(currentuserId);
            var shoppingCart = new ShoppingCart {
                ShoppingcartItems = new List<ShoppingcartItems>()
            };
            
            foreach(ShoppingcartItems item in shoppingcartitemslist)
                {
                  shoppingCart.ShoppingcartItems.Add(item);
                  shoppingCart.Total=shoppingCart.Total+item.Total;
                }
            shoppingCart.AppUserId = currentuserId;
            OrderPlaced(shoppingCart);
            OrderReceived(shoppingCart);
            _repo.DeleteRange(currentuserId);
            return View("OrderSuccess","ShoppingtCart");
        }
        public void OrderPlaced(ShoppingCart shoppingCart)
        {

            var order = new Order {
                OrderItems = new List<OrderItems>()
            };
            order.Total=shoppingCart.Total;
            var orderlist = order.OrderItems;
            order.AppUserId = shoppingCart.AppUserId;
            order.OrderedDate = DateTime.Now;
            _repoOrderplaced.Add(order);
            foreach (var item in shoppingCart.ShoppingcartItems)
            {
                var orderitem = new OrderItems();
                orderitem.total = item.Total;
                orderitem.price=item.Price;
                orderitem.quantity = item.count;
                orderitem.ProductId=item.ProductId;
                orderitem.OrderId = order.Id;
                orderitem.AppUserId=shoppingCart.AppUserId;
                orderlist.Add(orderitem);
                _repoOrderPlaceditems.Add(orderitem);
            }
            _repoOrderplaced.Update(order);

        }

        public void OrderReceived(ShoppingCart shoppingCart)
        {
            Dictionary<string, List<ShoppingcartItems>> map = new Dictionary<string, List<ShoppingcartItems>>();
            
            foreach(var item in shoppingCart.ShoppingcartItems)
            {
                 var sellerid = item.Product.AppUserId;
                if (map.ContainsKey(sellerid))
                {
                    List<ShoppingcartItems> list = map[sellerid];
                    list.Add(item);
                    map[sellerid] = list;
                }
                else
                {
                    List<ShoppingcartItems> list = new List<ShoppingcartItems>();
                    list.Add(item);
                    map.Add(sellerid, list);
                }

            }
            foreach(var seller in map)
                {
                var orderreceived = new ROrder {
                    ROrderItems = new List<ROrderItems>(),
                    OrderedDate = DateTime.Now,
                    CustomerId = shoppingCart.AppUserId,
                    SellerId = seller.Key,
                    Total=0
                };
                _repoReceivedorder.Add(orderreceived);   
                var orderreceivedlist = orderreceived.ROrderItems;
                List<ShoppingcartItems> list = seller.Value;
                
                foreach(var item in seller.Value)
                {
                    var orderitem = new ROrderItems();
                    orderitem.price = item.Price;
                    orderitem.Product = item.Product;
                    orderitem.ProductId = item.ProductId;
                    orderitem.CustomerId=item.AppUserId;
                    orderitem.SellerId = item.Product.AppUserId;
                    orderitem.Quantity = item.count;
                    orderitem.Total = item.Total;
                    orderreceivedlist.Add(orderitem);
                    _repoReceiveditems.Add(orderitem);
                    orderreceived.Total=orderreceived.Total+orderitem.Total;
                }
                _repoReceivedorder.Update(orderreceived);

            }
        }

        public IActionResult DetailsError()
        {
            return View();
        }
        public IActionResult OrderSuccess()
        {
            return View();
        }
    }
}
