using Digitalkirana.BusinessLogicLayer;
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
    public partial class User : Form
    {
        public User()
        {
            InitializeComponent();
        }

        UserBLL user = new UserBLL();
        UserDAL userDAL = new UserDAL();

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (textBoxFullName.Text != ""  && textBoxPhone.Text != "" && textBoxUsername.Text != "" && comboBoxGender.Text != "" && comboBoxUserType.Text != "" && textBoxAddress.Text != "")
            {
                if(btnSave.Text == "Add" && textBoxPassword.Text =="")
                {
                    MessageBox.Show("Some field is missing", "Missing", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }

                user.FullName = textBoxFullName.Text;
                user.UserName = textBoxUsername.Text;
                user.Password = textBoxPassword.Text;
                user.Phone = textBoxPhone.Text;
                user.Gender = comboBoxGender.SelectedItem.ToString();
                user.Address = textBoxAddress.Text;
                user.UserType = comboBoxUserType.SelectedItem.ToString();
                user.AddedDate = DateTime.Now;
                user.AddedBy = userDAL.getUserId(Login.username);
                if (checkBoxActive.Checked)
                {
                    user.Active = true;
                }
                else
                {
                    user.Active = false;
                }
                if (user.Id > 0)
                {
                    userDAL.UpdateUser(user);
                }
                else
                {
                    var checkDuplicateUsername = userDAL.CheckDuplicateUsername(textBoxUsername.Text);
                    if (checkDuplicateUsername)
                    {
                        MessageBox.Show("Username already exists. Please enter unique username.","Alert",MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        return;
                    }
                    userDAL.InsertUser(user);
                }
                dataGridViewUser.DataSource = userDAL.SelectAllUsers();
                reset();
            }
            else
            {
                MessageBox.Show("Some field is missing", "Missing", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void reset()
        {
            user.Id = 0;
            textBoxFullName.Clear();
            textBoxUsername.Clear();
            textBoxPassword.Clear();
            textBoxPhone.Clear();
            comboBoxGender.SelectedIndex = -1;
            textBoxAddress.Clear();
            comboBoxUserType.SelectedIndex = -1;
            checkBoxActive.Checked = false;
            btnSave.Text = "Add";
            btnResetPassword.Enabled = false;
            textBoxPassword.Enabled = true;
        }

        private void User_Load(object sender, EventArgs e)
        {
            dataGridViewUser.DataSource = userDAL.SelectAllUsers();
        }

        private void dataGridViewUser_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            if (rowIndex < 0)
            {
                return;
            }
            DataGridViewRow selectedRow = dataGridViewUser.Rows[rowIndex];
            btnSave.Text = "Update";
            user.Id = Convert.ToInt32(selectedRow.Cells[0].Value);
            textBoxFullName.Text = selectedRow.Cells[1].Value.ToString();
            textBoxUsername.Text = selectedRow.Cells[2].Value.ToString();
            textBoxPhone.Text = selectedRow.Cells[3].Value.ToString();
            textBoxAddress.Text = selectedRow.Cells[4].Value.ToString();
            comboBoxGender.Text = selectedRow.Cells[5].Value.ToString();
            comboBoxUserType.Text = selectedRow.Cells[6].Value.ToString();
            checkBoxActive.Checked = Convert.ToBoolean(selectedRow.Cells[8].Value);
            btnResetPassword.Enabled = true;
            textBoxPassword.Enabled = false;
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            string keyword = textBoxSearch.Text;
            if(keyword == null)
            {
                dataGridViewUser.DataSource = userDAL.SelectAllUsers();
            }
            else
            {
                dataGridViewUser.DataSource = userDAL.SearchUser(keyword);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Do you really want to reset?", "Reset", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.Cancel)
            {
                return;
            }
            reset();
        }

        private void btnResetPassword_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Do you really want to reset password?", "Reset Password", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.Cancel)
            {
                return;
            }
            ResetPassword resetPassword = new ResetPassword(user.Id);
            resetPassword.ShowDialog();
        }
    }
}