using MessagePack;
using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using KeyAttribute = System.ComponentModel.DataAnnotations.KeyAttribute;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace BlinkIt.Models.Order.PlacedOrder
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public List<OrderItems> OrderItems { get; set; }
        [Required]
        public double Total { get; set; }
        [Required]
        public string AppUserId { get; set; }
        
        public DateTime OrderedDate { get; set; } = DateTime.Now;
         
       
    }
}
