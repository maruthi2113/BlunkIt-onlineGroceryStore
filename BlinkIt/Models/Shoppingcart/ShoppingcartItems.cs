using MessagePack;
using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using KeyAttribute = System.ComponentModel.DataAnnotations.KeyAttribute;

namespace BlinkIt.Models.Shoppingcart
{
    public class ShoppingcartItems
    {
        [Key]
        public int Id { get; set; } 
        public double Price { get; set; }
        public double Total { get; set; }
        public int count { get; set; }
        public int ProductId { get; set; }
        public Product? Product{get;set;}

        public string AppUserId { get; set; }
    }
}
