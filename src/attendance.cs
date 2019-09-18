using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Globalization;
using System.Windows.Forms;

namespace Test3
{
    public class attendance
    {
        private string emp_id;
        private DateTime now;
        private string state;

        public attendance(string emp_id) {
            this.emp_id = emp_id;
            now = DateTime.Now;


        }
        public void markattendance()
        {

            if (checktime())

            {
                if (!exist_attendance(emp_id))
                {
                    MessageBox.Show(generatewarning());
                    string date = DateTime.Today.ToString("yyyy/M/d");
                    string time = DateTime.Now.ToString("H:mm:ss");

                    string status = state;
                    Connection con = new Connection();
                    MySqlConnection conn = con.start();
                    conn.Open();

                    // MessageBox.Show(userId.ToString());
                    string query = "INSERT INTO attendance (employee_id,Date,time,status) VALUES(@empid,@date,@time,@status)";
                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    cmd.Parameters.Add("@empid", MySqlDbType.VarChar, 20);
                    cmd.Parameters.Add("@date", MySqlDbType.VarChar, 10);
                    cmd.Parameters.Add("@time", MySqlDbType.VarChar, 10);
                    cmd.Parameters.Add("@status", MySqlDbType.VarChar, 20);
                    cmd.Parameters["@empid"].Value = emp_id;
                    cmd.Parameters["@date"].Value = date;
                    cmd.Parameters["@time"].Value = time;
                    cmd.Parameters["@status"].Value = status;
                  

                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        check_warn();
                        MessageBox.Show("Attendance Success Full");

                    }
                }
                else
                {
                    MessageBox.Show("Already Marked For Today");
                }

            }
            else
            {
                MessageBox.Show(" Attendance Time Limit Reached You Cannot Mark");
            }
        }
        // generate warning if false
        public string generatewarning()
        {
            
            string timenow=DateTime.Now.ToString("H:mm:ss");
            var dateTime = DateTime.ParseExact(timenow, "H:mm:ss", null, System.Globalization.DateTimeStyles.None);
            string now =this.get_time();

            var dateTime1 = DateTime.ParseExact(now, "H:mm:ss", null, System.Globalization.DateTimeStyles.None);
         
            if (TimeSpan.Compare(dateTime1.TimeOfDay, dateTime.TimeOfDay) > 0)
            {
                state = "on time";
                return "You Are On Time Keep it Up ";
            }
            else
            {
                state = "Late";
                return "You Are" + (dateTime1.TimeOfDay-dateTime.TimeOfDay).ToString() + " Late";
             

            }
        }
        
        public string get_time()
        {
            Connection con = new Connection();
            MySqlConnection conn = con.start();
            conn.Open();
       
            string today = DateTime.Now.ToString("yyyy/M/d");
            int d = (int)System.DateTime.Now.DayOfWeek;
            string query = "SELECT time_threshold FROM time_prefer WHERE date_id='" + d + "'";
            MySqlCommand cmd = new MySqlCommand(query, conn);
           
         
            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {

                    string time =  reader["time_threshold"].ToString();
                    return time;

                }
                return DateTime.Now.ToString();
            }
            else
            {
                return DateTime.Now.ToString();
            }
       
        }
        public string get_timegap()
        {
            string timenow = DateTime.Now.ToString("H:mm:ss");
            var dateTime = DateTime.ParseExact(timenow, "H:mm:ss", null, System.Globalization.DateTimeStyles.None);
            string now = this.get_time();

            var dateTime1 = DateTime.ParseExact(now, "H:mm:ss", null, System.Globalization.DateTimeStyles.None);
            //   MessageBox.Show(TimeSpan.Compare(dateTime1.TimeOfDay, dateTime.TimeOfDay).ToString());
            return (dateTime1 - dateTime).ToString();
        }
        //check starting and finishing time
        public Boolean checktime()
        {
            string timenow = DateTime.Now.ToString("H:mm:ss");
            var dateTime = DateTime.ParseExact(timenow, "H:mm:ss", null, System.Globalization.DateTimeStyles.None);
            Connection con = new Connection();
            MySqlConnection conn = con.start();
            conn.Open();

            string today = DateTime.Now.ToString("yyyy/M/d");
        
            int dt = (int)System.DateTime.Now.DayOfWeek;
        
         
            string query = "SELECT end_time FROM time_prefer  WHERE date_id='" + dt + "'";
            MySqlCommand cmd = new MySqlCommand(query, conn);

       
            MySqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {

                    string time = reader["end_time"].ToString();
                    var dateTime1 = DateTime.ParseExact(time, "H:mm:ss", null, System.Globalization.DateTimeStyles.None);
                    if (TimeSpan.Compare(dateTime1.TimeOfDay, dateTime.TimeOfDay) > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
                return false;
                

            }
            else
            {
                MessageBox.Show("No EndTime Found");
                return false;
            }
        }
        // checking for existing attendance
        public Boolean exist_attendance(string emp_id)
        {
            Connection con = new Connection();
            MySqlConnection conn = con.start();
            conn.Open();

            string today = DateTime.Now.ToString("yyyy/M/d");

            string query = "SELECT time FROM attendance WHERE date=@today AND employee_id=@emp_id";
            MySqlCommand cmd = new MySqlCommand(query, conn);

            cmd.Parameters.Add("@today", MySqlDbType.Date);
            cmd.Parameters["@today"].Value = today;
            cmd.Parameters.Add("@emp_id", MySqlDbType.VarChar,20);
            cmd.Parameters["@emp_id"].Value = emp_id;
            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)

            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        public void check_warn()
        {
            Connection cone = new Connection();
            MySqlConnection conne = cone.start();
            conne.Open();
            DateTime now = DateTime.Now;
            DateTime firstDay = new DateTime(now.Year, now.Month, 1);
            string sdate = firstDay.ToString("yyyy-MM-dd");
            
            DateTime lastDay = now.AddMonths(1).AddDays(-1);

            string edate = lastDay.ToString("yyyy-MM-dd");
           // MessageBox.Show(edate);

            MySqlCommand cmd3 = new MySqlCommand("SELECT COUNT(*) FROM attendance WHERE  status='Late' AND  Date>='" + sdate + "' AND Date<='" + edate + "' AND employee_id='" + emp_id + "' ", conne);
           int late = Convert.ToInt32(cmd3.ExecuteScalar());
        
            if (late==3)
            {
            
                string query4 ="INSERT INTO warnings(Employee_id, date,time_late) VALUES('"+ emp_id+"', '"+now.ToString("yyyy-MM-dd")+"','1')";
                MySqlCommand cm = new MySqlCommand(query4, conne);
                cm.ExecuteNonQuery();
            }
        }
    }
    
}
