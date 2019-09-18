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

namespace Test3
{
    public partial class ReportViewer : Form
    {
        public static string sdate;
        public static string edate;
        public static bool r1;
        public static bool r2;
        public static string emp_id;
        public ReportViewer()
        {
            InitializeComponent();
            textBox1.Hide();
            label4.Hide();
            radioButton1.Checked = true;

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                r1 = true;
                r2 = false;
                if (dateTimePicker1.Value != null && dateTimePicker2.Value != null)
                {
                    sdate = dateTimePicker1.Value.ToString("yyyy - MM - dd");
                    edate = dateTimePicker2.Value.ToString("yyyy - MM - dd");

                    general_report g = new general_report();
                    g.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Select A Valid Start Date And End Date");
                }
            }
            if (radioButton2.Checked)
            {
                r2 = true;
                r1 = false;
                if (textBox1.Text != null)
                {
                    emp_id = textBox1.Text.ToString();
                    Connection con = new Connection();
                    MySqlConnection conn = con.start();
                    conn.Open();
                    string query = "SELECT Name FROM employee WHERE Employee_id = '"+emp_id+"' ";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataReader r = cmd.ExecuteReader();
                    if (r.HasRows)
                    {
                        if (dateTimePicker1.Value != null && dateTimePicker2.Value != null)
                        {
                            sdate = dateTimePicker1.Value.ToString("yyyy - MM - dd");
                            edate = dateTimePicker2.Value.ToString("yyyy - MM - dd");

                            general_report g = new general_report();
                            g.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Select A Valid Start Date And End Date");
                        }
                    }
                    else
                    {
                        MessageBox.Show("No User Found On That ID ");
                    }
                }
                else
                {
                    MessageBox.Show("Enter a Valid ID");
                }

            }

          
        }

        private void ReportViewer_Load(object sender, EventArgs e)
        {

        }
        public string getsdate()
        {
            return sdate;
        }
        public string getedate()
        {
            return edate;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            
                
              
                label4.Hide();
                textBox1.Hide();

            
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            
          
           
            label4.Show();
            textBox1.Show();
        }
    }
}
