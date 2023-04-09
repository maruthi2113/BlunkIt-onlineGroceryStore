using System.ComponentModel.DataAnnotations;

namespace BlinkIt.Models.Order.PlacedOrder
{
    public class OrderItems
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int quantity { get; set; }
        [Required]
        public double price { get; set; }
        
        [Required]
        public double total { get; set; }
        [Required]
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        [Required]
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public string AppUserId { get; set; }
    }
}
