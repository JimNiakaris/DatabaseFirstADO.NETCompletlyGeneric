using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using DatabaseFirstADO.entities;

namespace DatabaseFirstADO
{
    class Program<T>
    {
        static void Main(string[] args)
        {
            string connectionString = @"Server=NBD-LW13-N;Database=MyDatabase;Trusted_Connection=True";
            string sql = "SELECT * FROM dbo.mytable";
            string sql2 = "SELECT * FROM dbo.mytable WHERE id = 1002";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    Console.WriteLine(connection.State);
                    ICollection<T> myTables1 = SelectData(sql, connection);
                    PrintData(myTables1);
                    SelectData(sql2, connection);
                } 
                catch(SqlException ex)
                {
                    Console.WriteLine($"Error occured: {ex.Message}");
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        static ICollection<T> SelectData(string sql, SqlConnection connection)
        {
            ICollection<MyTable> myTables = new List<MyTable>();
            SqlCommand command = new SqlCommand(sql, connection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                MyTable myTable = new MyTable(reader.GetInt32(0), reader.GetInt32(1) , reader.GetString(2), reader.GetString(3));
                myTables.Add(myTable);
            }
            if(!reader.IsClosed)
            {
                reader.Close();
            }
            return ((ICollection<T>)myTables);
        }

        static void PrintData(ICollection<T> table)
        {
            List<MyTable> myTables = table;
            Console.WriteLine(table[0]);
        }
    }
}
