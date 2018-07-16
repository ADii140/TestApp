using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KapelMajster.Models;

namespace KapelMajster.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            List<Category> categories = new CategoryModel().Category;
            ViewData["Message"] = "Tutaj możesz dodawać kategorie do bazy";
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Kategorie()
        { 

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult Index(Category dane)
        {
            string CategoryName = dane.CategoryName;
            return View();
        }
    }
}
