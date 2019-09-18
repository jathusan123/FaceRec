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
using System.Collections;
namespace Test3
{
    public partial class Notification_manager : Form
    {
        private ArrayList message;
        public static string emp_id;
        public Notification_manager()
        {
            InitializeComponent();
            panel2.Hide();
            panel3.Hide();
            panel4.Hide();
            panel5.Hide();
            panel6.Hide();

            message = new ArrayList();
        
            Connection con = new Connection();
            MySqlConnection conn = con.start();
            conn.Open();
            string query = "SELECT Employee_Id FROM warnings";
            MySqlCommand cmf = new MySqlCommand(query, conn);
            MySqlDataReader re = cmf.ExecuteReader();
        //  MessageBox.Show(re["Employee_Id"].ToString());
            while (re.Read())
            {
                message.Add(re["Employee_Id"].ToString());
            }
            if (0 < message.Count) {
                panel2.Show();
                label2.Text = "Employee On ID" + message[0].ToString() + " is late continiously three times";

            }
            if (1<message.Count)
            {
                panel2.Show();
                label2.Text = "Employee On ID" + message[1].ToString() + " is late continiously three times";

            }
            if (2 < message.Count)
            {
                panel2.Show();
                label2.Text = "Employee On ID" + message[2].ToString() + " is late continiously three times";

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            emp_id = message[0].ToString();
            Employee_profile_manager em = new Employee_profile_manager();
            em.Show();
        }
    }
}
