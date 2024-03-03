using Digitalkirana.BusinessLogicLayer;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Digitalkirana.DataAccessLayer
{
    internal class SalesDetailsDAL
    {
        MySqlConnection con = new MySqlConnection(Connection.connectionString);

        #region Insert Sales Details
        public bool InsertSalesDetails(SalesDetailsBLL sd)
        {
            bool success = false;
            try
            {
                string query = $"INSERT INTO sales_details_tbl (ProductId, Rate, Quantity, Total, CustomerId, AddedDate, AddedBy, SalesId ) VALUES ('{sd.ProductId}',{sd.Rate}, {sd.Quantity}, {sd.Total}, {sd.CustomerId}, '{sd.AddedDate.ToString("yyyy-MM-dd")}', {sd.AddedBy}, {sd.SalesId})";
                MySqlCommand cmd = new MySqlCommand(query, con);
                con.Open();
                int result = cmd.ExecuteNonQuery();
                if (result == 1)
                {
                    success = true;
                }
                else
                {
                    success = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
            return success;
        }
        #endregion

        #region Select Sales Details By Sales
        public DataTable SelectSalesDetailsBySalesId(int id)
        {
            DataTable dt = new DataTable();
            try
            {
                string query = $"SELECT p.ProductName `Product Name`, s.Rate, s.Quantity, s.Total FROM `sales_details_tbl` s INNER JOIN product_tbl p on  s.ProductId = p.Id WHERE SalesId = {id}";
                MySqlCommand cmd = new MySqlCommand(query, con);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                con.Open();
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
            return dt;
        }
        #endregion
    }
}