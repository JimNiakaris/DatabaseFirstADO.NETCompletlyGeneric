using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DatabaseFirstADO
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = @"Server=NBD-LW13-N22;Database=MyDatabase;Trusted_Connection=True";
            string sql = "SELECT * FROM dbo.mytable";
            SqlConnection connection = new SqlConnection(connectionString);
            using(connection)
            {
                try
                {
                    connection.Open();
                    Console.WriteLine(connection.State);
                    SqlCommand command = new SqlCommand(sql,connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Console.WriteLine($"ID: {reader.GetInt32(0)}, my_number: {reader.GetInt32(1)}, " +
                                                                    $"my_string:{reader.GetString(2)}, " +
                                                                    $"my_string2: {reader.GetString(3)}");
                    }

                    connection.Close();
                } 
                catch(SqlException ex)
                {
                    Console.WriteLine($"Error occured: {ex.Message}");
                }
                
            }
            
        }
    }
}
