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
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.Face;
using Emgu.CV.CvEnum;

using Emgu.Util;

using Emgu.CV.UI;
using System.Timers;

namespace Test3
{
    public partial class back : Form
    {
        private Capture capture;
        private Mat frame;
    
        public EigenFaceRecognizer _faceRecognizer;
        CascadeClassifier _cascadeClassifier1 = new CascadeClassifier(Application.StartupPath + "/haarcascade_frontalface_default.xml");
        CascadeClassifier _cascadeClassifier2 = new CascadeClassifier(Application.StartupPath + "/haarcascade_eye.xml");
        public Image<Bgr, byte> image2;
        public Trainee trainee = new Trainee();
        DataStoreAccess data = new DataStoreAccess();
       // _cascadeClassifier = new CascadeClassifier(Application.StartupPath + "/haarcascade_frontalface_alt_tree
        public back()


        {
            InitializeComponent();
            

          
          
            button2.Hide();
            label2.Hide();
            label3.Hide();
            label4.Hide();
            trainee.TrainRecognizer();

             capture = new Capture();
            capture.Start();
            //this.recognize();
            if (facedetect()!= null)
            {
                image2 = new Image<Bgr, byte>(facedetect());
            }
            //for provide live feed of the camera
            capture.ImageGrabbed += Capture_ImageGrabbed1;
           
            //System.Timers.Timer timer = new System.Timers.Timer();
            //timer.Interval = 4000;

            //timer.Elapsed += timer_Elapsed;
            //  timer.Start();



            // capture.ImageGrabbed += Capture_Recognize;
        }

        private void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
           // image2 = new Image<Bgr, byte>(facedetect());
        }

        public int RecognizeUser(Image<Gray, byte> userImage)
        {
            //creating new instatnce of facerecognizer
            FaceRecognizer _faceRecognizer = new EigenFaceRecognizer(80, double.PositiveInfinity);
           // EigenObjectRecognizer recognizer = new EigenObjectRecognizer(trainingImages.ToArray(), ref termCrit);
      // LBPHFaceRecognizer _faceRecognizer = new LBPHFaceRecognizer(1,8,8,8, double.PositiveInfinity);
            string _recognizerFilePath = Application.StartupPath + "/traineddata";

            _faceRecognizer.Load(_recognizerFilePath);

            var result = _faceRecognizer.Predict(userImage.Resize(100, 100, Inter.Cubic));
          
            return result.Label;
        }
        private void Capture_ImageGrabbed1(object sender, EventArgs e)
        {
            try
            {
                frame = new Mat();
                capture.Retrieve(frame, 0);


                imageBox1.Image = frame;
            }
            catch(Exception dd)
            {
                MessageBox.Show(dd.Message);
            }

        }
        //recognizing based on emuga face recognizer
        public int recognize()
        {
            try
            {
                frame = new Mat();
                capture.Retrieve(frame, 0);
                if (facedetect() != null)
                {
                    var userimage = new Image<Gray, byte>(facedetect());

                    int r = RecognizeUser(userimage);
                    imageBox1.Image = frame;
                    try
                    {
                        if (r != 0)
                        {
                            Invoke(new Action(() =>
                            {


                            }));
                            return r;
                        }
                        else
                        {
                            Invoke(new Action(() =>
                            {

                            }));
                            return 0;

                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    return r;
                }
                else
                {

                    MessageBox.Show("No faces Detected");
                    return 0;
                }
            }
            catch(Exception re)
            {
                MessageBox.Show(re.Message);
                return 0;
            }
        }

        private void Recognition_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (recognize() != 0)
            {
                label2.Show();
                label3.Show();
                label4.Show();
                label5.Text= data.get_name(recognize().ToString());
                label7.Text = recognize().ToString();

                button2.Show();
            };

        }
        //face detect funtion return the detected faces
        public Bitmap facedetect()
        {
            var imageFrame = capture.QueryFrame().ToImage<Bgr, Byte>();

            try
            {
                
              
                var grayframe = imageFrame.Convert<Gray, byte>();
                var faces = _cascadeClassifier1.DetectMultiScale(grayframe, 1.1, 10, Size.Empty);
                if (faces.Count() != 0)
                {
                    
                        imageFrame.Draw(faces[0], new Bgr(Color.BurlyWood), 3);
                        return imageFrame.Copy(faces[0]).Bitmap;
                
                   
                }
                else {
          
              
                  

                    return null;
               }


               
                
            }
            catch (Exception ex)
            {
               
                MessageBox.Show(ex.Message);
                return imageFrame.ToBitmap();
            }

        }

        private void imageBox1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("Are You Sure?", "Confirmation",
    MessageBoxButtons.YesNo, MessageBoxIcon.Question,
    MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes))
            {
                attendance a = new attendance("001");
                a.markattendance();
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
