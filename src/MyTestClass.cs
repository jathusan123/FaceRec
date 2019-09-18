using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Test3
{
    [TestFixture]
    class MyTestClass
    {
        [TestCase]
        public void get_name()
        {
            DataStoreAccess ds = new DataStoreAccess();

            Assert.AreEqual("Jathusan", ds.get_name("1"));
            
            
        }
        [TestCase]
        public void get2()
        {
            DataStoreAccess ds = new DataStoreAccess();

         
            Assert.AreEqual("nan", ds.get_name("666"));

       
        }
        [TestCase]
        public void get3()
        {
            DataStoreAccess ds = new DataStoreAccess();

          
            Assert.AreEqual("staff", ds.get_role("jan@yahoo.com"));

            //   public string getemp_id(int user_id)
        }
        [TestCase]
        public void attendance()
        {
            attendance a = new attendance("1");

            Assert.AreEqual(true, a.checktime());
        }
        [TestCase]
        public void attendance2()
        {
            attendance a = new attendance("1");

            Assert.AreEqual(false, a.exist_attendance("1"));
        }
        [TestCase]
        public void get_name2()
        {
            DataStoreAccess ds = new DataStoreAccess();

            Assert.AreEqual("Jathusan", ds.get_name("1"));
          ;

        }

        [TestCase]
        public void get4()
        {
            DataStoreAccess ds = new DataStoreAccess();


            Assert.AreEqual("manager", ds.get_role("t@gmail.com"));

            //   public string getemp_id(int user_id)
        }
        [TestCase]
        public void attendance3()
        {
            attendance a = new attendance("6");

            Assert.AreEqual(true, a.checktime());
        }
        [TestCase]
        public void attendance6()
        {
            attendance a = new attendance("666");

            Assert.AreEqual(false, a.exist_attendance("666"));
        }
        public void newlogin()
        {
            Login l = new Test3.Login();
            l.Show();
        }
        [TestCase]
        public void formtest()
        {
            newlogin();

        }
      

    }
}
