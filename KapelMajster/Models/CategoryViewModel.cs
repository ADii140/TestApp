using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

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
                SqlCommand command = new SqlCommand();
                command.CommandText = "PobierzKategorie";
                command.CommandType = CommandType.StoredProcedure;
                command.Connection = conn;
                SqlDataReader reader = command.ExecuteReader();
               while(reader.Read())
                {
                    categories.Add(new Category(int.Parse(reader["kat_id"].ToString()),reader["kat_nazwa"].ToString(), reader["kat_opis"].ToString()));
                }
                conn.Close();
            }
        }
    }

    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        private static string  connectionstring = @"Server=localhost;Integrated Security=true;database=Kapelmajster";
        public Category(int id, string name, string description)
        {
            this.CategoryId = id;
            this.CategoryName = name;
            this.CategoryDescription = description;
        }

        public Category(int id, string name)
        {
            this.CategoryId = id;
            this.CategoryName = name;
        }

        public static void AddCategoryToDb(string CategoryName, string CategoryDescription)
        {
            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = "DodajKategorie";
                command.CommandType = CommandType.StoredProcedure;
                command.Connection = conn;
                command.Parameters.AddWithValue("@nazwa", CategoryName);
                command.Parameters.AddWithValue("@opis", CategoryDescription);
                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
            }
        }
        
        public static void RemoveCateogryFromDb(int CategoryId)
        {
            using (SqlConnection conn = new SqlConnection(connectionstring))
            {

                SqlCommand command = new SqlCommand();
                command.CommandText = "UsunKategorie";
                command.CommandType = CommandType.StoredProcedure;
                command.Connection = conn;
                command.Parameters.AddWithValue("@id", CategoryId);
                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
            }
        }
    }
}
