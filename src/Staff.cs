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
    public partial class Staff : Form
    {
        DataStoreAccess data = new DataStoreAccess();
        public static string emp_id;
        public Staff()
        {
            InitializeComponent();
            label1.Text = "Welcome" + " " +data.get_name3(Login.emp_id);
        }
        
        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            registration r = new registration();
            r.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Recognition r = new Recognition();
            r.Show();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            ReportViewer r = new ReportViewer();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Recognition r = new Recognition();
            r.Show();
        }

        private void Staff_Load(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (textBox1.Text=="")
            {
                MessageBox.Show("Enter A Valid ID");
            }
            else
            {
                emp_id = textBox1.Text;
                Employee_View view = new Employee_View();
                view.Show();

                
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            registration re = new registration();
            re.Show();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            ReportViewer rt = new ReportViewer();
            rt.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            staff_view st = new staff_view();
            st.Show();
        }
    }
}
