using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu;
using Emgu.CV.Structure;
using Emgu.CV;
using Emgu.CV.Face;
using System.IO;
using Emgu.CV.CvEnum;

namespace Test3
{
    public partial class Trainer : Form

    {
        public Trainee tr;
        public Trainer()
        {

            InitializeComponent();
            
        }

        private void train_Click(object sender, EventArgs e)
        {
            Trainee tr = new Trainee();
    
        }
      

        private void button1_Click(object sender, EventArgs e)
        {
            

            //attendance a = new attendance("001");
            //a.markattendance();
        }

        private void Trainer_Load(object sender, EventArgs e)
        {

        }
        // public bool TrainRecognizer()
        // {
        //    EigenFaceRecognizer _faceRecognizer = new EigenFaceRecognizer();
        //    DataStoreAccess _datastoreaccess = new DataStoreAccess();
        //    var allFaces = _datastoreaccess.callfaces();
        //    if (allFaces != null)
        //    {
        //        var faceImages = new Image<Gray, byte>[allFaces.Count];
        //        var faceLabels = new int[allFaces.Count];
        //        for (int i = 0; i < allFaces.Count; i++)
        //        {
        //            Stream stream = new MemoryStream();
        //            stream.Write(allFaces[i].Image, 0, allFaces[i].Image.Length);
        //            var faceImage = new Image<Gray, byte>(new Bitmap(stream));
        //            faceImages[i] = faceImage.Resize(100, 100, Inter.Cubic);
        //            faceLabels[i] = allFaces[i].UserId;
        //        }
        //        String filepath = ("C:\\Users\\Jathu\\Documents\\Visual Studio 2015\\Projects\\Test3");
        //        _faceRecognizer.Train(faceImages, faceLabels);
        //        _faceRecognizer.Save(filepath);
        //        MessageBox.Show("ada");
        //    }
        //    return true;

        //}
    }
}
