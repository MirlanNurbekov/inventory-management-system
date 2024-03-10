using Digitalkirana.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Digitalkirana.Views
{
    public partial class Inventory : Form
    {
        public Inventory()
        {
            InitializeComponent();
        }

        ProductDAL productDAL = new ProductDAL();
        CategoryDAL categoryDAL = new CategoryDAL();

        private void Inventory_Load(object sender, EventArgs e)
        {
            loadCategoryInComboBox();
        }

        private void loadCategoryInComboBox()
        {
            dataGridViewInventory.DataSource = productDAL.SelectAllProducts();
            comboBoxCategory.DataSource = categoryDAL.SelectAllCategories();
            comboBoxCategory.DisplayMember = "CategoryName";
            comboBoxCategory.ValueMember = "Category Name";
            comboBoxCategory.SelectedIndex = -1;
        }

        private void comboBoxCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
           if(comboBoxCategory.SelectedIndex >= 0)
            {
            string categoryName = comboBoxCategory.Text;
            dataGridViewInventory.DataSource = productDAL.SelectProductsByCategory(categoryName);
            }
            else
            {
                dataGridViewInventory.DataSource = productDAL.SelectAllProducts();
            }
        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
           dataGridViewInventory.DataSource = productDAL.SelectAllProducts();
            comboBoxCategory.SelectedIndex = -1;
        }
    }
}