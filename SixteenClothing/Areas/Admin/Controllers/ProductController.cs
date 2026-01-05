using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SixteenClothing.Areas.Admin.ViewModels.Product;
using SixteenClothing.DAL;
using SixteenClothing.Models;
using SixteenClothing.Utilities.Extensions;
using SixteenClothing.Utilities.Enums;

namespace SixteenClothing.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public ProductController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<ActionResult> Index()
        {
            List<Product> categories = await _context.Products.ToListAsync();
            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Create(CreateProductVM createProductVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (!createProductVM.Photo.ValidateType("image/"))
            {
                ModelState.AddModelError("Photo", "Must be an image");
                return View();
            }

            if (createProductVM.Photo.ValidateSize(FileSize.MB, 2))
            {
                ModelState.AddModelError("Photo", "Must be less than 2 MB");
                return View();
            }

            Product product = new Product()
            {
                Title = createProductVM.Title,
                Price = createProductVM.Price,
                Description = createProductVM.Description,
                Image = await createProductVM.Photo.CreateFile(_env.WebRootPath, "assets", "images")
            };

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");



        }

        public async Task<IActionResult> Update(int id)
        {
            if(id==null || id < 1)
            {
                return BadRequest();
            }
            Product product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            UpdateProductVM updateProductVM = new UpdateProductVM()
            {
                Title = product.Title,
                Price = product.Price,
                Description = product.Description,
                Image = product.Image,
            };
            return View(updateProductVM);
        }
        [HttpPost]

        public async Task<IActionResult> Update(int id, UpdateProductVM updateProductVm)
        {
            if (id == null || id < 1)
            {
                return BadRequest();
            }
            Product product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return View();
            }
            if(updateProductVm.Photo != null)
            {
                if (!updateProductVm.Photo.ValidateType("image/"))
                {
                    ModelState.AddModelError("Photo", "Must be an image");
                    return View();
                }

                if (updateProductVm.Photo.ValidateSize(FileSize.MB, 2))
                {
                    ModelState.AddModelError("Photo", "Must be less than 2 MB");
                    return View();
                }
                string filename = await updateProductVm.Photo.CreateFile(_env.WebRootPath, "assets", "images");
                product.Image.DeleteFile(_env.WebRootPath, "assets", "images");
                product.Image = filename;
            }

            product.Title = updateProductVm.Title;
            product.Description = updateProductVm.Description;
            product.Price = updateProductVm.Price;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id == null || id < 1)
            {
                return BadRequest();
            }
            Product product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            product.Image.DeleteFile(_env.WebRootPath, "assets", "images");
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Detail(int id)
        {
            if (id == null || id < 1)
            {
                return BadRequest();
            }
            Product product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
    }
}
