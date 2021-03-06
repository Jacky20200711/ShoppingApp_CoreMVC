﻿using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ShoppingApp.Models;

namespace ShoppingApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToRoute(new { controller = "Product", action = "ShowProducts" });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}