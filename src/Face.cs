using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.Structure;

namespace Test3
{
   internal class Face
    {
        public Byte[] Image { get; set; }
       public int UserId { get; set; }
        public String Label { get; set; }
        public int Id { get; set; }

         
    }
}
