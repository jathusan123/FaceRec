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

using System.IO;

using MySql.Data.MySqlClient;
using Emgu.Util;

using Emgu.CV.UI;
using System.Timers;

namespace Test3
{
    public partial class Recognition : Form
    {
        private Capture capture;
        private Mat frame;
        private int i;
        private int i1;
        private int i2;
        private int i3;
        private bool facedetection;
        private bool facedetection1;
        private bool sidedetection;
        private bool sidedetection1;
        private int tr;
     //   System.Windows.Forms.Timer timer;
        //private int r33;
        //private Image<Gray, byte> userimage33;
        public EigenFaceRecognizer _faceRecognizer;
        CascadeClassifier _cascadeClassifier1 = new CascadeClassifier(Application.StartupPath + "/haarcascade_frontalface_default.xml");
        CascadeClassifier _cascadeClassifier2 = new CascadeClassifier(Application.StartupPath + "/haarcascade_eye.xml");
        CascadeClassifier _cascadeClassifier3 = new CascadeClassifier(Application.StartupPath + "/haarcascade_profileface.xml");
        public Image<Bgr, byte> image2;
        public Trainee trainee = new Trainee();
        DataStoreAccess data = new DataStoreAccess();
       
        public Recognition()


        {

            InitializeComponent();
            //timer = new System.Windows.Forms.Timer();
            //timer.Interval = 50000;
            //timer.Tick += new EventHandler(timer_Tick);
            //timer.Start();



            button2.Hide();
            label2.Hide();
            label3.Hide();
            label4.Hide();
            button1.Hide();
            button2.Hide();
            button3.Hide();
           // MessageBox.Show(double.PositiveInfinity.ToString());
            i = 0;
            i1 = 0;
            i2 = 0;
            i3 = 0;
            facedetection = false;
            sidedetection = false;
            sidedetection1 = false;
            facedetection1 = false;
            trainee.TrainRecognizer();

             capture = new Capture();
            capture.Start();
            //this.recognize();
            
            //for provide live feed of the camera
            capture.ImageGrabbed += Capture_ImageGrabbed1;
           
           



            // capture.ImageGrabbed += Capture_Recognize;
        }

        //private void timer_Tick(object sender, EventArgs e)
        //{
        //    MessageBox.Show("Time Has Finished Try Again");
        //    timer.Stop();
        //    this.Controls.Clear();
        //    Recognition r = new Recognition();
        //    this.Hide();
        //    r.Show();


        //}

        private void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
           // image2 = new Image<Bgr, byte>(facedetect());
        }

