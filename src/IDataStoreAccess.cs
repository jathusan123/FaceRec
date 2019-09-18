using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test3
{
    interface IDataStoreAccess
    {
        void SaveFace(string emp_id, Byte[] faceBlob);
        int GenerateUserId(String emp_id);
        List<Face> callfaces();
        void insertuser(string name, int phno, string email, string emp_id, string role);
    }
    
}
