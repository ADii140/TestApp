using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using KapelMajster.Models;

namespace KapelMajster.Controllers
{
    public class HomeController : Controller
    {
        //public IActionResult Index()
        //{
        //    CategoryViewModel categoriesList = new CategoryViewModel();
        //    ViewData["Message"] = "Tutaj możesz dodawać kategorie do bazy";
        //    return View(categoriesList.categories);
        //}

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";
            return View();
        }

        public IActionResult Kategorie()
        {
            CategoryViewModel categoriesList = new CategoryViewModel();
            ViewData["Message"] = "Tutaj możesz dodawać kategorie do bazy";
            return View(categoriesList.categories);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult Kategorie(string CategoryName, string CategoryDescription)
        {
            Category.AddCategoryToDb(CategoryName, CategoryDescription);
            return RedirectToAction("Kategorie");

        }

        [HttpPost]
        [ActionName("Remove")]
        public IActionResult Kategorie(string item)
        {
            Category.RemoveCateogryFromDb(item);
            return RedirectToAction("Kategorie");
        }
    }

}
