using Customer_salun_.Data;
using Customer_salun_.Data.Models;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Customer_salun_.Forms
{
    public partial class CustomerForm : Form
    {
        int score = 0;
        BindingSource bsCustomer;
        public CustomerForm()
        {
            InitializeComponent();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            score = 1;
            btnSave.Enabled = true;
            if (dtAddress != null)
                dtAddress.Rows.Clear();
            btnSave.BackColor = Color.FromArgb(32, 85, 131);
            Clear();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!CheckValidate())
            {
                return;
            }
            Customer cus = new Customer();
            cus.CustomerName = txtCustomerName.Text.Trim();
            cus.Sex = SetGender();
            cus.DoB = dtpDoB.Value;
            cus.PoB = txtPoB.Text.Trim();
            cus.Phone = txtPhone.Text.Trim();
            cus.Email = txtEmail.Text.Trim();
            if (score == 1)
            {
                try
                {

                    int id = Customers.Add(cus);
                    if (id > 0)
                    {
                        foreach (DataRow row in dtAddress.Rows)
                        {
                            Address addr = new Address();
                            addr.AddressName = row["AddressName"].ToString();
                            addr.CustomerId = id;
                            Addresses.Add(addr);
                        }
                    }
                    MessageBox.Show("Record is saving!.");

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Exception is " + ex.Message);
                }
            }
            else if (score == 2)
            {
                int id = int.Parse(dgCustomer.SelectedRows[0].Cells[0].Value.ToString());
                if (id > 0)
                {
                    try
                    {
                        cus.CustomerId = id;
                        Customers.Update(cus);
                        Addresses.Delete(id);
                        foreach (DataRow row in dtAddress.Rows)
                        {
                            Address addr = new Address();
                            addr.AddressName = row["AddressName"].ToString();
                            addr.CustomerId = id;
                            Addresses.Add(addr);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                MessageBox.Show("Record is Updating!.");
            }
            else
            {
                MessageBox.Show("the system is erroring!.");
                return;
            }



            CustomerForm_Load(null, null);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id = int.Parse(dgCustomer.SelectedRows[0].Cells[0].Value.ToString());
            Customers.Delete(id);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            score = 2;
            btnSave.Enabled = true;
            btnSave.BackColor = Color.FromArgb(32, 85, 131);
            Clear();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            LoadData();
            this.Hide();
        }

        private void CustomerForm_Load(object sender, EventArgs e)
        {
            btnSave.BackColor = Color.FromArgb(166, 187, 205);
            btnSave.Enabled = false;
            EnableControl(false);

            LoadData();
        }

        // Other Method
        void GetGender(char gender)
        {
            if (gender == 'F')
            {
                rdFemale.Checked = true;
            }
            else
            {
                rdMale.Checked = true;
            }
        }

        char SetGender()
        {
            char gender;
            if (rdFemale.Checked)
            {
                gender = 'F';
            }
            else
            {
                gender = 'M';
            }
            return gender;
        }

        public void LoadData()
        {
            DataTable dt = new DataTable();
            dt = Customers.GetAll();
            bsCustomer = new BindingSource();

            dgCustomer.DataSource = dt;
            bsCustomer.DataSource = dt;


            Setup();
        }
        void Setup()
        {
            dgCustomer.ReadOnly = true;
            dgCustomer.RowHeadersVisible = false;
            dgCustomer.Columns[0].DataPropertyName = "CustomerID";
            dgCustomer.Columns[0].Visible = false;
            dgCustomer.Columns[1].DataPropertyName = "CustomerName";
            dgCustomer.Columns[1].Visible = true;
            dgCustomer.Columns[1].HeaderText = "Customer Name";
            dgCustomer.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgCustomer.Columns[2].DataPropertyName = "Sex";
            dgCustomer.Columns[2].Visible = false;
            dgCustomer.Columns[3].DataPropertyName = "DOB";
            dgCustomer.Columns[3].Visible = false;
            dgCustomer.Columns[4].DataPropertyName = "POB";
            dgCustomer.Columns[4].Visible = false;
            dgCustomer.Columns[5].DataPropertyName = "Phone";
            dgCustomer.Columns[5].Visible = false;
            dgCustomer.Columns[6].DataPropertyName = "Email";
            dgCustomer.Columns[6].Visible = false;
            dgCustomer.Columns[7].DataPropertyName = "ishidden";
            dgCustomer.Columns[7].Visible = false;
        }

        void EnableControl(bool result)
        {
            txtCustomerName.Enabled = result;
            txtPoB.Enabled = result;
            txtPhone.Enabled = result;
            txtEmail.Enabled = result;
            dtpDoB.Enabled = result;
            rdFemale.Enabled = result;
            rdMale.Enabled = result;
            if (result == false)
            {
                epCustomerName.Clear();
                epPoB.Clear();
                epPhone.Clear();
                epGender.Clear();
                epDateOfBirth.Clear();
                dgAddress.ReadOnly = true;
            }
            else
            {
                dgAddress.ReadOnly = false;
            }

        }
        void Clear()
        {
            EnableControl(true);
            if (score == 1)
            {
                txtCustomerName.Clear();
                txtEmail.Clear();
                txtPhone.Clear();
                txtPoB.Clear();
                dtpDoB.Value = DateTime.Now.Date;
                rdMale.Checked = false;
                rdFemale.Checked = false;
            }
        }
        bool CheckValidate()
        {
            bool result = true;
            if (txtCustomerName.Text == "")
            {
                epCustomerName.SetError(txtCustomerName, "Pls enter customer name");
                result = false;
            }
            else
            {
                epCustomerName.Clear();
            }
            if (txtPhone.Text == "")
            {
                epPhone.SetError(txtPhone, "Pls enter Phone Number");
                result = false;
            }
            else
            {
                epPhone.Clear();
            }
            if (txtPoB.Text == "")
            {
                epPoB.SetError(txtPoB, "Pls enter place of birth");
                result = false;
            }
            else
            {
                epPoB.Clear();
            }
            if (dtpDoB.Value.Date == DateTime.Now.Date)
            {
                epDateOfBirth.SetError(dtpDoB, "Pls select date of birth");
                result = false;
            }
            else
            {
                epDateOfBirth.Clear();
            }
            return result;
        }
        DataTable dtAddress;
        private void dgCustomer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            EnableControl(false);
            btnSave.Enabled = false;

            int id = int.Parse(dgCustomer.SelectedRows[0].Cells[0].Value.ToString());
            Customer customer = Customers.Get(id);
            if (customer != null)
            {
                txtCustomerName.Text = customer.CustomerName;
                txtEmail.Text = customer.Email;
                txtPhone.Text = customer.Phone;
                txtPoB.Text = customer.PoB;
                GetGender(customer.Sex);
                dtpDoB.Value = customer.DoB;
            }

            dtAddress = new DataTable();
            dtAddress = Addresses.Get(id);
            dgAddress.DataSource = dtAddress;

            dgAddress.RowHeadersVisible = false;
            dgAddress.Columns[0].DataPropertyName = "AddressId";
            dgAddress.Columns[0].Visible = false;
            dgAddress.Columns[1].DataPropertyName = "CustomerID";
            dgAddress.Columns[1].Visible = false;
            dgAddress.Columns[2].DataPropertyName = "AddressName";
            dgAddress.Columns[2].Visible = true;
            dgAddress.Columns[2].HeaderText = "Address";
            dgAddress.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }
    }
}
