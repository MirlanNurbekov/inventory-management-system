using Digitalkirana.DataAccessLayer;
using System;
using System.Windows.Forms;

namespace Digitalkirana.Views
{
    public partial class AdminDashboard : Form
    {
        public AdminDashboard()
        {
            InitializeComponent();
        }
        PurchaseDAL purchaseDAL = new PurchaseDAL();
        SalesDAL salesDAL = new SalesDAL();
        SupplierDAL supplierDAL = new SupplierDAL();
        CustomerDAL customerDAL = new CustomerDAL();
        UserDAL userDAL = new UserDAL();
        ProductDAL productDAL = new ProductDAL();
        CategoryDAL categoryDAL = new CategoryDAL();

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            User user = new User();
            user.ShowDialog();
        }

        private void AdminDashboard_Load(object sender, EventArgs e)
        {
            labelUsername.Text = Login.fullName;
            loadDisplayData();
        }

        private void loadDisplayData()
        {
            var totalPurchase = purchaseDAL.GetTotalPurchase();
            var totalSales = salesDAL.GetTotalSales();
            var noOfSuppliers = supplierDAL.NoOfSuppliers();
            var noOfCustomers = customerDAL.NoOfCustomers();
            var noOfUsers = userDAL.NoOfUsers();
            var noOfProducts = productDAL.NoOfProducts();
            var noOfCategories = categoryDAL.NoOfCategories();
            grossPurchaseLbl.Text = totalPurchase.ToString();
            grossSalesLbl.Text = totalSales.ToString();
            SuppliersLbl.Text = noOfSuppliers.ToString();
            CustomersLbl.Text = noOfCustomers.ToString();
            usersLbl.Text = noOfUsers.ToString();
            productsLbl.Text = noOfProducts.ToString();
            categoriesLbl.Text = noOfCategories.ToString();
            dataGridViewLowStock.DataSource = productDAL.LowStockProducts();
        }

        private void categoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Category category = new Category();
            category.ShowDialog();
            loadDisplayData();
        }

        private void productsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Product product = new Product();
            product.ShowDialog();
            loadDisplayData();
        }

        private void suppliersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Supplier supplier = new Supplier();
            supplier.ShowDialog();
            loadDisplayData();
        }

        private void customersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Customer customer = new Customer();
            customer.ShowDialog();
            loadDisplayData();
        }

        private void transactionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Transactions transactions = new Transactions();
            transactions.ShowDialog();
            loadDisplayData();
        }

        private void inventoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Inventory inventory = new Inventory();
            inventory.ShowDialog();
            loadDisplayData();
        }
    }
}