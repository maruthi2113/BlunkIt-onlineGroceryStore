using MessagePack;
using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using KeyAttribute = System.ComponentModel.DataAnnotations.KeyAttribute;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace BlinkIt.Models.Order.ReceivedOrder
{
    public class ROrder
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public ICollection<ROrderItems> ROrderItems { get; set; }
        [Required]
        public double Total { get; set; }
        [Required]
        public DateTime OrderedDate { get; set; }

        [Required]
        public string SellerId { get; set; }
        [Required]
        public string CustomerId { get; set; }
    }
}
