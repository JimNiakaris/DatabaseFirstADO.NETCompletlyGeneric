using DatabaseFirstADO.Entities;
using DatabaseFirstADO.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseFirstADO.Services
{
    class MyTableService<T>: IDataService<T>
    {
        public string connectionString = @"Server=NBD-LW13-N;Database=MyDatabase;Trusted_Connection=True";

        public SqlConnection connection {
            get;
            private set;
        }
        private string entity;

        public MyTableService(string sql, string entity)
        {
            
            this.entity = entity;
            //string sql = "SELECT * FROM dbo.mytable";
            //string sql2 = "SELECT * FROM dbo.mytable WHERE id = 1002";
            using (connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    Console.WriteLine(connection.State);
                    ICollection<T> myTables1 = SelectData(sql, connection);
                    PrintData(myTables1);
                    //ICollection<T> myTables2 = SelectData(sql2, connection);
                    //PrintData(myTables2);
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"Error occured: {ex.Message}");
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public ICollection<T> SelectData(string sql, SqlConnection connection)
        {
            ICollection<T> myTables = new List<T>();
            SqlCommand command = new SqlCommand(sql, connection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                // can we make this more Generic????
                if(entity == "MyTable")
                {
                    MyTable _myTable = new MyTable(reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2), reader.GetString(3));
                    T myTable = (T)(object)_myTable;
                    myTables.Add(myTable);
                }
                else if(entity == "Customer")
                {
                    Customer _customer = new Customer(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), 
                        reader.GetString(3), reader.GetDateTime(4), reader.GetString(5), reader.GetString(6));
                    T myTable = (T)(object)_customer;
                    myTables.Add(myTable);
                }
            }
            if (!reader.IsClosed)
            {
                reader.Close();
            }
            return (myTables);
        }

        public void PrintData(ICollection<T> table)
        {
            List<T> myTables = (List<T>)table;
            foreach (var item in myTables)
            {
                Console.WriteLine(item.ToString());
            }
        }

        public int InsertData(string connectionString, string tableName, T aObject)
        {

            using (SqlConnection myConnection = new SqlConnection(connectionString))
            using (SqlDataAdapter dbAdapter = new SqlDataAdapter("SELECT first_name, last_name FROM dbo.customers", myConnection))
            using (SqlCommandBuilder cmdBuilder = new SqlCommandBuilder(dbAdapter))
            {
                myConnection.Open();

                SqlCommand command = cmdBuilder.GetInsertCommand();
                //SqlParameterCollection parameters = command.Parameters;
            
                Console.WriteLine(command.CommandText);
            }
            

            return (0);
        }
    }
}
