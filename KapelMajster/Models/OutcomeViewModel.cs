using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace KapelMajster.Models
{
    public class OutcomeViewModel
    {
        public List<Outcome> outcomes = new List<Outcome>();
        string connectionstring = @"Server=localhost;Integrated Security=true;database=Kapelmajster";

        public OutcomeViewModel()
        {
            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                conn.Open();
                SqlCommand select = new SqlCommand(@"select wyd_nazwa, wyd_wartosc, wyd_kategoria, wyd_data from Wydatki", conn);
                SqlDataReader reader = select.ExecuteReader();
                while (reader.Read())
                {
                    outcomes.Add(new Outcome(reader["wyd_nazwa"].ToString(),
                        reader["wyd_wartosc"].ToString(),
                        reader["wyd_kategoria"].ToString(),
                        reader["wyd_data"].ToString()
                        ));
                }
                conn.Close();
            }
        }
    }

    public class Outcome
    {
        public string OutcomeName { get; set; }
        public string OutcomeValue { get; set; }
        public string OutcomeCategory { get; set; }
        public string OutcomeDate { get; set; }
        private static string connectionstring = @"Server=localhost;Integrated Security=true;database=Kapelmajster";
        public Outcome(string name, string value, string category, string date)
        {
            this.OutcomeName = name;
            this.OutcomeValue = value;
            this.OutcomeCategory = category;
            this.OutcomeDate = date;
        }

        public static void AddOutcomeToDb(string OutcomeName, string OutcomeValue, string OutcomeCategory, string OutcomeDate)
        {
            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                SqlCommand insert = new SqlCommand(@"insert into Wydatki (wyd_nazwa, wyd_wartosc, wyd_kategoria, wyd_data) values (@name,@value,@category,@date)", conn);
                insert.Parameters.AddWithValue("@name", OutcomeName);
                insert.Parameters.AddWithValue("@value", OutcomeValue);
                insert.Parameters.AddWithValue("@category", OutcomeCategory);
                insert.Parameters.AddWithValue("@date", OutcomeDate);
                conn.Open();
                insert.ExecuteNonQuery();
                conn.Close();
            }
        }

        public static void RemoveOutcomeFromDb(string OutcomeName, string OutcomeValue, string OutcomeCategory, string OutcomeDate)
        {
            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                SqlCommand delete = new SqlCommand(@"delete Kategorie where wyd_nazwa = @name and wyd_kategoria = @category and wyd_data = @date and wyd_wartosc = @value", conn);
                delete.Parameters.AddWithValue("@name", OutcomeName);
                delete.Parameters.AddWithValue("@category", OutcomeCategory);
                delete.Parameters.AddWithValue("@value", OutcomeValue);
                delete.Parameters.AddWithValue("@date", OutcomeDate);
                conn.Open();
                delete.ExecuteNonQuery();
                conn.Close();
            }
        }
    }
}
