using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KapelMajster.Models
{
    public class Category
    {
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }

        public Category(string name, string desc)
        {
            CategoryName = name;
            CategoryDescription = desc;
        }
    }
}
