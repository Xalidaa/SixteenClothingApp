using Microsoft.AspNetCore.Mvc;

namespace SixteenClothing.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
