using Digitalkirana.BusinessLogicLayer;
using Digitalkirana.Views;
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
    public class ProductDAL
    {

        MySqlConnection con = new MySqlConnection(Connection.connectionString);

        #region Select Products
        public DataTable SelectAllProducts()
        {
            DataTable dt = new DataTable();
            try
            {
                string query = "SELECT p.Id AS `Product ID`, p.ProductName AS `Product Name`, p.Category, p.Description, p.Rate, p.Quantity, p.AddedDate AS `Added Date`, u.FullName AS `Added By` FROM `product_tbl` AS p INNER JOIN user_tbl AS u ON P.AddedBy = u.Id";
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

        #region Insert Product
        public bool InsertProduct(ProductBLL product)
        {
            try
            {
                string query = $"INSERT INTO product_tbl (Id, ProductName, Category, Description, Rate, Quantity, AddedBy, AddedDate ) VALUES ('{product.Id}','{product.ProductName}', '{product.Category}', '{product.Description}', {product.Rate}, {product.Quantity}, {product.AddedBy},'{product.AddedDate.ToString("yyyy-MM-dd")}')";
                MySqlCommand cmd = new MySqlCommand(query, con);
                con.Open();
                int result = cmd.ExecuteNonQuery();
                if (result == 1)
                {
                    MessageBox.Show("Product added successfully", "Added", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
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
            MessageBox.Show("Product could not be added", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }
        #endregion

        #region Update Product
        public bool UpdateProduct(ProductBLL product)
        {
            try
            {
                string query = $"UPDATE product_tbl SET ProductName='{product.ProductName}', Category = '{product.Category}', Description = '{product.Description}', Rate = {product.Rate}, Quantity = {product.Quantity} WHERE Id = '{product.Id}'";
                MySqlCommand cmd = new MySqlCommand(query, con);
                con.Open();
                int result = cmd.ExecuteNonQuery();
                if (result == 1)
                {
                    MessageBox.Show("Product updated successfully", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
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
            MessageBox.Show("Product could not be updated", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }
        #endregion

        #region Search Product
        public DataTable SearchProduct(string keyword)
        {
            DataTable dt = new DataTable();
            try
            {
                string query = $"SELECT p.Id AS `Product ID`, p.ProductName AS `Product Name`, p.Category, p.Description, p.Rate, p.Quantity, p.AddedDate AS `Added Date`, u.FullName AS `Added By` FROM `product_tbl` AS p INNER JOIN user_tbl AS u ON P.AddedBy = u.Id WHERE p.Id LIKE '%{keyword}%' OR p.ProductName LIKE '%{keyword}%'";
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

        #region Delete Product
        public bool DeleteProduct(string productId)
        {
            try
            {
                string query = $"DELETE FROM product_tbl WHERE Id = '{productId}'";
                MySqlCommand cmd = new MySqlCommand(query, con);
                con.Open();
                int result = cmd.ExecuteNonQuery();
                if (result == 1)
                {
                    MessageBox.Show("Product deleted successfully", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
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
            MessageBox.Show("Product could not be deleted", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }
        #endregion

        #region Search Product For Purchase
        public ProductBLL SearchProductForPurchase(string keyword)
        {
            DataTable dt = new DataTable();
            ProductBLL product = new ProductBLL();
            try
            {
                string query = $"SELECT Id, ProductName, Rate, Quantity FROM product_tbl WHERE Id LIKE '%{keyword}%' OR ProductName LIKE '%{keyword}%'";
                MySqlCommand cmd = new MySqlCommand(query, con);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                con.Open();
                da.Fill(dt);
                if(dt.Rows.Count > 0)
                {
                    product.Id = dt.Rows[0]["Id"].ToString();
                    product.ProductName = dt.Rows[0]["ProductName"].ToString();
                    product.Rate = Convert.ToDecimal(dt.Rows[0]["Rate"]);
                    product.Quantity = Convert.ToDecimal(dt.Rows[0]["Quantity"]);
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
            return product;
        }
        #endregion

        #region Current Quantity Based On ProductID
        public Decimal GetProductQuantity(string productId)
        {
            Decimal quantity = 0;
            DataTable dt = new DataTable();
            try
            {
                string query = $"SELECT Quantity FROM product_tbl Where Id = {productId}";
                MySqlCommand cmd = new MySqlCommand(query, con);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                con.Open();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    quantity = Convert.ToDecimal(dt.Rows[0]["Quantity"]);
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
            return quantity;
        }
        #endregion

        #region Update Quantity
        public bool UpdateQuantity(string productId, decimal quantity)
        {
            try
            {
                string query = $"UPDATE product_tbl SET Quantity={quantity} WHERE Id = '{productId}'";
                MySqlCommand cmd = new MySqlCommand(query, con);
                con.Open();
                int result = cmd.ExecuteNonQuery();
                if (result == 1)
                {
                    return true;
                }
                else
                {
                    return false;
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
            return false;
        }
        #endregion

        #region Increase Quantity
        public bool IncreaseQuantity(string productId, decimal incresaseQuantity)
        {
            try
            {
                decimal currentQuantity = GetProductQuantity(productId);
                decimal newQuantity = currentQuantity + incresaseQuantity;
                return UpdateQuantity(productId, newQuantity);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
            return false;
        }
        #endregion

        #region Decrease Quantity
        public bool DecreaseQuantity(string productId, decimal decreaseQuantity)
        {
            try
            {
                decimal currentQuantity = GetProductQuantity(productId);
                decimal newQuantity = currentQuantity - decreaseQuantity;
                return UpdateQuantity(productId, newQuantity);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
            return false;
        }
        #endregion

        #region Select Products by Category
        public DataTable SelectProductsByCategory(string categoryName)
        {
            DataTable dt = new DataTable();
            try
            {
                string query = $"SELECT * FROM product_tbl WHERE Category = '{categoryName}'";
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

        #region Get Number of Products
        public int NoOfProducts()
        {
            int noOfProducts = 0;
            DataTable dt = new DataTable();
            try
            {
                string query = "SELECT COUNT(*) FROM product_tbl";
                MySqlCommand cmd = new MySqlCommand(query, con);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                con.Open();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    noOfProducts = Convert.ToInt32(dt.Rows[0][0]);
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
            return noOfProducts;
        }
        #endregion

        #region Get Low Stock Products
        public DataTable LowStockProducts()
        {
            DataTable dt = new DataTable();
            try
            {
                string query = "SELECT Id, ProductName `Product Name` FROM product_tbl WHERE Quantity < 5";
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

        #region Check Duplicate ProductId
        public bool CheckDuplicateProductId(string id)
        {
            DataTable dt = new DataTable();
            try
            {
                string query = $"SELECT * FROM product_tbl WHERE Id = '{id}'";
                MySqlCommand cmd = new MySqlCommand(query, con);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                con.Open();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    return true;
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
            return false;
        }
        #endregion

        #region Check Product in Product Details 
        public bool CheckProductInPurchaseDetails(string id)
        {
            DataTable dt = new DataTable();
            try
            {
                string query = $"SELECT * FROM purchase_details_tbl WHERE ProductId = {id}";
                MySqlCommand cmd = new MySqlCommand(query, con);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                con.Open();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    return true;
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
            return false;
        }
        #endregion

        #region Check Product in Sales Details 
        public bool CheckProductInSalesDetails(string id)
        {
            DataTable dt = new DataTable();
            try
            {
                string query = $"SELECT * FROM sales_details_tbl WHERE ProductId = {id}";
                MySqlCommand cmd = new MySqlCommand(query, con);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                con.Open();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    return true;
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
            return false;
        }
        #endregion
    }
}
