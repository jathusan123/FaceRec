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
    public partial class general_report : Form
    {
        private string sdate;
        private string edate; 
        public general_report()
        {
            InitializeComponent();
            sdate = ReportViewer.sdate.Replace(" ", String.Empty);
            edate = ReportViewer.edate.Replace(" ", String.Empty);
            //label9.Hide();
            //label10.Hide();
            //label11.Hide();
            //label17.Hide();

            label6.Hide();
            label7.Hide();
            label8.Hide();
            label16.Hide();
         
            label2.Hide();
            label3.Hide();
            label4.Hide();
            label5.Hide();
            dataGridView1.BorderStyle = BorderStyle.None; dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249); dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal; dataGridView1.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise; dataGridView1.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke; dataGridView1.BackgroundColor = Color.White; dataGridView1.EnableHeadersVisualStyles = false; dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None; dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72); dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

        }

        private void general_report_Load(object sender, EventArgs e)
        {
            if (ReportViewer.r1)
            {
                try
                {
                    Connection con = new Connection();
                    MySqlConnection conn = con.start();



                    // string dm = sdate;
                    MySqlCommand cmd = new MySqlCommand("SELECT * FROM attendance LEFT OUTER JOIN employee ON attendance.employee_id = employee.Employee_id WHERE Date>='" + sdate + "' AND Date<='" + edate + "' ", conn);
                    //cmd.Parameters.Add("@sdate", MySqlDbType.VarChar,20);
                    //cmd.Parameters["@sdate"].Value =sdate;
                    //cmd.Parameters.Add("@edate", MySqlDbType.VarChar, 20);
                    //cmd.Parameters["@edate"].Value = edate;
                    //  cmd.Parameters.Add(new MySqlParameter("@sdate", sdate));
                    MySqlDataAdapter m = new MySqlDataAdapter(cmd);



                    conn.Open();
                    DataSet d = new DataSet();
                    m.Fill(d, "attendance");
                    dataGridView1.DataSource = d.Tables["attendance"];


                    conn.Close();
                }
                catch (Exception eb)
                {
                    MessageBox.Show(eb.Message);
                }
            }
            if (ReportViewer.r2)
            {
                Connection con = new Connection();
                MySqlConnection conn = con.start();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM attendance WHERE Date>='" + sdate + "' AND Date<='" + edate + "' AND employee_id='"+ReportViewer.emp_id+"' ", conn);
             
           
                MySqlDataAdapter m = new MySqlDataAdapter(cmd);



                conn.Open();
                DataSet d = new DataSet();
                m.Fill(d, "attendance");
                dataGridView1.DataSource = d.Tables["attendance"];


                conn.Close();


            }
        }
       

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
          
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
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

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
        public void getstat()
        {
            try
            {
                Connection con = new Connection();
                MySqlConnection conn = con.start();
                conn.Open();
               MySqlCommand cmd1 = new MySqlCommand("SELECT COUNT(*) FROM attendance WHERE  Date>='" + sdate + "' AND Date<='" + edate + "' AND employee_id='" + ReportViewer.emp_id + "' ", conn);
                MySqlCommand cmd2 = new MySqlCommand("SELECT COUNT(*) FROM attendance WHERE status='on time'AND  Date>='" + sdate + "' AND Date<='" + edate + "' AND employee_id='" + ReportViewer.emp_id + "'  ", conn);
                MySqlCommand cmd3 = new MySqlCommand("SELECT COUNT(*) FROM attendance WHERE  status='Late' AND  Date>='" + sdate + "' AND Date<='" + edate + "' AND employee_id='" + ReportViewer.emp_id + "' ", conn);
                MySqlCommand cmd4 = new MySqlCommand("SELECT COUNT(*) FROM attendance WHERE  status='Absent' AND  Date>='" + sdate + "' AND Date<='" + edate + "' AND employee_id='" + ReportViewer.emp_id + "'  ", conn);
               int total = Convert.ToInt32(cmd1.ExecuteScalar());
                double ontime = Convert.ToDouble(cmd2.ExecuteScalar());
                double late = Convert.ToDouble(cmd3.ExecuteScalar());
                double absent = Convert.ToDouble(cmd4.ExecuteScalar());
                label12.Text = total.ToString()+" "+"Days";
                label13.Text = ontime.ToString() + " " + "Days";
                label14.Text = absent.ToString() + " " + "Days";
                label15.Text = late.ToString() + " " + "Days";
                double d= (((ontime + late) / total) * 100);
                double d1 = (100-d);
                double d2 = (((late) / total) * 100);
                label9.Text = d.ToString()+"%";
                label10.Text = d1.ToString() + "%";
                
                label11.Text = d2.ToString() + "%";
            }
            catch(Exception r)
            {
                MessageBox.Show(r.Message);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true && ReportViewer.r2)
            {

                label6.Show();
                label7.Show();
                label8.Show();
                label9.Show();
                label10.Show();
                label11.Show();
                label12.Show();
                label13.Show();
                label14.Show();
                label15.Show();
                label2.Show();
                label3.Show();
                label4.Show();
                label5.Show();
                this.getstat();
            }
            if((!checkBox1.Checked == true && ReportViewer.r2))
            {

                label9.Hide();
                label10.Hide();
                label11.Hide();
                label12.Hide();
                label13.Hide();
                label14.Hide();
                label15.Hide();

                label6.Hide();
                label7.Hide();
                label8.Hide();
             

                label2.Hide();
                label3.Hide();
                label4.Hide();
                label5.Hide();
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ReportViewer r = new ReportViewer();
            r.Show();
            this.Close();
        }
    }
}
