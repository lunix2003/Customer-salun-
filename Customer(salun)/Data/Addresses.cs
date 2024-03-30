using Customer_salun_.Data.Models;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Windows.Forms;

namespace Customer_salun_.Data
{
    public static class Addresses
    {
        public static DataTable Get(int customerId)
        {
            OracleCommand cmd = new OracleCommand("AddressGet", Connection.GetConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("CustomerId", customerId);
            OracleDataAdapter adapter = new OracleDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            return dt;
        }
        public static void Add(Address address)
        {
            try
            {
                OracleCommand cmd = new OracleCommand("AddressAdd", Connection.GetConnection());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("CustomerId", address.CustomerId);
                cmd.Parameters.Add("AddressName", address.AddressName);
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public static void Delete(int customerId)
        {
            try
            {
                OracleCommand cmd = new OracleCommand("AddressDelete", Connection.GetConnection());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("CustomerId", customerId);
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
