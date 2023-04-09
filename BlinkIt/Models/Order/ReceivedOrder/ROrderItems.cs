using System.ComponentModel.DataAnnotations;

namespace BlinkIt.Models.Order.ReceivedOrder
{
    public class ROrderItems
    {
        [Key]
        public int Id { get; set; } 
        public int Quantity { get; set; }
        public double price { get; set; }
        public double Total { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int ROrderId { get; set; }
        public ROrder ROrder { get; set; }
        public string SellerId { get; set; }
        public string CustomerId { get; set; }
    }
}
