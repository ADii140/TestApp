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

        public IActionResult Kategorie()
        {
            CategoryViewModel categoriesList = new CategoryViewModel();
            ViewData["Message"] = "Tutaj możesz dodawać kategorie do bazy";
            return View(categoriesList.categories);
        }

        public IActionResult Wydatki()
        {
            OutcomeViewModel outcomeList = new OutcomeViewModel();
            ViewData["Message"] = "Tutaj możesz dodawać wydatki do bazy";
            return View(outcomeList.outcomes);
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
        [ActionName("RemoveCategory")]
        public IActionResult Kategorie(int item)
        {
            Category.RemoveCateogryFromDb(item);
            return RedirectToAction("Kategorie");
        }

        [HttpPost]
        [ActionName("RemoveCategory")]
        public IActionResult Kategorie(int item)
        {
            Category.RemoveCateogryFromDb(item);
            return RedirectToAction("Kategorie");
        }

        [HttpPost]
        public IActionResult Wydatki(string OutcomeName, string OutcomeValue, string OutcomeCategory)
        {
            Outcome.AddOutcomeToDb(OutcomeName, OutcomeValue, OutcomeCategory,"12/12/2012");
            return RedirectToAction("Wydatki");
        }

        [HttpPost]
        [ActionName("Remove")]
        public IActionResult Wydatki(string OutcomeName, string OutcomeValue, string OutcomeCategory, string OutcomeDate)
        {
            Outcome.RemoveOutcomeFromDb(OutcomeName, OutcomeValue, OutcomeCategory, OutcomeDate);
            return RedirectToAction("Wydatki");
        }
    }

}
