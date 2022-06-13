using System;
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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textUsername_MouseClick(object sender, MouseEventArgs e)
        {
            if (textUsername.Text == "Username")
            {
                textUsername.Clear();
            }
        }

        private void textPassword_MouseClick(object sender, MouseEventArgs e)
        {
            if (textPassword.Text == "Password")
            {
                textPassword.Clear();
                textPassword.PasswordChar = '*';
            }
        }

        private void pictureBoxYouTube_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("");
        }

        private void pictureBoxFacebook_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("");
        }

        private void pictureBoxInsta_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("");
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = @"Data Source=DESKTOP-DT2FM9V\SQLEXPRESS;Initial Catalog=libraryDatabase;Integrated Security=True";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;   

            cmd.CommandText = "SELECT * FROM LoginTable WHERE username = '"+textUsername.Text+"' and pass = '"+textPassword.Text+"'";
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

           if (ds.Tables[0].Rows.Count != 0)
            {
               this.Hide();
                DashBoard dsa = new DashBoard(); 
                dsa.Show();
            }
           else
            {
                MessageBox.Show("Wrong Username or Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        
        }

       
    }
}
