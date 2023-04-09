namespace BlinkIt.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } 
        public string ImageUrl { get; set; }
        public int count { get; set; }
        public double  Price { get; set; }
        //[DataType(DataType.EmailAddress,ErrorMessage="E-Mail is not Valis)]
        public int CategoryId { get; set; }
        public virtual Category? Category { get; set; }  
        
        public int SubCategoryId { get; set; }
        public virtual SubCategory? SubCategory { get; set; }
        
        public string AppUserId { get; set; }
        public AppUser? AppUser { get;set; }
        
        
       

    }
}
