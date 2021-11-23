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
        int InsertData(SqlConnection connection, string tableName, T aObject);

        // SelectData - (R)ead
        ICollection<T> SelectData(string sql, SqlConnection connection);

        // UpdateData - (U)pdate
        // DeleteData - (D)elete


        // CreateTable - create a table using the parameters passed
    }
}
