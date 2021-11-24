using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using DatabaseFirstADO.Entities;
using DatabaseFirstADO.Services;

namespace DatabaseFirstADO
{
    class Program
    {
        static void Main(string[] args)
        {
            string sql = "SELECT * FROM dbo.mytable";
            //MyTableService<MyTable> myTableService = new MyTableService<MyTable>(sql, "MyTable");
            //int result = myTableService.InsertData(myTableService.connectionString, "mytable", new MyTable(0, 99, "Hello", "World"));
            //Console.WriteLine($"Inserted {result} row(s)!");
            ////Console.Clear();
            //myTableService = new MyTableService<MyTable>(sql, "MyTable");

            sql = "SELECT * FROM dbo.customers";
            MyTableService<Customer> myTableService2 = new MyTableService<Customer>(sql, "Customer");
            //int result2 = myTableService2.InsertData(myTableService2.connectionString, "customers",
            //    new Customer(0, "Bill", "Gates", "billgates@microsoft.com", new DateTime(1962, 10, 11), "001239812789", "0011923789231"));
            //Console.WriteLine($"Inserted {result2} row(s)!");
            ////Console.Clear();
            //myTableService2 = new MyTableService<Customer>(sql, "Customer");
            //int result2 = myTableService2.UpdateData(myTableService2.connectionString, "customers", 17,
            //    new Customer(0, "John", "Wayne", "john@wayne.org", new DateTime(1947, 2, 3), "1234", "1234"));
            //Console.WriteLine($"Updated {result2} row(s)!");

            int result2 = myTableService2.DeleteData(myTableService2.connectionString, "customers", 17);
            Console.WriteLine($"Succesfully deleted {result2} row(s)!");
            new MyTableService<Customer>(sql, "Customer");
            Console.ReadKey();
        }
    }
}
