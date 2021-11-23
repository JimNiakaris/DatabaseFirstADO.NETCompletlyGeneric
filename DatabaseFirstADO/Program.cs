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
            MyTableService<MyTable> myTableService = new MyTableService<MyTable>();
        }

        
    }
}