        public int RecognizeUser(Image<Gray, byte> userImage)
        {
            //creating new instatnce of facerecognizer
            FaceRecognizer _faceRecognizer = new EigenFaceRecognizer(80,1000);
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
              
                {


                    if (facedetection == false)
                    {

                        {
                            detect();
                        }

                    }

                    else if (sidedetection == false)
                    {

                        {
                            sidedetect();
                        }
                    }
                    else if (facedetection1 == false)
                    {
                        detect1();

                    }
                    else if (sidedetection1 == false)
                    {
                        sidedetect2();
                    }
                    else
                        Invoke(new Action(() =>
                    {

                        button1.Show();
                        button2.Show();

                    }));
                 

                    imageBox1.Image = frame;
                    
                }
                    

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
                    // getting image from camera if face detected
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
            int tr = recognize();
           // Recognition started
            if (tr!= 0)
            {
                label2.Show();
               
                label3.Show();
                label4.Show();
                label5.Text= data.get_name(tr.ToString());
               // button3.Show();

                label7.Text = tr.ToString();

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
                //Creating new attendance 
                attendance a = new attendance(label7.Text);
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
        //validatin human by detecting face
        private   void detect()
        {
            
           
            try
            {
                var img = frame.ToImage<Bgr, byte>();
                var imgframe = img.Convert<Gray, byte>();
                  var faces = _cascadeClassifier1.DetectMultiScale(imgframe, 1.1, 10, Size.Empty);

                //the actual eyes detection happens here
               
                foreach (var eye in faces)
                {
                    if (i < 5)
                    {
                       
                        img.Draw(eye, new Bgr(Color.Green), 3);
                        Invoke(new Action(() =>
                        {
                            progressBar1.Increment( (5));


                        }));
                        i = i + 1;

                    }
                    else
                    {
                        facedetection = true;
               
                        
                        Invoke(new Action(() =>
                        {

                            label8.Text = ("Turn Your face Right or Left Side");

                        }));
                      
                        
                    }
                }

           
                imageBox1.Image = img;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        //detecting side face
        //non frontal images
        private void sidedetect()
        {
      
            Invoke(new Action(() =>
            {
                


            }));
            try
            {
                var img = frame.ToImage<Bgr, byte>();
                var imgframe = img.Convert<Gray, byte>();
               //cascade for profile face
                var half = _cascadeClassifier3.DetectMultiScale(imgframe, 1.1, 10, Size.Empty);
            

                

                foreach (var eye in half)
                {
                    if (i1 < 5)
                    {
                       
                        img.Draw(eye, new Bgr(Color.Red), 3);
                        Invoke(new Action(() =>
                        {
                            progressBar1.Increment(5);


                        }));
                        i1 = i1 + 1;
                       
                    }
                    else
                    {
                       
                            sidedetection = true;
                        
                       
           
                        
                        Invoke(new Action(() =>
                        {
                           
                            label8.Text = ("Turn Your Head To Front"); 

                        }));
                       
                    }
                }
                imageBox1.Image = img;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void detect1()
        {


            try
            {
                var img = frame.ToImage<Bgr, byte>();
                var imgframe = img.Convert<Gray, byte>();
                var faces = _cascadeClassifier1.DetectMultiScale(imgframe, 1.1, 10, Size.Empty);

                //the actual eyes detection happens here

                foreach (var eye in faces)
                {
                    
                    if (i2 < 5)
                    {

                        img.Draw(eye, new Bgr(Color.Green), 3);
                        Invoke(new Action(() =>
                        {
                            try
                            {
                               
                            }
                            catch(Exception ft)
                            {
                                MessageBox.Show(ft.Message);
                            }
                            progressBar1.Increment((5));


                        }));
                        i2 = i2 + 1;
                     

                    }
                    else
                    {
                       
                        facedetection1 = true;
                    
                     
                        Invoke(new Action(() =>
                        {
                           
                            label8.Text = ("Turn Your face Right or Left Side");

                        }));
                      

                    }
                }


                imageBox1.Image = img;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void sidedetect2()
        {

            Invoke(new Action(() =>
            {



            }));
            try
            {
                var img = frame.ToImage<Bgr, byte>();
                var imgframe = img.Convert<Gray, byte>();

                var half = _cascadeClassifier3.DetectMultiScale(imgframe, 1.1, 10, Size.Empty);
                //the actual eyes detection happens here



                foreach (var eye in half)
                {
                    if (i3 < 5)
                    {

                        img.Draw(eye, new Bgr(Color.Red), 3);
                        Invoke(new Action(() =>
                        {
                            progressBar1.Increment(5);


                        }));
                        i3 = i3 + 1;

                    }
                    else
                    {
                        sidedetection1 = true;

                 
                
                        Invoke(new Action(() =>
                        {
                          
                            button1.Show();
                            button2.Show();
                            label8.Text = "Validation Successfull";
                          
                            


                        }));

                    }
                }
                imageBox1.Image = img;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        private void button3_Click_1(object sender, EventArgs e)
        {
           
        }

        private void button12_Click_1(object sender, EventArgs e)
        {

            this.Controls.Clear();
            // this.InitializeComponent();
          
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Controls.Clear();
            Recognition r = new Recognition();
            this.Hide();
            r.Show();
        }
    }
}
