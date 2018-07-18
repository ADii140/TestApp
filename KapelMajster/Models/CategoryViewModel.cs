using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace KapelMajster.Models
{
    public class CategoryViewModel
    {
        public List<Category> categories = new List<Category>();
        string connectionstring = @"Server=localhost;Integrated Security=true;database=Kapelmajster";

        public CategoryViewModel()
        {
            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                conn.Open();
                SqlCommand select = new SqlCommand(@"select kat_nazwa, kat_opis from Kategorie", conn);
                SqlDataReader reader = select.ExecuteReader();
               while(reader.Read())
                {
                    categories.Add(new Category(reader["kat_nazwa"].ToString(), reader["kat_opis"].ToString()));
                }
                conn.Close();
            }
        }
    }

    public class Category
    {
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        private static string  connectionstring = @"Server=localhost;Integrated Security=true;database=Kapelmajster";
        public Category(string name, string description)
        {
            this.CategoryName = name;
            this.CategoryDescription = description;
        }

        public static void AddCategoryToDb(string CategoryName, string CategoryDescription)
        {
            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                SqlCommand insert = new SqlCommand(@"insert into Kategorie (kat_nazwa,kat_opis) values (@name,@desc)",conn);
                insert.Parameters.AddWithValue("@name", CategoryName);
                insert.Parameters.AddWithValue("@desc", CategoryDescription);
                conn.Open();
                insert.ExecuteNonQuery();
                conn.Close();
            }
        }
        
        public static void RemoveCateogryFromDb(string CategoryName)
        {
            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                SqlCommand delete = new SqlCommand(@"delete Kategorie where kat_nazwa = @name", conn);
                delete.Parameters.AddWithValue("@name", CategoryName);
                conn.Open();
                delete.ExecuteNonQuery();
                conn.Close();
            }
        }
    }
}
