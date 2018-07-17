using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KapelMajster.Models
{
    public class CategoryViewModel
    {
        public List<Category> categories = new List<Category>();

        public CategoryViewModel()
        {
            categories.Add(new Category("Kategoria 1", "Opis 1"));
            categories.Add(new Category("Kategoria 2", "Opis 2"));
        }
    }

    public class Category
    {
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        public Category(string name, string description)
        {
            this.CategoryName = name;
            this.CategoryDescription = description;
        }
    }
}
