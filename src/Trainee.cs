using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Emgu;
using Emgu.CV.Structure;
using Emgu.CV;
using Emgu.CV.Face;
using System.IO;
using Emgu.CV.CvEnum;
using System.Windows.Forms;

namespace Test3
{
    public class Trainee
    {
        public void TrainRecognizer()
        {

          //  FaceRecognizer _faceRecognizer = new LBPHFaceRecognizer(1, 8, 8, 8, double.PositiveInfinity);
           FaceRecognizer _faceRecognizer = new EigenFaceRecognizer(80, double.PositiveInfinity);
            DataStoreAccess _datastoreaccess = new DataStoreAccess();
            var allFaces = _datastoreaccess.callfaces();
          //  MessageBox.Show(allFaces.Count.ToString());
            if (allFaces != null)
            {
                
                var faceImages = new Image<Gray, byte>[allFaces.Count];
                var faceLabels = new int[allFaces.Count];
                for (int i = 0; i < allFaces.Count; i++)
                {
                    
                    Stream stream = new MemoryStream();
                    stream.Write(allFaces[i].Image, 0, allFaces[i].Image.Length);
                    var faceImage = new Image<Gray, byte>(new Bitmap(stream));
                    // faceImages[i] = faceImage.Resize(100, 100, Inter.Cubic);
                    faceImages[i] = faceImage.Resize(100, 100, Inter.Cubic);
                    faceLabels[i] = Convert.ToInt32( allFaces[i].Label);
                }
               
                string filepath = Application.StartupPath + "/traineddata";
               // var filePath = Application.StartupPath + String.Format("/{0}.bmp", face);
                _faceRecognizer.Train(faceImages, faceLabels);
            
                _faceRecognizer.Save(filepath);
               // MessageBox.Show(allFaces[0].Label);

            }
            else
            {
                //MessageBox.Show("adasfsf");
            }
           

        }
    }
}
