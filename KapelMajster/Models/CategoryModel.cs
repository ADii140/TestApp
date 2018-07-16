using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace KapelMajster.Models
{
    public class CategoryModel
    {
        public List<Category> Category = new List<Category>();

        public CategoryModel()
        {
            string connectionString = "";
            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand("SELECT TOP 2 * FROM Dogs1", conn))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        Category.Add(new Category(reader.GetString(0), reader.GetString(1)));
                    }
                }
            }
        }
    }
}
