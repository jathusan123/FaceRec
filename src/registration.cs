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
    public partial class registration : Form

    {
        public static string name1;
        public static string emp_id;
        public static int phn_no;
        public static string role1;
        public static string email1;

        public registration()
        {
            InitializeComponent();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void image_Click(object sender, EventArgs e)
        {
            DataStoreAccess d = new DataStoreAccess();
            if (textBox1.Text=="" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "") {
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
                    int Num;
                    bool isNum = int.TryParse(textBox3.Text.ToString(), out Num);
                    if (isNum )
                    {
                        name1 = textBox1.Text;
                        email1 = textBox2.Text;
                        emp_id = textBox5.Text;

                        phn_no = Convert.ToInt32(textBox3.Text);
                        role1 = textBox4.Text;
                        this.Hide();
                        Form1 image = new Form1();
                        image.Show();
                    }
                    else
                    {
                        MessageBox.Show("Enter Valid Phone Number");
                    }
                 
                }
                
            }
        }
        private string getname()
        {

            return textBox1.Text;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void role_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
