using Oracle.ManagedDataAccess.Client;

namespace Customer_salun_.Data
{
    public class Connection
    {
        static OracleConnection conn;
        public static OracleConnection GetConnection()
        {
            conn = new OracleConnection("Data Source = localhost:1521/XEPDB1; User id = Customer; Password = customer123");
            if (conn != null)
            {
                conn.Open();
            }
            return conn;
        }
    }
}