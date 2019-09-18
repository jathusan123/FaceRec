using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test3
{
    public partial class manager_reg : Form
    {
        public  string name1;
        public string emp_id;
        public  int phn_no;
        public string p1;
        public  string p2;
        public string email1;

        public manager_reg()
        {
            InitializeComponent();


        }

        private void staff_reg_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataStoreAccess d = new DataStoreAccess();
            if (textBox1.Text == "" || textBox2.Text == "" || textBox6.Text=="" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "")
            {
                MessageBox.Show("Enter All Fields");

            }
            else
            {
                if (d.existId(textBox5.Text))
                {
                    MessageBox.Show("User Already Exists");

                }
                else
                {
                    if (textBox4.Text != textBox5.Text)
                    {
                        MessageBox.Show("Passwords Do Not Match");
                    }
                    else
                    {
                        name1 = textBox1.Text;
                        email1 = textBox2.Text;
                        emp_id = textBox6.Text;
                        p1 = encrypt.Encrypt( textBox4.Text);
                        
                        phn_no = Convert.ToInt32(textBox3.Text);

                       string role1 = "manager";
                        d.insertstaff(name1, phn_no, email1, emp_id, role1, p1);
                        this.Hide();
                        Login j = new Login();
                        j.Show();
                    }
                    
                }
            }
        }
    }
}
