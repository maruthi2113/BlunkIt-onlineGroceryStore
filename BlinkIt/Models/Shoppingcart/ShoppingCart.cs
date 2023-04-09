using BlinkIt.Data;
using System.ComponentModel.DataAnnotations;

namespace BlinkIt.Models.Shoppingcart
{
    public class ShoppingCart
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public List<ShoppingcartItems> ShoppingcartItems { get; set; }
        [Required]
        public double Total { get; set; }
        [Required]
        public string AppUserId { get; set; }  


    }
}
