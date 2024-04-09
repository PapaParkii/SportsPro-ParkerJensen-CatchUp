using Microsoft.AspNetCore.Mvc;
using SportsPro.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace SportsPro.Controllers
{
    // Controller for managing incidents
    public class IncidentsController : Controller
    {
        // Database context for accessing incidents
        private SportsProContext context { get; set; }

        // Constructor to initialize the database context
        public IncidentsController(SportsProContext ctx)
        {
            context = ctx;
        }

        // Action to display a list of all incidents
        [Route("Incidents")]
        public ViewResult Incident()
        {
            var incidents = context.Incidents.ToList();
            return View(incidents);
        }

        // Action to display the form for adding a new incident
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            return View("Edit", new Incident());
        }

        // Action to display the form for editing an existing incident
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            var incident = context.Incidents.Find(id);
            return View(incident);
        }

        // Action to handle the submission of the incident edit form
        [HttpPost]
        public IActionResult Edit(Incident incident)
        {
            if (ModelState.IsValid)
            {
                if (incident.IncidentID == 0)
                    context.Incidents.Add(incident);
                else
                    context.Incidents.Update(incident);
                context.SaveChanges();
                return RedirectToAction("List", incident);
            }
            else
            {
                ViewBag.Action = (incident.IncidentID == 0) ? "Add" : "Edit";
                return View(incident);
            }
        }

        // Action to display the form for confirming the deletion of an incident
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var incident = context.Incidents.Find(id);
            return View(incident);
        }

        // Action to handle the deletion of an incident
        [HttpPost]
        public IActionResult Delete(Incident incident)
        {
            context.Incidents.Remove(incident);
            context.SaveChanges();
            return RedirectToAction("List", incident);
        }
    }
}
