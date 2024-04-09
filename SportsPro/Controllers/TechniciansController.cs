using Microsoft.AspNetCore.Mvc;
using SportsPro.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace SportsPro.Controllers
{
    // Controller for managing technicians
    public class TechniciansController : Controller
    {
        // Database context for accessing technicians
        private SportsProContext context { get; set; }

        // Constructor to initialize the database context
        public TechniciansController(SportsProContext ctx)
        {
            context = ctx;
        }

        // Action to display a list of all technicians
        [Route("Technicians")]
        public ViewResult Technician()
        {
            var technicians = context.Technicians.ToList();
            return View(technicians);
        }

        // Action to display the form for adding a new technician
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            return View("Edit", new Technician());
        }

        // Action to display the form for editing an existing technician
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            var technician = context.Technicians.Find(id);
            return View(technician);
        }

        // Action to handle the submission of the technician edit form
        [HttpPost]
        public IActionResult Edit(Technician technician)
        {
            if (ModelState.IsValid)
            {
                if (technician.TechnicianID == 0)
                    context.Technicians.Add(technician);
                else
                    context.Technicians.Update(technician);
                context.SaveChanges();
                return RedirectToAction("List", technician);
            }
            else
            {
                ViewBag.Action = (technician.TechnicianID == 0) ? "Add" : "Edit";
                return View(technician);
            }
        }

        // Action to display the form for confirming the deletion of a technician
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var technician = context.Technicians.Find(id);
            return View(technician);
        }

        // Action to handle the deletion of a technician
        [HttpPost]
        public IActionResult Delete(Technician technician)
        {
            context.Technicians.Remove(technician);
            context.SaveChanges();
            return RedirectToAction("List", technician);
        }
    }
}
