﻿using Microsoft.AspNetCore.Mvc;

namespace SportsPro.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Product()
        {
            return View();
        }
    }
}