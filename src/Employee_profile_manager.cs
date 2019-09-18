using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.IO;

namespace Test3
{
    public partial class Employee_profile_manager2 : Form
    {
        private string emp_id;
        public Employee_profile_manager2()
        {
            InitializeComponent();
            checkBox1.Hide();
            label6.Hide();
            label11.Hide();

            emp_id = Manager.emp_id;

            Connection con = new Connection();
            MySqlConnection conn = con.start();
            conn.Open();
            string query4 = "SELECT * FROM employee WHERE Employee_id='"+emp_id+"'";
            MySqlCommand command1 = new MySqlCommand(query4, conn);

            MySqlDataReader dr = (command1.ExecuteReader());
            dr.Read();
            textBox1.Text = dr["Name"].ToString();
            textBox2.Text = dr["email"].ToString();
            textBox3.Text = dr["Phone_number"].ToString();
            textBox4.Text = dr["Role"].ToString();
            dr.Close();
            textBox1.Hide();
            textBox2.Hide();
            textBox3.Hide();
            textBox4.Hide();
            button1.Hide();
        }

        private void Employee_View_Load(object sender, EventArgs e)
        {
            try
            {
                Connection con = new Connection();
                MySqlConnection conn = con.start();
                conn.Open();
                string query3 = "SELECT facesample FROM faces WHERE Employee_id='" + emp_id + "'";
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(new MySqlCommand(query3, conn));
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet, "Image");
                if (dataSet.Tables["Image"].Rows.Count > 0)
                {

                    MemoryStream ms = new MemoryStream((byte[])dataSet.Tables["Image"].Rows[0][0]);
                    ms.Position = 0;

                    pictureBox1.Image = Image.FromStream(ms);//>error        
                }

                string query4 = "SELECT * FROM employee WHERE Employee_id='" + emp_id + "'";
                string query2 = "SELECT COUNT(*) FROM faces WHERE  Employee_id='" + emp_id + "'";

                MySqlCommand cmd2 = new MySqlCommand(query2, conn);
                MySqlCommand cmd4 = new MySqlCommand(query4, conn);
                MySqlDataReader reader = cmd4.ExecuteReader();


                if (reader.HasRows)
                {
                    reader.Read();

                    label7.Text = reader["Name"].ToString();
                    label8.Text = reader["email"].ToString();
                    label9.Text = reader["Phone_number"].ToString();
                    label10.Text = reader["Role"].ToString();
                    reader.Close();
                    int r = Convert.ToInt32(cmd2.ExecuteScalar());
                    label11.Text = r.ToString();
                    conn.Close();
                }
                else
                {
                    MessageBox.Show("No User Id Found");
                }
            }
            catch(Exception ed)
            {
                MessageBox.Show(ed.Message);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                button1.Show();
                label7.Hide();
                label8.Hide();
                label9.Hide();
                label10.Hide();
                textBox1.Show();
                textBox2.Show();
                textBox3.Show();
                textBox4.Show();

            }
            if (checkBox1.Checked == false)
            {
                button1.Hide();
                label7.Show();
                label8.Show();
                label9.Show();
                label10.Show();
                textBox1.Hide();
                textBox2.Hide();
                textBox3.Hide();
                textBox4.Hide();

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("Are You Sure To Update?", "Confirm",
    MessageBoxButtons.YesNo, MessageBoxIcon.Question,
    MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes))
            {
                Connection con = new Connection();
                MySqlConnection conn = con.start();
                conn.Open();
                string query3 = "UPDATE employee SET Name ='" + textBox1.Text + "', email ='" + textBox2.Text + "',Phone_number ='" + textBox3.Text + "',Role ='" + textBox4.Text + "'  WHERE Employee_id='" + emp_id + "'";

                MySqlCommand cmd = new MySqlCommand(query3, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Update Successfull");
                this.Close();
            }


        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }
    }
}
