namespace BlinkIt.Models.ViewModels.subcategory
{
    public class SubCategoryEditViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } 
        public IFormFile? Image { get; set; }
        public string? ImageUrl { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }  
    }
}
