﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using KapelMajster.Models;
using System;

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
            ViewData["Message"] = "Tutaj możesz dodawać kategorie";
            if (Request.QueryString.Value.Contains("Error"))
            {
                int errorCode = int.Parse(Request.QueryString.Value.Substring(Request.QueryString.Value.Length-3));
                if (errorCode.Equals(547)) TempData["sErrMsg"] = "Kategoria nie została usunięta, ponieważ są podpięte do niej wydatki";
            }
            return View(categoriesList.categories);
        }

        public PartialViewResult ShowError(String sErrorMessage)
        {
            return PartialView("_PartialErrorView.cshtml");
        }

        public IActionResult Wydatki()
        {
            CategoryViewModel categories = new CategoryViewModel();
            ViewBag.CategoryList = categories.categories;
            OutcomeViewModel outcomeList = new OutcomeViewModel();
            ViewData["Message"] = "Tutaj możesz dodawać wydatki";
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
        public IActionResult Kategorie(int CategoryId)
        {
            try { Category.RemoveCateogryFromDb(CategoryId); }
            catch (System.Data.SqlClient.SqlException e)
            {
                return RedirectToAction("Kategorie", new { Error = e.Number });
            }
            return RedirectToAction("Kategorie");
        }

        [HttpPost]
        public IActionResult Wydatki(string OutcomeName, string OutcomeValue, int OutcomeCategoryId,string OutcomeDate)
        {
            OutcomeValue = OutcomeValue.Replace(',', '.');
            Outcome.AddOutcomeToDb(OutcomeName, OutcomeValue, OutcomeCategoryId, OutcomeDate);
            return RedirectToAction("Wydatki");
        }

        [HttpPost]
        [ActionName("Remove")]
        public IActionResult Wydatki(int OutcomeId)
        {
            Outcome.RemoveOutcomeFromDb(OutcomeId);
            return RedirectToAction("Wydatki");
        }
    }

}
