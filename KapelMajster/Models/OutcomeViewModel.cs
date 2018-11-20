using System;
using System.Collections.Generic;
using System.Data;
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
                SqlCommand command = new SqlCommand();
                command.CommandText = "PobierzWydatki";
                command.CommandType = CommandType.StoredProcedure;
                command.Connection = conn;
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    outcomes.Add(new Outcome(int.Parse(reader["wyd_id"].ToString()),
                        reader["wyd_nazwa"].ToString(),
                        reader["wyd_kwota"].ToString(),
                        reader["wyd_data"].ToString(),
                        new Category(int.Parse(reader["wyd_kat_id"].ToString()),reader["kat_nazwa"].ToString())
                        ));
                }
                conn.Close();
            }
        }
    }

    public class Outcome
    {
        public int OutcomeId { get; set; }
        public string OutcomeName { get; set; }
        public string OutcomeValue { get; set; }
        public Category OutcomeCategory { get; set; }
        public string OutcomeDate { get; set; }
        private static string connectionstring = @"Server=localhost;Integrated Security=true;database=Kapelmajster";
        public Outcome(int id, string name, string value, string date, Category category)
        {
            this.OutcomeId = id;
            this.OutcomeName = name;
            this.OutcomeValue = value;
            this.OutcomeCategory = category;
            this.OutcomeDate = date;
       
        }

        public static void AddOutcomeToDb(string OutcomeName, string OutcomeValue, int OutcomeCategoryId, string OutcomeDate)
        {
            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = "DodajWydatek";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@kwota",OutcomeValue);
                command.Parameters.AddWithValue("@nazwa", OutcomeName==null?"":OutcomeName);
                command.Parameters.AddWithValue("@data", OutcomeDate);
                command.Parameters.AddWithValue("@kat_nazwa", OutcomeCategoryId);
                command.Connection = conn;
                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
            }
        }



        public static void RemoveOutcomeFromDb(int OutcomeId)
        {
            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = "UsunWydatek";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id", OutcomeId);
                command.Connection = conn;
                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
            }
        }
    }
}
