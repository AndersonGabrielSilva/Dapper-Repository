using Microsoft.Data.SqlClient;

namespace Dapper_Blog 
{
    public static class DataBase
    {
        public static SqlConnection Connection { get; set; };
    }
}