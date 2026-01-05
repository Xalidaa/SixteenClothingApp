namespace SixteenClothing.Areas.Admin.ViewModels.Product
{
    public class UpdateProductVM
    {
        public string Image { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public IFormFile? Photo { get; set; }
    }
}
