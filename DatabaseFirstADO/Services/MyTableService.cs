using DatabaseFirstADO.Entities;
using DatabaseFirstADO.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
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
            Type entityType = typeof(T);
            while (reader.Read())
            {
                // can we make this more Generic???? Yes we can!
                
                //if(entity == "MyTable")
                //{
                //    MyTable _myTable = new MyTable(reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2), reader.GetString(3));
                //    T myTable = (T)(object)_myTable;
                //    myTables.Add(myTable);
                //}
                //else if(entity == "Customer")
                //{
                //    Customer _customer = new Customer(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), 
                //        reader.GetString(3), reader.GetDateTime(4), reader.GetString(5), reader.GetString(6));
                //    T myTable = (T)(object)_customer;
                //    myTables.Add(myTable);
                //}

                //Create an instance of the class passed in as T and get the second constructor this class
                object passedClass = Activator.CreateInstance(entityType);
                var constructor = passedClass.GetType().GetConstructors()[1]; 
                var parameters = constructor.GetParameters().ToArray();
                //Now we have an array of all the parameters of the second constructor. We also need the count of those parameters
                int paramCount = parameters.Count();
                // We need to invoke the constructor of the passed class with the values 
                // that we are going to get from the SqlDataReader. For this we are going to
                // use the the second overload of the Invoke method which takes an object
                // of our passed class and an object array which will be our reader values
                
                object [] readValues = new object[paramCount];
                //Iterate through the array of parameters and set the value of the array object
                //readValues based on the value we get from the reader and the index of the value we get it from.
                foreach(var parameter in parameters)
                {
                    readValues.SetValue(reader.GetValue(parameter.Position), parameter.Position);
                }
                // Now we have an object array of the reader values that we can pass in the Invoke method

                constructor.Invoke(passedClass, readValues);

                // Now we can add the passed class into the generic List myTables

                myTables.Add((T)(object)passedClass);

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
            int result;
            using (SqlConnection myConnection = new SqlConnection(connectionString))
            using (SqlDataAdapter dbAdapter = new SqlDataAdapter($"SELECT * FROM dbo.{tableName}", myConnection))
            using (SqlCommandBuilder cmdBuilder = new SqlCommandBuilder(dbAdapter))
            {
                try
                {
                    myConnection.Open();

                    SqlCommand command = cmdBuilder.GetInsertCommand();
                    if (tableName == "mytable")
                    {
                        MyTable mT = aObject as MyTable;
                        command.Parameters[0].Value = mT.MyNumber;
                        command.Parameters[1].Value = mT.MyString;
                        command.Parameters[2].Value = mT.MyString2;
                    }
                    if (tableName == "customers")
                    {
                        Customer c = aObject as Customer;
                        command.Parameters[0].Value = c.FirstName;
                        command.Parameters[1].Value = c.LastName;
                        command.Parameters[2].Value = c.Email;
                        command.Parameters[3].Value = c.DateOfBirth;
                        command.Parameters[4].Value = c.LandLineTel;
                        command.Parameters[5].Value = c.MobileTel;
                    }
                    result = command.ExecuteNonQuery();
                    command.Dispose(); // we don't need to do this because Garbage Collector(GC) does his job.... I HOPE!!!
                }
                catch(SqlException ex)
                {
                    result = 0;
                }
                finally
                {

                    cmdBuilder.Dispose();
                    dbAdapter.Dispose();
                    myConnection.Close();
                }
            }
            return (result);
        }

        public int UpdateData(string connectionString, string tableName, int id, T aObject)
        {
            int result;
            using (SqlConnection myConnection = new SqlConnection(connectionString))
            using (SqlDataAdapter dbAdapter = new SqlDataAdapter($"SELECT * FROM dbo.{tableName} WHERE id = {id}", myConnection))
            using (SqlCommandBuilder cmdBuilder = new SqlCommandBuilder(dbAdapter)) // Do we need this???
            {
                myConnection.Open();

                SqlCommand command = cmdBuilder.GetUpdateCommand(); // Do I need this???
                DataTable dt = new DataTable(tableName);
                DataSet ds = new DataSet(tableName);
                dbAdapter.Fill(ds);
                Console.WriteLine($"{ds.Tables.Count}");
                dbAdapter.Fill(dt);
                if(tableName == "mytable")
                {
                    var myTable = aObject as MyTable;
                    dt.Rows[0][1] = myTable.MyNumber;
                    dt.Rows[0][2] = myTable.MyString;
                    dt.Rows[0][3] = myTable.MyString2;
                    
                }
                if(tableName == "customers")
                {
                    var customer = aObject as Customer;
                    dt.Rows[0][1] = customer.FirstName;
                    dt.Rows[0][2] = customer.LastName;
                    dt.Rows[0][3] = customer.Email;
                    dt.Rows[0][4] = customer.DateOfBirth;
                    dt.Rows[0][5] = customer.LandLineTel;
                    dt.Rows[0][6] = customer.MobileTel;
                }
                result = dbAdapter.Update(dt);
                myConnection.Close();
            }
            return (result);
        }

        public int DeleteData(string connectionString, string tableName, int id)
        {
            int result;
            using (SqlConnection myConnection = new SqlConnection(connectionString))
            using (SqlDataAdapter dbAdapter = new SqlDataAdapter($"SELECT * FROM dbo.{tableName} WHERE id = {id}", myConnection))
            using (SqlCommandBuilder cmdBuilder = new SqlCommandBuilder(dbAdapter)) // Do we need this???
            {
                myConnection.Open();

                SqlCommand command = cmdBuilder.GetDeleteCommand(); // Do we need this? 
                DataTable dt = new DataTable(tableName);
                dbAdapter.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {
                    row.Delete();
                }
                //dt.Rows[0].Delete();
                result = dbAdapter.Update(dt);
            }
            return (result);
        }
    }
}
