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
    public class PurchaseDetailsDAL
    {
        MySqlConnection con = new MySqlConnection(Connection.connectionString);

        #region Insert Purchase Details
        public bool InsertPurchaseDetails(PurchaseDetailsBLL pd)
        {
            bool success = false;
            try
            {
                string query = $"INSERT INTO purchase_details_tbl (ProductId, Rate, Quantity, Total, SupplierId, AddedDate, AddedBy, PurchaseId ) VALUES ('{pd.ProductId}',{pd.Rate}, {pd.Quantity}, {pd.Total}, {pd.SupplierId}, '{pd.AddedDate.ToString("yyyy-MM-dd")}', {pd.AddedBy}, {pd.PurchaseId})";
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

        #region Select Purchase Details By PurchaseId
        public DataTable SelectPurchaseDetailsByPurchaseId(int id)
        {
            DataTable dt = new DataTable();
            try
            {
                string query = $"SELECT product.ProductName `Product Name`, p.Rate, p.Quantity, p.Total FROM purchase_details_tbl p INNER JOIN product_tbl product on  product.Id = p.ProductId WHERE PurchaseId = {id}";
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