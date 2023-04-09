namespace BlinkIt.Models.ViewModels.category
{
    public class CategoryEditModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string? ImageUrl { get; set; }
        public IFormFile? Image { get; set; }

    }
}
