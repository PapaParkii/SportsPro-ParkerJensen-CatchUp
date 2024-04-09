using Microsoft.AspNetCore.Mvc;
using SportsPro.Models;
using System.Collections.Generic;
using System.Linq;

namespace SportsPro.Controllers
{
    // Controller for managing products
    public class ProductsController : Controller
    {
        // Database context for accessing products
        private SportsProContext context;

        // Constructor to initialize the database context
        public ProductsController(SportsProContext ctx)
        {
            context = ctx;
        }

        // Action to display the form for adding a new product
        [HttpGet]
        public ViewResult Add()
        {
            ViewBag.Action = "Add";
            Product product = new Product();
            TempData["message"] = $"{product.Name} successfully added.";
            return View("AddEdit", product);
        }

        // Action to display the form for editing an existing product
        public ViewResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            Product product = context.Products.Find(id);
            TempData["message"] = $"{product.Name} successfully edited.";
            return View("AddEdit", product);
        }

        // Action to handle the submission of the product edit form
        [HttpPost]
        public IActionResult AddEdit(Product product)
        {
            if (ModelState.IsValid)
            {
                if (product.ProductID == 0)
                {
                    context.Products.Add(product);
                }
                else
                {
                    context.Products.Update(product);
                }
                context.SaveChanges();
                return RedirectToAction("Product");
            }
            else
            {
                return View("AddEdit", product);
            }
        }

        // Action to display a list of all products
        [Route("Products")]
        public ViewResult Product()
        {
            var products = context.Products.ToList();
            return View(products);
        }

        // Action to display the form for confirming the deletion of a product
        [HttpGet]
        public ViewResult Delete(int id)
        {
            var product = context.Products.Find(id);
            return View(product);
        }

        // Action to handle the deletion of a product
        [HttpPost]
        public RedirectToActionResult Delete(Product product)
        {
            context.Products.Remove(product);
            context.SaveChanges();
            TempData["message"] = $"{product.Name} successfully deleted.";
            return RedirectToAction("Product", "Products");
        }
    }
}