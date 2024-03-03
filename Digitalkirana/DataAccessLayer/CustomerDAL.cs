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
    public class CustomerDAL
    {
        MySqlConnection con = new MySqlConnection(Connection.connectionString);

        #region Select Customers
        public DataTable SelectAllCustomers()
        {
            DataTable dt = new DataTable();
            try
            {
                string query = "SELECT c.Id `Customer ID`, c.CustomerName `Customer Name`, c.Email, c.Phone, c.Address, c.AddedDate `Added Date`, u.FullName `Added By` FROM customer_tbl c INNER JOIN user_tbl u ON c.AddedBy = u.Id";
                MySqlCommand cmd = new MySqlCommand(query, con);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                con.Open();
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK);
            }
            finally
            {
                con.Close();
            }
            return dt;
        }
        #endregion

        #region Insert Customers
        public bool InsertCustomer(CustomerBLL customer)
        {
            try
            {
                string query = $"INSERT INTO customer_tbl (CustomerName, Email, Phone, Address, AddedBy, AddedDate) VALUES ('{customer.CustomerName}','{customer.Email}', '{customer.Phone}','{customer.Address}',{customer.AddedBy}, '{customer.AddedDate.ToString("yyyy-MM-dd")}')";
                MySqlCommand cmd = new MySqlCommand(query, con);
                con.Open();
                int result = cmd.ExecuteNonQuery();
                if (result == 1)
                {
                    MessageBox.Show("Customer added successfully", "Added", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            MessageBox.Show("Customer could not be added", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }
        #endregion

        #region Update Customer
        public bool UpdateCustomer(CustomerBLL customer)
        {
            try
            {
                string query = $"UPDATE customer_tbl SET CustomerName = '{customer.CustomerName}', Email = '{customer.Email}', Phone = '{customer.Phone}', Address = '{customer.Address}' WHERE Id = {customer.Id}";
                MySqlCommand cmd = new MySqlCommand(query, con);
                con.Open();
                int result = cmd.ExecuteNonQuery();
                if (result == 1)
                {
                    MessageBox.Show("Customer updated successfully", "Added", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            MessageBox.Show("Customer could not be updated", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }
        #endregion

        #region Search Customer
        public DataTable SearchCustomer(string keyword)
        {
            DataTable dt = new DataTable();
            try
            {
                string query = $"SELECT * FROM customer_tbl WHERE Id LIKE '%{keyword}%' OR CustomerName LIKE '%{keyword}%'";
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

        #region Delete Customer
        public bool DeleteCustomer(CustomerBLL customer)
        {
            try
            {
                string query = $"DELETE FROM customer_tbl WHERE Id = {customer.Id}";
                MySqlCommand cmd = new MySqlCommand(query, con);
                con.Open();
                int result = cmd.ExecuteNonQuery();
                if (result == 1)
                {
                    MessageBox.Show("Customer deleted successfully", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            MessageBox.Show("Customer could not be deleted", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }
        #endregion

        #region Search Customer For Sale
        public CustomerBLL SearchCustomerForSale(string keyword)
        {
            DataTable dt = new DataTable();
            CustomerBLL customer = new CustomerBLL();
            try
            {
                string query = $"SELECT c.Id `Customer ID`, c.CustomerName `Customer Name`, c.Email, c.Phone, c.Address, c.AddedDate `Added Date`, u.FullName `Added By` FROM customer_tbl c INNER JOIN user_tbl u ON c.AddedBy = u.Id WHERE c.Id LIKE '%{keyword}%' OR c.CustomerName LIKE '%{keyword}%'";
                MySqlCommand cmd = new MySqlCommand(query, con);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                con.Open();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    customer.Id = Convert.ToInt32(dt.Rows[0]["Customer Id"]);
                    customer.CustomerName = dt.Rows[0]["Customer Name"].ToString();
                    customer.Email = dt.Rows[0]["Email"].ToString();
                    customer.Phone = dt.Rows[0]["Phone"].ToString();
                    customer.Address = dt.Rows[0]["Address"].ToString();
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

            return customer;
        }
        #endregion

        #region Get Number of Customers
        public int NoOfCustomers()
        {
            int noOfCustomers = 0;
            DataTable dt = new DataTable();
            try
            {
                string query = "SELECT COUNT(*) FROM customer_tbl";
                MySqlCommand cmd = new MySqlCommand(query, con);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                con.Open();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    noOfCustomers = Convert.ToInt32(dt.Rows[0][0]);
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
            return noOfCustomers;
        }
        #endregion

        #region Check Customer in SalesDAL 
        public bool CheckCustomerInSales(int id)
        {
            DataTable dt = new DataTable();
            try
            {
                string query = $"SELECT * FROM sales_tbl WHERE CustomerId = {id}";
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