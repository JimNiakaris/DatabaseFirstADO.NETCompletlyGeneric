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
            MyTableService<MyTable> myTableService = new MyTableService<MyTable>(sql, "MyTable");
            myTableService.InsertData(myTableService.connection, "Customer", new MyTable());

            sql = "SELECT * FROM dbo.Customers"; 
            MyTableService<Customer> myTableService2 = new MyTableService<Customer>(sql, "Customer");



            
        }

        
    }
}
