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
    public partial class Settings_manager : Form
    {
        public Settings_manager()
        {
            InitializeComponent();
            dateTimePicker1.Hide();
            dateTimePicker2.Hide();
            dateTimePicker3.Hide();
            dateTimePicker4.Hide();
            dateTimePicker5.Hide();
            dateTimePicker6.Hide();
            dateTimePicker7.Hide();
            dateTimePicker8.Hide();
            dateTimePicker9.Hide();
            dateTimePicker10.Hide();

            dateTimePicker1.Format= DateTimePickerFormat.Time;
            dateTimePicker1.ShowUpDown = true;
            dateTimePicker2.Format = DateTimePickerFormat.Time;
            dateTimePicker2.ShowUpDown = true;
            dateTimePicker3.Format = DateTimePickerFormat.Time;
            dateTimePicker3.ShowUpDown = true;
            dateTimePicker4.Format = DateTimePickerFormat.Time;
            dateTimePicker4.ShowUpDown = true;
            dateTimePicker5.Format = DateTimePickerFormat.Time;
            dateTimePicker5.ShowUpDown = true;
            dateTimePicker6.Format = DateTimePickerFormat.Time;
            dateTimePicker6.ShowUpDown = true;
            dateTimePicker7.Format = DateTimePickerFormat.Time;
            dateTimePicker7.ShowUpDown = true;
            dateTimePicker8.Format = DateTimePickerFormat.Time;
            dateTimePicker8.ShowUpDown = true;
            dateTimePicker9.Format = DateTimePickerFormat.Time;
            dateTimePicker9.ShowUpDown = true;
            dateTimePicker10.Format = DateTimePickerFormat.Time;
            dateTimePicker10.ShowUpDown = true;

            Connection con = new Connection();
            MySqlConnection conn = con.start();
            conn.Open();
            string query1 = "SELECT * FROM time_prefer WHERE day = 'Mon' ";
            string query2 = "SELECT * FROM time_prefer WHERE day = 'Tue' ";
            string query3 = "SELECT * FROM time_prefer WHERE day = 'Wed' ";
            string query4 = "SELECT * FROM time_prefer WHERE day = 'Thu' ";
            string query5 = "SELECT * FROM time_prefer WHERE day = 'Fri' ";
            MySqlCommand cmd1 = new MySqlCommand(query1, conn);
            MySqlCommand cmd2 = new MySqlCommand(query2, conn);
            MySqlCommand cmd3 = new MySqlCommand(query3, conn);
            MySqlCommand cmd4 = new MySqlCommand(query4, conn);
            MySqlCommand cmd5 = new MySqlCommand(query5, conn);
            MySqlDataReader m1 = cmd1.ExecuteReader();
            m1.Read();
            label6.Text = m1["time_threshold"].ToString();
            label11.Text = m1["end_time"].ToString();
            m1.Close();
            MySqlDataReader m2 = cmd2.ExecuteReader();
            m2.Read();
            label7.Text = m2["time_threshold"].ToString();
            label12.Text = m2["end_time"].ToString();
            m2.Close();
            MySqlDataReader m3 = cmd3.ExecuteReader();
            m3.Read();
            label8.Text = m3["time_threshold"].ToString();
            label13.Text = m3["end_time"].ToString();
            m3.Close();
            MySqlDataReader m4 = cmd4.ExecuteReader();
            m4.Read();
            label9.Text = m4["time_threshold"].ToString();
            label14.Text = m4["end_time"].ToString();
            m4.Close();
            MySqlDataReader m5 = cmd5.ExecuteReader();
            m5.Read();
            label10.Text = m5["time_threshold"].ToString();
            label15.Text = m5["end_time"].ToString();
            m5.Close();

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
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
                if (checkBox1.Checked == true)
                {

                    string d1 = DateTime.Parse(dateTimePicker1.Value.ToString()).ToString("HH:mm");
                    string d2 = DateTime.Parse(dateTimePicker6.Value.ToString()).ToString("HH:mm");


                    string query3 = "UPDATE time_prefer SET time_threshold ='" + d1 + "', end_time ='" + d2 + "'  WHERE day='Mon'";

                    MySqlCommand cmd = new MySqlCommand(query3, conn);
                    cmd.ExecuteNonQuery();



                }
                if (checkBox2.Checked == true)
                {

                    string d1 = DateTime.Parse(dateTimePicker2.Value.ToString()).ToString("HH:mm");
                    string d2 = DateTime.Parse(dateTimePicker7.Value.ToString()).ToString("HH:mm");


                    string query3 = "UPDATE time_prefer SET time_threshold ='" + d1 + "', end_time ='" + d2 + "'  WHERE day='Tue'";

                    MySqlCommand cmd = new MySqlCommand(query3, conn);
                    cmd.ExecuteNonQuery();


                }
                if (checkBox3.Checked == true)
                {

                    string d1 = DateTime.Parse(dateTimePicker3.Value.ToString()).ToString("HH:mm");
                    string d2 = DateTime.Parse(dateTimePicker8.Value.ToString()).ToString("HH:mm");


                    string query3 = "UPDATE time_prefer SET time_threshold ='" + d1 + "', end_time ='" + d2 + "'  WHERE day='Wed'";

                    MySqlCommand cmd = new MySqlCommand(query3, conn);
                    cmd.ExecuteNonQuery();



                }
                if (checkBox4.Checked == true)
                {

                    string d1 = DateTime.Parse(dateTimePicker4.Value.ToString()).ToString("HH:mm");
                    string d2 = DateTime.Parse(dateTimePicker9.Value.ToString()).ToString("HH:mm");


                    string query3 = "UPDATE time_prefer SET time_threshold ='" + d1 + "', end_time ='" + d2 + "'  WHERE day='Thu'";

                    MySqlCommand cmd = new MySqlCommand(query3, conn);
                    cmd.ExecuteNonQuery();



                }
                if (checkBox5.Checked == true)
                {

                    string d1 = DateTime.Parse(dateTimePicker5.Value.ToString()).ToString("HH:mm");
                    string d2 = DateTime.Parse(dateTimePicker10.Value.ToString()).ToString("HH:mm");


                    string query3 = "UPDATE time_prefer SET time_threshold ='" + d1 + "', end_time ='" + d2 + "'  WHERE day='Fri'";

                    MySqlCommand cmd = new MySqlCommand(query3, conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Upadate Sucessfull");


                }
                else
                {
                    MessageBox.Show("Nothing Selected");
                }
            }
         
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                label6.Hide();
                label11.Hide();
                dateTimePicker1.Show();
                dateTimePicker6.Show();
            }
            if (checkBox1.Checked == false)
            {
                dateTimePicker1.Hide();
                dateTimePicker6.Hide();
                label6.Show();
                label11.Show();
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                label7.Hide();
                label12.Hide();
                dateTimePicker2.Show();
                dateTimePicker7.Show();
            }
            if (checkBox2.Checked == false)
            {
                dateTimePicker2.Hide();
                dateTimePicker7.Hide();
                label7.Show();
                label12.Show();
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true)
            {
                label8.Hide();
                label13.Hide();
                dateTimePicker3.Show();
                dateTimePicker8.Show();
            }
            if (checkBox3.Checked == false)
            {
                dateTimePicker3.Hide();
                dateTimePicker8.Hide();
                label8.Show();
                label13.Show();
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked == true)
            {
                label9.Hide();
                label14.Hide();
                dateTimePicker4.Show();
                dateTimePicker9.Show();
            }
            if (checkBox4.Checked == false)
            {
                dateTimePicker4.Hide();
                dateTimePicker9.Hide();
                label9.Show();
                label14.Show();
            }
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {

            if (checkBox5.Checked == true)
            {
                label10.Hide();
                label15.Hide();
                dateTimePicker5.Show();
                dateTimePicker10.Show();
            }
            if (checkBox5.Checked == false)
            {
                dateTimePicker5.Hide();
                dateTimePicker10.Hide();
                label10.Show();
                label15.Show();
            }
        }
    }
}
