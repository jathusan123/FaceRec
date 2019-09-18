using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test3
{
   public class Employee
    {
        private string name;
        private string emp_id;
        private string date_birth;
        private string position;
        private Face image;

        public Employee (string name, string emp_id, string date_birth, string position)
        {
            this.date_birth = date_birth;
            this.name = name;
            this.position = position;
            this.emp_id = emp_id;

        }
        public void saveemployee()
        {

        }


    }
    
}
