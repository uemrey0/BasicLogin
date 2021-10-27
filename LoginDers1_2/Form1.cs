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

namespace LoginDers1_2
{
    public partial class LoginForm : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        String conString = "Data Source=BOSS;Initial Catalog=loginDers1;Integrated Security=True";
        public LoginForm()
        {
            InitializeComponent();
            Init_Data();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Init_Data()
        {
            if (Properties.Settings.Default.username != string.Empty)
            {
                if (Properties.Settings.Default.remember == true)
                {
                    textBox1.Text = Properties.Settings.Default.username;
                    checkBox1.Checked = true;
                }
                else
                {
                    textBox1.Text = Properties.Settings.Default.username;
                }
            }
        }
        private void Save_Data()
        {
            if (checkBox1.Checked)
            {
                Properties.Settings.Default.username = textBox1.Text.Trim();
                Properties.Settings.Default.remember = true;
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.username = "";
                Properties.Settings.Default.remember = false;
                Properties.Settings.Default.Save();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            
            string username = textBox1.Text;
            string password = textBox2.Text;
            if(username != "" && password != "")
            {
                con = new SqlConnection(conString);
                cmd = new SqlCommand();
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "SELECT * FROM userInfo WHERE username = '" + username + "' AND password = '" + password + "'";
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    MessageBox.Show("Giriş Başarılı");
                    Save_Data();
                }
                else
                {
                    MessageBox.Show("Kullanıcı adı veya şidre hatalı", "Uyarı!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Boş alan bırakmayınız", "Uyarı!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            SifremiUnuttum frm = new SifremiUnuttum();
            frm.Show();
        }
    }
}
