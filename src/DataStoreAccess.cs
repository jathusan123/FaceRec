using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace Test3
{
    class DataStoreAccess:IDataStoreAccess

    {
        //generate random user id based on date
        public int GenerateUserId(string emp_id)
        {
            if (existId(emp_id))
            {
                Connection con = new Connection();
                MySqlConnection conn = con.start();
                conn.Open();
                string query = "SELECT DISTINCT userId FROM faces WHERE Employee_Id = @username ";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.Add("@username", MySqlDbType.VarChar, 20);
                cmd.Parameters["@username"].Value = emp_id;
                MySqlDataReader myReader = cmd.ExecuteReader();
                // MessageBox.Show((myReader["userId"]).ToString());
                myReader.Read();
                return Convert.ToInt32(myReader["userId"]);
            }
            else
            {
                var date = DateTime.Now.ToString("MMddHHmmss");
                return Convert.ToInt32(date);
            }
        }
        public Boolean existId(string emp_id)
        {
            Connection con = new Connection();
            MySqlConnection conn = con.start();
            conn.Open();
            string query = "SELECT userId FROM faces WHERE Employee_Id = @username ";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.Add("@username", MySqlDbType.VarChar, 20);
            cmd.Parameters["@username"].Value = emp_id;
            if (!cmd.ExecuteReader().HasRows)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        // actual face saving happen here
        public void SaveFace(string emp_id, byte[] faceBlob)
        {
            Connection con = new Connection();
            MySqlConnection conn= con.start();
            conn.Open();
            int userId = GenerateUserId(emp_id);
           // MessageBox.Show(userId.ToString());
            string query = "INSERT INTO faces (Employee_Id,facesample,userId) VALUES(@username,@faceblob,@userId)";
            MySqlCommand cmd = new MySqlCommand(query, conn);
           // MySqlCommand cmd = new MySqlCommand(query1, connection);
            cmd.Parameters.Add("@username", MySqlDbType.VarChar,20);
            cmd.Parameters.Add("@faceblob", MySqlDbType.LongBlob);
            cmd.Parameters.Add("@userId", MySqlDbType.Int32,10);
            cmd.Parameters["@username"].Value = emp_id;
            cmd.Parameters["@faceblob"].Value = faceBlob;
            cmd.Parameters["@userId"].Value = userId;
            //Execute command
            if (cmd.ExecuteNonQuery() == 1)
            {
                System.Windows.Forms.MessageBox.Show("success");
                conn.Close();
            }
           
           
        }
        public void insert(string u, int user)
        {
            Connection con = new Connection();
            MySqlConnection conn = con.start();
            conn.Open();
            int userId = GenerateUserId(u);
            string query1 = "INSERT INTO faces (username,userId) VALUES(" + u + "," + user + ")";

            MySqlCommand cmd = new MySqlCommand(query1, conn);

            //Execute command
            cmd.ExecuteNonQuery();

            conn.Close();
        }
        // inserting new employee
        public void insertuser(string name, int phno, string email, string emp_id,string role)
        {
            Connection con = new Connection();
            MySqlConnection conn = con.start();
            conn.Open();
          
            string query1 = "INSERT INTO employee (Name,email,Employee_id,Phone_number,Role) VALUES(@name,@email,@emp_id,@phno,@role)";

            MySqlCommand cmd = new MySqlCommand(query1, conn);
            cmd.Parameters.Add("@name", MySqlDbType.VarChar, 20);
            cmd.Parameters.Add("@email", MySqlDbType.VarChar, 40);
            cmd.Parameters.Add("@emp_id", MySqlDbType.VarChar, 20);
            cmd.Parameters.Add("@phno", MySqlDbType.Int32);
            cmd.Parameters.Add("@role", MySqlDbType.VarChar, 20);
            cmd.Parameters["@name"].Value = name;
            cmd.Parameters["@email"].Value =email;
            cmd.Parameters["@emp_id"].Value = emp_id;
            cmd.Parameters["@phno"].Value =phno;
            cmd.Parameters["@role"].Value =role;
            //Execute command
            cmd.ExecuteNonQuery();
            
            conn.Close();
        }

        public List<Face> callfaces()
        {

            List<Face> faces = new List<Face>();
            Connection con = new Connection();
            MySqlConnection conn = con.start();
            conn.Open();
            string query = "SELECT * FROM faces ";
            MySqlCommand cmd = new MySqlCommand(query, conn);
          
            MySqlDataReader myReader = cmd.ExecuteReader();
            while (myReader.Read())
            {
                var face = new Face
                {
                    Image = (byte[])myReader["faceSample"],
                    Id = Convert.ToInt32(myReader["id"]),
                    Label = (String)myReader["Employee_Id"],
                    UserId = Convert.ToInt32(myReader["userId"])
                };
                faces.Add(face);
               // faces = faces.OrderBy(f => f.Id).ToList();
         
                //face1.Image.SetValue(myReader["facesample"]);

            }
            return faces;

        }
        public string getemp_id(int user_id)
        {
            Connection con = new Connection();
            MySqlConnection conn = con.start();
            conn.Open();
            string query = "SELECT Employee_Id FROM faces WHERE userId = @user_id ";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.Add("@username", MySqlDbType.Int32, 20);
            cmd.Parameters["@user_id"].Value = user_id;
            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                return reader["Employee_Id"].ToString();
            }
            else
            {
                return "No User Id Found";
            }
        }
        //getting the appropiate name fora emp_id
        public string get_name(string emp_id)
        {
            Connection con = new Connection();
            MySqlConnection conn = con.start();
            conn.Open();
            string query = "SELECT Name FROM employee WHERE Employee_id = @user_id ";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.Add("@user_id", MySqlDbType.VarChar, 20);
            cmd.Parameters["@user_id"].Value =emp_id;
            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                return reader["Name"].ToString();
            }
            else
            {
                return "No User Id Found";
            }
        }
        public void insertstaff(string name, int phno, string email, string emp_id, string role,string password)
        {
            Connection con = new Connection();
            MySqlConnection conn = con.start();
            conn.Open();

            string query1 = "INSERT INTO employee (Name,email,Employee_id,Phone_number,Role) VALUES(@name,@email,@emp_id,@phno,@role)";
            string query2 = "INSERT INTO login (email,password,role) VALUES(@email,@password,@role)";
            MySqlCommand cmd2 = new MySqlCommand(query2, conn);
            MySqlCommand cmd = new MySqlCommand(query1, conn);
            cmd.Parameters.Add("@name", MySqlDbType.VarChar, 20);
            cmd.Parameters.Add("@email", MySqlDbType.VarChar, 40);
            cmd.Parameters.Add("@emp_id", MySqlDbType.VarChar, 20);
            cmd.Parameters.Add("@phno", MySqlDbType.Int32);
            cmd.Parameters.Add("@role", MySqlDbType.VarChar, 20);
            cmd.Parameters["@name"].Value = name;
            cmd.Parameters["@email"].Value = email;
            cmd.Parameters["@emp_id"].Value = emp_id;
            cmd.Parameters["@phno"].Value = phno;
            cmd.Parameters["@role"].Value = role;
           
            cmd2.Parameters.Add("@email", MySqlDbType.VarChar, 40);
            cmd2.Parameters.Add("@password", MySqlDbType.VarChar, 50);

            cmd2.Parameters.Add("@role", MySqlDbType.VarChar, 20);
            cmd2.Parameters["@password"].Value = password;
            cmd2.Parameters["@email"].Value = email;
           
            cmd2.Parameters["@role"].Value = role;
            //Execute command
            cmd2.ExecuteNonQuery();
            cmd.ExecuteNonQuery();
            MessageBox.Show("Registration SuccessFull");
        

            conn.Close();
           
        }

        public string get_pass(string user_id)
        {
            Connection con = new Connection();
            MySqlConnection conn = con.start();
            conn.Open();
            string query = "SELECT password FROM login WHERE email = @user_id ";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.Add("@user_id", MySqlDbType.VarChar, 40);
            cmd.Parameters["@user_id"].Value = user_id;
            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                return reader["password"].ToString();
            }
            else
            {
                MessageBox.Show("NO User Found");
                return null;
            }
        }
        public string get_emp(string user_id)
        {
            Connection con = new Connection();
            MySqlConnection conn = con.start();
            conn.Open();
            string query = "SELECT Employee_id FROM employee WHERE email = @user_id ";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.Add("@user_id", MySqlDbType.VarChar, 40);
            cmd.Parameters["@user_id"].Value = user_id;
            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                return reader["Employee_id"].ToString();
            }
            else
            {
               
                return null;
            }
        }
        public string get_name3(string emp_id)
        {
            Connection con = new Connection();
            MySqlConnection conn = con.start();
            conn.Open();
            string query = "SELECT Name FROM employee WHERE Employee_id = @user_id ";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.Add("@user_id", MySqlDbType.VarChar, 20);
            cmd.Parameters["@user_id"].Value =  emp_id;
            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                return reader["Name"].ToString();
            }
            else
            {
                return "No User Id Found";
            }
        }
        public string get_role(string email)
        {
            Connection con = new Connection();
            MySqlConnection conn = con.start();
            conn.Open();
            string query = "SELECT Role FROM employee WHERE email = @user_id ";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.Add("@user_id", MySqlDbType.VarChar, 40);
            cmd.Parameters["@user_id"].Value = email;
            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                return reader["Role"].ToString();
            }
            else
            {
                return "No Role Found";
            }
        }
    }
}
