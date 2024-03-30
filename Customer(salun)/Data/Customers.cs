using Customer_salun_.Data.Models;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Windows.Forms;

namespace Customer_salun_.Data
{
    public static class Customers
    {
        public static DataTable GetAll()
        {
            OracleCommand cmd = new OracleCommand("CustomerGet", Connection.GetConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            OracleDataAdapter adapter = new OracleDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            return dt;


        }
        public static Customer Get(int id)
        {
            OracleCommand cmd = new OracleCommand("CustomerGet", Connection.GetConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("CustomerId", id);
            OracleDataReader reader = cmd.ExecuteReader();
            Customer customer = null;
            if (reader.Read())
            {
                customer = new Customer();
                customer.CustomerName = reader["CustomerName"].ToString();
                customer.Sex = char.Parse(reader["Sex"].ToString());
                customer.DoB = DateTime.Parse(reader["DOB"].ToString());
                customer.PoB = reader["POB"].ToString();
                customer.Phone = reader["Phone"].ToString();
                customer.Email = reader["Email"].ToString();
            }
            reader.Close();

            return customer;
        }
        public static int Add(Customer cus)
        {
            int id = 0;
            try
            {
                OracleCommand cmd = new OracleCommand("CustomerAdd", Connection.GetConnection());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("CustomerName", cus.CustomerName);
                cmd.Parameters.Add("Sex", cus.Sex);
                cmd.Parameters.Add("DOB", cus.DoB);
                cmd.Parameters.Add("POB", cus.PoB);
                cmd.Parameters.Add("Phone", cus.Phone);
                cmd.Parameters.Add("Email", cus.Email);
                cmd.Parameters.Add("CustomerId", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                id = Convert.ToInt32(cmd.Parameters["CustomerId"].Value.ToString());

            }
            catch
            {
                MessageBox.Show("customer id is null value");
            }
            return id;
        }
        public static void Update(Customer cus)
        {
            try
            {
                OracleCommand cmd = new OracleCommand("CustomerUpdate", Connection.GetConnection());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("CustomerId", cus.CustomerId);
                cmd.Parameters.Add("CustomerName", cus.CustomerName);
                cmd.Parameters.Add("Sex", cus.Sex);
                cmd.Parameters.Add("DOB", cus.DoB);
                cmd.Parameters.Add("POB", cus.PoB);
                cmd.Parameters.Add("Phone", cus.Phone);
                cmd.Parameters.Add("Email", cus.Email);
                cmd.ExecuteNonQuery();
            }
            catch
            {
                MessageBox.Show("Exception");
            }
        }
        public static void Delete(int customerId)
        {
            try
            {
                OracleCommand cmd = new OracleCommand("CustomerDelete", Connection.GetConnection());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("CustomerId", customerId);
                cmd.ExecuteNonQuery();

            }
            catch
            {
                MessageBox.Show("Exception");
            }
        }
    }
}
