﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Software_Chasers
{
    public partial class IssueBooks : Form
    {
        public IssueBooks()
        {
            InitializeComponent();
        }

        private void IssueBooks_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = @"Data Source=DESKTOP-DT2FM9V\SQLEXPRESS;Initial Catalog=libraryDatabase;Integrated Security=True";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            con.Open();

            cmd = new SqlCommand("select bName from AddNewbook", con);
            SqlDataReader Sdr = cmd.ExecuteReader();
            /*  SqlConnection con = new SqlConnection();
                con.ConnectionString = @"Data Source=DESKTOP-DT2FM9V\SQLEXPRESS;Initial Catalog=libraryDatabase;Integrated Security=True";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;


                cmd.CommandText = "select * from AddNewbook";
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();*/

            while (Sdr.Read())
            {
                for (int i = 0; i < Sdr.FieldCount; i++)
                {
                    comboBoxBooks.Items.Add(Sdr.GetString(i));
                }
            }
            Sdr.Close();
            con.Close();
        }
        int count;
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtEnrollment.Text != "")
            {
                string eid = txtEnrollment.Text;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = @"Data Source=DESKTOP-DT2FM9V\SQLEXPRESS;Initial Catalog=libraryDatabase;Integrated Security=True";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;


                cmd.CommandText = "select * from NewStudent where enroll = '" + eid + "'";
                SqlDataAdapter DA = new SqlDataAdapter(cmd);
                DataSet DS = new DataSet();
                DA.Fill(DS);


                //------------------------------------------------------------------------------------------------
                //Code to count how many book has been issued on this enrollment number 
                cmd.CommandText = "select count(std_enroll) from IRBook where std_enroll = '" + eid + "' and book_return_data is null";
                SqlDataAdapter DA1 = new SqlDataAdapter(cmd);
                DataSet DS1 = new DataSet();
                DA.Fill(DS1);

                count = int.Parse(DS1.Tables[0].Rows[0][0].ToString());

                //------------------------------------------------------------------------------------------------

                if (DS.Tables[0].Rows.Count != 0)
                {
                    txtName.Text = DS.Tables[0].Rows[0][1].ToString();
                    txtDep.Text = DS.Tables[0].Rows[0][3].ToString();
                    txtSem.Text = DS.Tables[0].Rows[0][4].ToString();
                    txtContact.Text = DS.Tables[0].Rows[0][5].ToString();
                    txtEmail.Text = DS.Tables[0].Rows[0][6].ToString();

                }
                else
                {
                    txtName.Clear();
                    txtDep.Clear();
                    txtSem.Clear();
                    txtContact.Clear();
                    txtEmail.Clear();
                    MessageBox.Show("Invalid Enrollment No", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
        }

        private void btnIssueBook_Click(object sender, EventArgs e)
        {
            if (txtName.Text != "")
            {
                if (comboBoxBooks.SelectedIndex != -1 && count <= 2)
                {
                    string enroll = txtEnrollment.Text;
                    string sname = txtName.Text;
                    string sdep = txtDep.Text;
                    string sem = txtSem.Text;
                    Int64 contact = Int64.Parse(txtContact.Text);
                    string email = txtEmail.Text;
                    string bookname = comboBoxBooks.Text;
                    string bookIssueDate = DateTimePicker.Text;


                    string eid = txtEnrollment.Text;
                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = @"Data Source=DESKTOP-DT2FM9V\SQLEXPRESS;Initial Catalog=libraryDatabase;Integrated Security=True";
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    con.Open();

                    cmd.CommandText = "insert into IRBook (std_enroll,std_name,std_dep,std_sem,std_contact,std_email,book_name,book_issue_date) values ('" + enroll + "', '" + sname + "', '" + sdep + "','" + sem + "','" + contact + "','" + email + "','" + bookname + "','" + bookIssueDate + "')";
                     cmd.ExecuteNonQuery();
                    con.Close();

                    MessageBox.Show("Book Issued.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Select Book. OR Maximum number of Book Has been ISSUED", "No Book Selected", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Enter Valid Enrollment No", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtEnrollment_TextChanged(object sender, EventArgs e)
        {
            if ( txtEnrollment.Text == "")
            {
                txtName.Clear();
                txtDep.Clear();
                txtSem.Clear();
                txtContact.Clear();
                txtEmail.Clear();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtEnrollment.Clear();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You Sure?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                this.Close();
            }
        }
    }
}