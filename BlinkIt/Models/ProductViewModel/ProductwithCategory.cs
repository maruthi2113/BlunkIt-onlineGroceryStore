namespace BlinkIt.Models.ProductViewModel
{
    public class ProductwithCategory
    {
        public IEnumerable<Product> products { get;set; }
        public IEnumerable<SubCategory> subCategories { get;set; }

    }
}
