using Microsoft.AspNetCore.Mvc;
using SportsPro.Models;
using System.Linq;

namespace SportsPro.Controllers
{
    public class ProductController : Controller
    {
        private readonly SportsProContext _context;

        public ProductController(SportsProContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var products = _context.Products.ToList();
            return View(products);
        }

        public IActionResult Add()
        {
            return View(new Product());
        }

        public IActionResult Edit(int id)
        {
            var product = _context.Products.Find(id);
            return View("AddEditProduct", product);
        }

        public IActionResult Save(Product product)
        {
            if (ModelState.IsValid)
            {
                if (product.ProductID == 0)
                {
                    _context.Products.Add(product);
                }
                else
                {
                    _context.Products.Update(product);
                }
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("AddEditProduct", product);
        }

        public IActionResult Delete(int id)
        {
            var product = _context.Products.Find(id);
            return View(product);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int productID)
        {
            var product = _context.Products.Find(productID);
            _context.Products.Remove(product);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}