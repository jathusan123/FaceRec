using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using Emgu;
using Emgu.CV;
using Emgu.CV.Structure;
using MySql.Data.MySqlClient;

namespace Test3
{
    public partial class Form1 : Form


    {
        public string emp_id = registration.emp_id;
        public string name1 = registration.name1;
      public string email1 = registration.email1;
     public int phnno = registration.phn_no;
      public string role1 = registration.role1;
        
        public int check=0;
        
        private  Capture capture;
        Mat myNewMat;
         
        public ImageList pics;
        public List<Image> imageList;
        Mat frame;
     
      

       CascadeClassifier _cascadeClassifier1 = new CascadeClassifier(Application.StartupPath + "/haarcascade_frontalface_default.xml");
        CascadeClassifier _cascadeClassifier2 = new CascadeClassifier(Application.StartupPath + "/haarcascade_eye.xml");
        CascadeClassifier _cascadeClassifier3 = new CascadeClassifier(Application.StartupPath + "/haarcascade_profileface.xml");
        public Form1()
        {
            InitializeComponent();
            
            pics = new ImageList();
            imageList = new List<Image>();
            capture = new Capture();
            capture.Start();
          
            capture.ImageGrabbed += Capture_ImageGrabbed1;
            
        }

        private void Capture_ImageGrabbed1(object sender, EventArgs e)
        {
            frame = new Mat() ;
            capture.Retrieve(frame, 0);
            facedetect(frame);

        }
        private void button5_Click(object sender, EventArgs e)
        {
           
        }
        // save the image
        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are You Sure To Save The Current Image", "Title", MessageBoxButtons.YesNoCancel,
        MessageBoxIcon.Information);
            if (dr == DialogResult.Yes)
            {
               
                if (check > 0)
                {


                    var faceToSave = new Image<Gray, byte>(sample.Image.Bitmap);
                   
                    Byte[] file;
                    IDataStoreAccess dataStore = new DataStoreAccess();
                    var username = emp_id;
            
                    var filePath = Application.StartupPath + String.Format("/{0}.bmp", username);
                  
                    faceToSave.Save(filePath);
                    using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                    {
                        using (var reader = new BinaryReader(stream))
                        {
                            file = reader.ReadBytes((int)stream.Length);
                        }
                    }
                    if (check == 1)
                    {
                      dataStore.insertuser(name1, phnno, email1, emp_id, role1);

                    }
                    dataStore.SaveFace(username, file);
               



                }
                else {
                    MessageBox.Show("Take Atleast One Image");
                        }
                    }
        }
        private void Grab1(object sender, EventArgs e) {

           
        }
        private void facedetect(Mat img2)
        {
            try
            {
                var img = frame.ToImage<Bgr, byte>();
                var imgframe = img.Convert<Gray, byte>();
              //  var faces = _cascadeClassifier1.DetectMultiScale(imgframe, 1.1, 10, Size.Empty);
               // var eyes = _cascadeClassifier2.DetectMultiScale(imgframe, 1.1, 10, Size.Empty);
                var half = _cascadeClassifier3.DetectMultiScale(imgframe, 1.1, 10, Size.Empty);
                //the actual eyes detection happens here

                //foreach (var face in faces)
                //{
                //    img.Draw(face, new Bgr(Color.Red), 3);


                //}

                foreach (var eye in half)
                {
                    img.Draw(eye, new Bgr(Color.Green), 3);

                }
                imageBox1.Image = img;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
           // var faceToSave1 = new Image<Gray, byte>(imageBox1.Image.Bitmap);
            // pics.Images.Add(imageBox1.Image.Bitmap);
            pics.Images.Add("0",imageBox1.Image.Bitmap);
            CascadeClassifier _cascadeClassifier1 = new CascadeClassifier(Application.StartupPath + "/haarcascade_frontalface_default.xml");
            var img = frame.ToImage<Bgr, byte>();
            var imgframe = img.Convert<Gray, byte>();
            var faces = _cascadeClassifier1.DetectMultiScale(imgframe, 1.1, 10, Size.Empty);
            if (faces.Count() != 0)
            {
                Bitmap croppedImage = CropImage(img.ToBitmap(), faces[0]);
                var faceToSave12 = new Image<Gray, byte>(croppedImage);
                imageList.Add(imageBox1.Image.Bitmap);
                Mat fr = new Mat();
                capture.Retrieve(fr, 0);
                sample.Image = faceToSave12;
                check = check + 1;
            }
            else
            {
                MessageBox.Show("No Faces Detected Try Again");
            }
          
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button6_Click(object sender, EventArgs e)
        { MessageBox.Show("Employee Registration Succesfull");
            this.Close();

        }
        public Bitmap CropImage(Bitmap source, Rectangle section)
        {   //empty file for holding croped image
            Bitmap bmp = new Bitmap(section.Width, section.Height);
            Graphics g = Graphics.FromImage(bmp);

            // Draw the given area (section) of the source image
            // at location 0,0 on the empty bitmap (bmp)
            g.DrawImage(source, 0, 0, section, GraphicsUnit.Pixel);

            return bmp;

        }

        private void test_Click(object sender, EventArgs e)
        {
            MessageBox.Show(emp_id);
        }

        private void previous_Click(object sender, EventArgs e)
        {
           
            
        //        if ((imageList.IndexOf(sample.Image.Bitmap)) - 1>=0)
        //    {
        //        sample.Refresh();
        //        int index1 = (imageList.IndexOf(sample.Image.Bitmap));
        //       // sample.Image = imageList[index1-1].;
        //    }
        //    else
        //    {
        //        MessageBox.Show("No More Images");
        //    }
               
        }

        private void next_Click(object sender, EventArgs e)
        {

           
            //if ((imageList.IndexOf(sample.Image)) + 1 < imageList.Count)
          //  {
               // int index2 = (imageList.IndexOf(sample.Image));

               // sample.Image = imageList[index2 + 1];
           // }
           // else
           // {
          //      MessageBox.Show("No More Images");
          //  }

        }

        private void delete_Click(object sender, EventArgs e)
        {
           // int index = (imageList.IndexOf(sample.Image));
           // imageList.RemoveAt(index);

        }

        private void image3_Click(object sender, EventArgs e)
        {
            //sample.Image = pics.Images[2];
        }

        private void image2_Click(object sender, EventArgs e)
        {
            //sample.Image = imageList[1];
        }

        private void image1_Click(object sender, EventArgs e)
        {
            //sample.Image = pics.Images[0];
        }

        private void imageBox1_Click(object sender, EventArgs e)
        {

        }

        private void sample_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
