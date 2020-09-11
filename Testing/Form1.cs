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
using System.Text.RegularExpressions;



namespace Testing
{
    

    public partial class Form1 : Form
    {
       

        RE reg = new RE();

        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\USERS\SHS\DOCUMENTS\VISUAL STUDIO 2012\PROJECTS\DATABASES\TESTING.MDF;Integrated Security=True;Connect Timeout=30");

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)   // start of insert button
        {

            if (txtname.Text != "" && txtpass.Text != "")       //    start of filling empty Fields
            {


                if (reg.regexPhone(txtphone.Text.ToString()))   //  Start of RegEx
                {
                    string Password = Cryptography.encrypt(txtpass.Text.ToString());

                    conn.Open();

 //         SqlCommand cmd = new SqlCommand("insert into Testing(Name, Password, Phone) values('" + txtname.Text + "', '" + Password + "', '" + Int64.Parse(txtphone.Text) + "' )", conn);

                    SqlCommand cmd = new SqlCommand("sp_insert", conn);
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("Name", SqlDbType.NChar).Value = txtname.Text;
                        cmd.Parameters.Add("Password", SqlDbType.NChar).Value = Password;
                        cmd.Parameters.Add("Phone", SqlDbType.BigInt).Value = txtphone.Text;

                    }

                    cmd.ExecuteNonQuery();

                    conn.Close();

                    MessageBox.Show("YOUR PHONE NO. IS VALID\nALSO\nDATA INSERTED SUCCESSFULLY :-*", "You are DONE");

                }

                else
                {
                    MessageBox.Show("YOUR PHONE NO. IS INVALID", "You are NOT DONE");       //  END of RegEx
                }

            }

            else
            {
                MessageBox.Show("Please fill NAME & PASSWORD fields"); //    end of filling empty Fields
            }   
         




        }   // end of insert button

        private void txtpass_TextChanged(object sender, EventArgs e)
        {
            txtpass.PasswordChar = '*';
        }

        private void button3_Click(object sender, EventArgs e)
        {
            conn.Open();

            SqlCommand cmd = new SqlCommand("UPDATE Testing SET Name = '" + txtname.Text + "' , Password = '" + txtpass.Text + "', Phone = '" + txtphone.Text + "' WHERE Id = '" + txtid.Text + "' ", conn);
            cmd.ExecuteNonQuery();

            conn.Close();

            MessageBox.Show("DATA UPDATED SUCCESSFULLY :-*", "You are DONE");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            txtid.Text = "";
            txtname.Text = "";
            txtpass.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            conn.Open();

            SqlCommand cmd = new SqlCommand("DELETE from Testing WHERE Id = '" + txtid.Text + "' ", conn);
            cmd.ExecuteNonQuery();

            conn.Close();

            MessageBox.Show("DATA DELETED SUCCESSFULLY :-*", "You are DONE");
        }


        DataTable dt = new DataTable();
        
        private void Form1_Load(object sender, EventArgs e)
        {
            conn.Open();

            SqlCommand cmd = new SqlCommand("Select * from Testing", conn);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            dataGridView1.DataSource = dt;

            conn.Close();
            

        }
    }
}
