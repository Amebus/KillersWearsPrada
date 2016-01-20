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

        //TODO Scatenare l'evento qundo viene salvato uno screenshot e si riconosce il giocatore 
        private const string attScreen = "screenshot.png";
        private int attWaitingTime;

        private KinectSensor attKinectSensor;

        private ColorFrameReader attColorFrameReader;

        private PlayerChecker attPlayerChecker;
        private BarCodeRecognized attBarcodeChecker;

        private BackgroundWorker attScreenshotWorker;

        private bool attEnableTakingScreenshot;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Sensor">The Kinect Sensor to use</param>
        /// <param name="WaitingTime">Reppresent the time, in milliseconnds, to wait before taking another screenshot</param>
        /// <param name="Checker"></param>
        public KinectInterrogator(KinectSensor Sensor, int WaitingTime) 
        {
            attEnableTakingScreenshot = false;
            attBarcodeChecker = new BarCodeRecognized();
            attPlayerChecker = new PlayerChecker();
            attWaitingTime = WaitingTime;
            this.attKinectSensor = Sensor;
            
            attColorFrameReader = Sensor.ColorFrameSource.OpenReader();
            attColorFrameReader.FrameArrived += this.Reader_ColorFrameArrived;
            //attScreenshotSaver = new Thread(new ThreadStart(TakeScreenshot));

            SetBackgroundWorker();
        }

        private void SetBackgroundWorker ()
        {
            attScreenshotWorker = new BackgroundWorker();
            attScreenshotWorker.WorkerReportsProgress = true;
            attScreenshotWorker.WorkerSupportsCancellation = true;
            attScreenshotWorker.DoWork += worker_DoWork;
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
            set { attPlayerChecker.RaisePlayerEnterKinectSensor += value; }
        }

        public EventHandler<PlayerChecker.PlayerLeaveKinectSensor.Arguments> PlayerLeaveKinectSensorEvent
        {
            set { attPlayerChecker.RaisePlayerLeaveKinectSensor += value; }
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

            //attSavePath = Helpers.ResourcesHelper.ImagesDirectory + "\\" + attScreen;

            //creao uno stream per convertire writablebitmap in bitmap, in questo modo posso usare subito l'immagine
            //wvImage = wvBWP.ImageToBeChecked;

            attPlayerChecker.CheckPlayer(Helpers.QRReaderHelper.QRCode(out wvQRCodeFound, wvImage));

            if(wvQRCodeFound)
            {
                bool wvBarCodeFound;
                string r = Helpers.QRReaderHelper.BarCode(out wvBarCodeFound, wvImage);
                if (wvBarCodeFound)
                    attBarcodeChecker.RaiseEvent(r);
            }


            wvMemoryImege.Close();



            //throw new NotImplementedException("Mettere i controlli sulla disponibilità del kinect");
            /*codice utile per scatenare gli eventi del backgroundworker
            int max = (int)e.Argument;
            int result = 0;
            for (int i = 0; i < max; i++)
            {
                int progressPercentage = Convert.ToInt32(((double)i / max) * 100);
                if (i % 42 == 0)
                {
                    result++;
                    (sender as BackgroundWorker).ReportProgress(progressPercentage, i);
                }
                else
                    (sender as BackgroundWorker).ReportProgress(progressPercentage);
                System.Threading.Thread.Sleep(1);

            }
            e.Result = result;
            */
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Reader_ColorFrameArrived(object sender, ColorFrameArrivedEventArgs e)
        {
            if (!attEnableTakingScreenshot)
                return;

            if (attScreenshotWorker.IsBusy)
                return;

            
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


            //BitmapEncoder wvEncoder = new PngBitmapEncoder();
            // create a png bitmap encoder which knows how to save a .png file
            //wvEncoder.Frames.Add(BitmapFrame.Create(wvColorBitmap));

            //Stream wvMemoryImege = new MemoryStream();
            //wvEncoder.Save(wvMemoryImege);


            //Bitmap wvImage = new Bitmap(wvMemoryImege);
            //wvMemoryImege.Close();
            wvColorBitmap.Freeze();
            BackgroundWorkerParameters wvBWP = new BackgroundWorkerParameters();
            wvBWP.ImageToBeChecked = wvColorBitmap;
            attScreenshotWorker.RunWorkerAsync(wvBWP);

        }

        public class BackgroundWorkerParameters
        {

            public WriteableBitmap ImageToBeChecked
            {
                get; set;
            }

        }

    }
}
