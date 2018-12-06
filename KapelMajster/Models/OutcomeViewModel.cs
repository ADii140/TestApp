using Newtonsoft.Json;
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
            CreateJsonFile();
        }

        private void CreateJsonFile()
        {
            List<object> OutcomesForJson = new List<object>();
            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = "PobierzSumyWydatkow";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@data_od", "01-12-2012");
                command.Parameters.AddWithValue("@data_do", "31-12-2018");
                command.Connection = conn;
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Dictionary<string, decimal> temp= new Dictionary<string, decimal>();
                    for (int i = 2; i < reader.FieldCount; i++)
                    {
                        Console.Write(reader.GetValue(i).GetType().ToString());
                        if (reader.GetValue(i).GetType() != typeof(DBNull)) temp.Add(reader.GetName(i),(decimal)reader.GetValue(i));
                    }
                    var tempOutcomeForJson = new { date = reader.GetValue(0), sum = reader.GetValue(1), pie = temp };
                    OutcomesForJson.Add(tempOutcomeForJson);
                }
                conn.Close();
                string json = JsonConvert.SerializeObject(OutcomesForJson,Formatting.Indented);
                System.IO.File.WriteAllText(@"wwwroot\json\data.json", json);
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
