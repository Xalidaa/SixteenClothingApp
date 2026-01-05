using SixteenClothing.Models.Base;

namespace SixteenClothing.Models
{
    public class Product:BaseEntity
    {
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

    }
}
