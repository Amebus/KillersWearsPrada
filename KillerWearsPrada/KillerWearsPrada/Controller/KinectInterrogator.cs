using Microsoft.Kinect;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Timers;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace KillerWearsPrada.Controller
{


    /// <summary>
    /// Implementa il thread per il polling sul kinect
    /// </summary>
    class KinectInterrogator
    {
        
        private int attWaitingTime;
        private int attBodyCount;

        private KinectSensor attKinectSensor;

        private ColorFrameReader attColorFrameReader;
        private BodyFrameReader attBodyFrameReader;

        private PlayerChecker attPlayerChecker;
        private BarCodeRecognized attBarcodeChecker;

        private DateTime attLastcheck;

        private BackgroundWorker attScreenshotWorker;
        private volatile bool attBackGroundWorkerBusy; 

        private bool attEnableTakingScreenshot;



        /// <summary>
        /// 
        /// </summary>
        /// <param name="Sensor">The Kinect Sensor to use</param>
        /// <param name="WaitingTime">Reppresent the time, in milliseconnds, to wait before taking another screenshot</param>
        /// <param name="Checker"></param>
        public KinectInterrogator(KinectSensor Sensor, int WaitingTime) 
        {
            attBodyCount = 0;
            attEnableTakingScreenshot = false;
            attBarcodeChecker = new BarCodeRecognized();
            attPlayerChecker = new PlayerChecker();
            attWaitingTime = WaitingTime;
            this.attKinectSensor = Sensor;

            attBodyFrameReader = attKinectSensor.BodyFrameSource.OpenReader();
            attBodyFrameReader.FrameArrived += this.Reader_BodyFrameArrived;

            attColorFrameReader = Sensor.ColorFrameSource.OpenReader();
            attColorFrameReader.FrameArrived += this.Reader_ColorFrameArrived;
            

            attKinectSensor.Open();
            attLastcheck = DateTime.Now;

            SetBackgroundWorker();
        }


        private void SetBackgroundWorker ()
        {
            attBackGroundWorkerBusy = true;
            attScreenshotWorker = new BackgroundWorker();
            attScreenshotWorker.DoWork += worker_DoWork;
            attScreenshotWorker.RunWorkerCompleted += worker_WorkEnded;
            attBackGroundWorkerBusy = false;
        }


        /// <summary>
        /// Return a value that indicate if the KinectSensor is connected
        /// </summary>
        public bool IsKinectConnected
        {
            get { return attKinectSensor.IsAvailable; }
        }

        public ProgressChangedEventHandler ScreenshotWorkerProgressChanged
        {
            set { attScreenshotWorker.ProgressChanged += value; }
        }

        public RunWorkerCompletedEventHandler ScreenshotWorkerCompleted
        {
            set { attScreenshotWorker.RunWorkerCompleted += value; }
        } 

        public EventHandler<PlayerChecker.PlayerEnterKinectSensor.Arguments> PlayerEnterKinectSensorEvent
        {
            set { attPlayerChecker.PlayerEnterKinectSensorEvent += value; }
        }

        public EventHandler<PlayerChecker.PlayerLeaveKinectSensor.Arguments> PlayerLeaveKinectSensorEvent
        {
            set { attPlayerChecker.PlayerLeaveKinectSensorEvent += value; }
        }

        public EventHandler<BarCodeRecognized.Arguments> BarCodeRecognizedEvent
        {
            set { attBarcodeChecker.BarCodeRecognizedEvent += value; }
        }

        /// <summary>
        /// Start the thread that take periodic screenshot for the kinect sensor
        /// </summary>
        public void StartTakingScreenshots()
        {
            attEnableTakingScreenshot = true;
        }

        /// <summary>
        /// Stop the thread that take periodic screenshot for the kinect sensor
        /// </summary>
        public void StopTakingScreenshots()
        {
            attEnableTakingScreenshot = false;
        }


        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            attBackGroundWorkerBusy = true;
            Bitmap wvImage;
            BitmapEncoder wvEncoder;
            BackgroundWorkerParameters wvBWP = (BackgroundWorkerParameters)e.Argument;
            bool wvQRCodeFound;

            
            wvEncoder = new PngBitmapEncoder();
            // create a png bitmap encoder which knows how to save a .png file
            wvEncoder.Frames.Add(BitmapFrame.Create(wvBWP.ImageToBeChecked));

            Stream wvMemoryImege = new MemoryStream();
            wvEncoder.Save(wvMemoryImege);

            wvImage = new Bitmap(wvMemoryImege);
            
            //QRCode found = Player found
            attPlayerChecker.CheckPlayer(Helpers.QRReaderHelper.QRCode(out wvQRCodeFound, wvImage), wvBWP.BodyCount);

            if(wvQRCodeFound)//if a player has been found check for a BarCode
            {
                bool wvBarCodeFound;
                string r = Helpers.QRReaderHelper.BarCode(out wvBarCodeFound, wvImage);
                if (wvBarCodeFound)
                    attBarcodeChecker.RaiseEvent(r);
            }


            wvMemoryImege.Close();
            
        }

        private void worker_WorkEnded(object sender, RunWorkerCompletedEventArgs e)
        {
            attBackGroundWorkerBusy = false;   
        }

        private void Reader_BodyFrameArrived(object sender, BodyFrameArrivedEventArgs e)
        {
            bool wvDataReceived = false;
            BodyFrame wvBodyFrame = e.FrameReference.AcquireFrame();

            if (wvBodyFrame == null)
                return;

            Body[] wvBodies = new Body[wvBodyFrame.BodyCount];

            wvBodyFrame.GetAndRefreshBodyData(wvBodies);
            wvDataReceived = true;


            if (!wvDataReceived)
                return;

            attBodyCount = 0;
            foreach(Body b in wvBodies)
            {
                if (b.IsTracked)
                    attBodyCount++;
            }

        }


        
        private void Reader_ColorFrameArrived(object sender, ColorFrameArrivedEventArgs e)
        {
            if (!attEnableTakingScreenshot)
                return;
            
            if (attBackGroundWorkerBusy)//if the backgroundworker is still checking the existance of a player in front of the kinect return and wait for another screenshot
                return;

            DateTime wvNow = DateTime.Now;
            if (wvNow.Subtract(attLastcheck).Seconds < attWaitingTime)
                return;
            attLastcheck = wvNow;
            //attKinectSensor.BodyFrameSource.BodyCount >= 1;
            ColorFrame wvColorFrame = e.FrameReference.AcquireFrame();

            if (wvColorFrame == null)
                return;
            
            FrameDescription colorFrameDescription = wvColorFrame.FrameDescription;
            KinectBuffer wvColorBuffer = wvColorFrame.LockRawImageBuffer();

            

            WriteableBitmap wvColorBitmap = null;
            FrameDescription wvColorFrameDescription = this.attKinectSensor.ColorFrameSource.CreateFrameDescription(ColorImageFormat.Bgra);
            wvColorBitmap = new WriteableBitmap(wvColorFrameDescription.Width, wvColorFrameDescription.Height, 96.0, 96.0, PixelFormats.Bgr32, null);
            wvColorBitmap.Lock();

            // verify data and write the new color frame data to the display bitmap
            if ((colorFrameDescription.Width == wvColorBitmap.PixelWidth) && (colorFrameDescription.Height == wvColorBitmap.PixelHeight))
            {
                wvColorFrame.CopyConvertedFrameDataToIntPtr(
                    wvColorBitmap.BackBuffer,
                    (uint)(colorFrameDescription.Width * colorFrameDescription.Height * 4),
                    ColorImageFormat.Bgra);

                wvColorBitmap.AddDirtyRect(new Int32Rect(0, 0, wvColorBitmap.PixelWidth, wvColorBitmap.PixelHeight));
            }

            wvColorBitmap.Unlock();

            
            wvColorBitmap.Freeze();//to allow a read of the captured frame in another thread
            BackgroundWorkerParameters wvBWP = new BackgroundWorkerParameters();
            wvBWP.ImageToBeChecked = wvColorBitmap;
            wvBWP.BodyCount = attBodyCount;
            attBackGroundWorkerBusy = true;
            attScreenshotWorker.RunWorkerAsync(wvBWP);

        }

        public class BackgroundWorkerParameters
        {

            public WriteableBitmap ImageToBeChecked
            {
                get; set;
            }

            public int BodyCount
            {
                get; set;
            }

        }

    }
}
