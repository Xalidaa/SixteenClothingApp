using Microsoft.AspNetCore.Mvc;
using SixteenClothing.DAL;
using SixteenClothing.Models;

namespace SixteenClothing.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Product> products = new()
            {
                new Product
                {
                    Title = "Classic White T-Shirt",
                    Price = 19.99m,
                    Description = "A timeless white t-shirt made from 100% organic cotton.",
                    Image = "product_01.jpg"
                },
                new Product
                {
                    Title = "Denim Jeans",
                    Price = 49.99m,
                    Description = "Comfortable and stylish denim jeans with a modern fit.",
                    Image = "product_02.jpg"

                },
                new Product
                {
                    Title = "Leather Jacket",
                    Price = 129.99m,
                    Description = "Premium leather jacket that adds an edge to any outfit.",
                    Image = "product_03.jpg"
                },
                new Product
                {
                    Title = "Summer Dress",
                    Price = 39.99m,
                    Description = "Lightweight and colorful summer dress perfect for warm days.",
                    Image = "product_04.jpg"
                },
                new Product
                {
                    Title = "Running Sneakers",
                    Price = 59.99m,
                    Description = "Durable and comfortable sneakers designed for running and everyday wear.",
                    Image = "product_05.jpg"
                },
                new Product
                {
                    Title = "Wool Scarf",
                    Price = 24.99m,
                    Description = "Cozy wool scarf to keep you warm during the colder months.",
                    Image = "product_06.jpg"
                }
            };
            //_context.Products.AddRange(products);
            //_context.SaveChanges();
            return View(products);
        }
    }
}
