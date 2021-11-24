using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseFirstADO.Interfaces
{
    interface IDataService<T>
    {

        // InsertData - (C)reate
        int InsertData(string connectionString, string tableName, T aObject);

        // SelectData - (R)ead
        ICollection<T> SelectData(string sql, SqlConnection connection);

        // UpdateData - (U)pdate
        int UpdateData(string connectionString, string tableName, int id, T aObject);

        // DeleteData - (D)elete
        int DeleteData(string connectionString, string tableName, int id);

        // CreateTable - create a table using the parameters passed
        // to be continued!
    }
}
