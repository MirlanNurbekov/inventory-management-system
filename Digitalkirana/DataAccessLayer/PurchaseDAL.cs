using Digitalkirana.BusinessLogicLayer;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Digitalkirana.DataAccessLayer
{
    public class PurchaseDAL
    {
        MySqlConnection con = new MySqlConnection(Connection.connectionString);

        #region Insert Purchase
        public bool InsertPurchase(PurchaseBLL purchase, out int purchaseId)
        {
            bool success = false;
            purchaseId = -1;
            try
            {
                string query = $"INSERT INTO purchase_tbl (SupplierId, GrandTotal, Date, Tax, Discount, AddedBy ) VALUES ({purchase.SupplierId}, {purchase.GrandTotal},'{purchase.Date.ToString("yyyy-MM-dd")}', {purchase.Tax}, {purchase.Discount}, {purchase.AddedBy}); SELECT LAST_INSERT_ID()";
                MySqlCommand cmd = new MySqlCommand(query, con);
                con.Open();
                var obj = cmd.ExecuteScalar();
                if (obj!=null)
                {
                    purchaseId = int.Parse(obj.ToString());
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

        #region Select Purchase Transactions
        public DataTable SelectPurchaseTransactions()
        {
            DataTable dt = new DataTable();
            try
            {
                string query = "SELECT p.Id `Purchase ID`, s.SupplierName `Supplier Name`, p.GrandTotal `Grand Total`, p.Tax, p.Discount, p.Date, u.FullName `Added By` FROM purchase_tbl p INNER JOIN supplier_tbl s on p.SupplierId = s.Id INNER JOIN user_tbl u on u.Id = p.AddedBy";
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

        #region Select Purchase Transactions
        public DataTable SelectTodaysPurchaseTransactions()
        {
            DataTable dt = new DataTable();
            try
            {
                string query = $"SELECT p.Id `Purchase ID`, s.SupplierName `Supplier Name`, p.GrandTotal `Grand Total`, p.Tax, p.Discount, p.Date, u.FullName `Added By` FROM purchase_tbl p INNER JOIN supplier_tbl s on p.SupplierId = s.Id INNER JOIN user_tbl u on u.Id = p.AddedBy where p.Date = '{DateTime.Now.ToString("yyyy-MM-dd")}'";
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

        #region Get Today's Grand Total From Purchase By Username
        public Decimal GetTotalPurchaseByUsername(int userId)
        {
            Decimal totalPurchase = 0;
            DataTable dt = new DataTable();
            try
            {
                string query = $"SELECT GrandTotal FROM `purchase_tbl` WHERE AddedBy = {userId} AND Date = '{DateTime.Now.ToString("yyyy-MM-dd")}'";
                MySqlCommand cmd = new MySqlCommand(query, con);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                con.Open();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        totalPurchase = totalPurchase + Convert.ToDecimal(dt.Rows[i][0]);
                    }
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
            return totalPurchase;
        }
        #endregion

        #region Get Today's Grand Total From Purchase
        public Decimal GetTotalPurchase()
        {
            Decimal totalPurchase = 0;
            DataTable dt = new DataTable();
            try
            {
                string query = $"SELECT GrandTotal FROM `purchase_tbl` WHERE  Date = '{DateTime.Now.ToString("yyyy-MM-dd")}'";
                MySqlCommand cmd = new MySqlCommand(query, con);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                con.Open();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        totalPurchase = totalPurchase + Convert.ToDecimal(dt.Rows[i][0]);
                    }
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
            return totalPurchase;
        }
        #endregion
    }
}