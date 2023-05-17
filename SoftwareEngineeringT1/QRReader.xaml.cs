using AForge.Video.DirectShow;
using AForge.Video;
using ZXing;
using BusinessLayer;
using SoftwareEngineeringTeam1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Drawing;

namespace SoftwareEngineeringT1
{
    public partial class QRReader : Window
    {
        FilterInfoCollection filterInfoCollection;
        VideoCaptureDevice captureDevice;

        public static string resultat { get; set; } = "";

        System.Windows.Forms.PictureBox Camera = new System.Windows.Forms.PictureBox();
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();

        public QRReader()
        {
             InitializeComponent();
            Application.Current.MainWindow.Closing += new CancelEventHandler(QRWindow_Closing);
            CameraWindow.Child = Camera;
            dispatcherTimer.Tick += new EventHandler(Read_Interval);
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(300);
            dispatcherTimer.Start();

            filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach(FilterInfo filterinfo in filterInfoCollection)
            {
                Devices.Items.Add(filterinfo.Name);
                Devices.SelectedIndex = 0;
            }

            if (captureDevice == null)
            {
                captureDevice = new VideoCaptureDevice(filterInfoCollection[Devices.SelectedIndex].MonikerString);
                captureDevice.NewFrame += Update_Image;

                if (!captureDevice.IsRunning)
                    captureDevice.Start();
            }
        }

        private void Update_Image(object sender, NewFrameEventArgs eventArgs)
        {
            Camera.Image = (Bitmap)eventArgs.Frame.Clone();
        }

        private void QRWindow_Closing(object sender, CancelEventArgs e)
        { 
            if(captureDevice.IsRunning)
            {
                captureDevice.Stop();
            }

        }

        private void Read_Interval(object sender, EventArgs e)
        {
            if(Camera.Image != null)
            {
               //MessageBox.Show("2");
                BarcodeReader barcodeReader = new BarcodeReader();
                Result result = barcodeReader.Decode((Bitmap)Camera.Image);
                if(result != null)
                {
                    MessageBox.Show("Successfully scanned code with reservation ID: " + result.ToString());

                    resultat = result.ToString();
                    dispatcherTimer.Stop();
                    if (captureDevice.IsRunning)
                    {
                        //MessageBox.Show("4");
                        captureDevice.Stop();
                    }

                    this.Close();
                }
            }
        }
    }
}
