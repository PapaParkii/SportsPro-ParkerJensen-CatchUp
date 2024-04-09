using Microsoft.AspNetCore.Mvc;
using SportsPro.Models;
using System.Linq;

namespace SportsPro.Controllers
{
    // Controller for managing customers
    public class CustomersController : Controller
    {
        // Database context for accessing customers
        private SportsProContext context;

        // Constructor to initialize the database context
        public CustomersController(SportsProContext ctx)
        {
            context = ctx;
        }

        // Action to display the default view
        public IActionResult Index()
        {
            return View();
        }

        // Action to display a list of all customers
        [Route("Customers")]
        public ViewResult Customer()
        {
            var customers = context.Customers.ToList();
            return View(customers);
        }
    }
}