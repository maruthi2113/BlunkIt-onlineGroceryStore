namespace BlinkIt.Models
{
    public class SubCategory
    {
        public int Id { get; set; } 

        public string Name { get; set; }

        public string Description { get; set; }
        public string ImageUrl { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }  
    }
}
