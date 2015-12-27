using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Media.Imaging;

namespace KillerWearsPrada.Controller
{
    

    /// <summary>
    /// Implementa il thread per il polling sul kinect
    /// </summary>
    class KinectInterrogator
    {

        //TODO Scatenare l'evento qundo viene salvato uno screenshot e si riconosce il giocatore 
        private const String attScreen = "screenshot.png";
        private String attSavePath;
        private Int32 attWaitingTime;

        private KinectSensor attKinectSensor;
        private WriteableBitmap attColorBitmap;
        private Bitmap attImage;

        private ColorFrameReader attColorFrameReader;

        private PlayerChecker attPlayerChecker;

        private Timer attTimerScreenshots;
        private BackgroundWorker attScreenshotWorker;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Sensor">The Kinect Sensor to use</param>
        /// <param name="WaitingTime">Reppresent the time, in milliseconnds, to wait before taking another screenshot</param>
        /// <param name="Checker"></param>
        public KinectInterrogator(KinectSensor Sensor, Int32 WaitingTime) 
        {
            attPlayerChecker = new PlayerChecker();
            attWaitingTime = WaitingTime;
            attSavePath = "";
            this.attKinectSensor = Sensor;
            attColorBitmap = null;
            attColorFrameReader = Sensor.ColorFrameSource.OpenReader();
            attColorFrameReader.FrameArrived += this.Reader_ColorFrameArrived;
            //attScreenshotSaver = new Thread(new ThreadStart(TakeScreenshot));

            attTimerScreenshots = new Timer(WaitingTime);
            attTimerScreenshots.Elapsed += TimerScreenshotsTick;

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
        public Boolean IsKinectConnected
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

        public EventHandler<PlayerChecker.PlayerEnterKinectSensor.Args> RaisePlayerEnterKinectSensor
        {
            set { attPlayerChecker.RaisePlayerEnterKinectSensor += value; }
        }

        public EventHandler<PlayerChecker.PlayerLeaveKinectSensor.Args> RaisePlayerLeaveKinectSensor
        {
            set { attPlayerChecker.RaisePlayerLeaveKinectSensor += value; }
        }

        /// <summary>
        /// Start the thread that take periodic screenshot for the kinect sensor
        /// </summary>
        public void StartTakingScreenshot()
        {
            attTimerScreenshots.Start();
        }

        /// <summary>
        /// Stop the thread that take periodic screenshot for the kinect sensor
        /// </summary>
        public void StopTakingScreenshot()
        {
            if(attScreenshotWorker.IsBusy)
            {
                attScreenshotWorker.CancelAsync();
            }
            attTimerScreenshots.Stop();
        }

        private void TimerScreenshotsTick(object sender, ElapsedEventArgs e)
        {
            if (!attScreenshotWorker.IsBusy)
            {
                attScreenshotWorker.RunWorkerAsync(new BackgroundWorkerParameters());
            }
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            //butta il thread per lo screenshot
            if (this.attColorBitmap == null)
                return;

            BackgroundWorkerParameters wvBWP = (BackgroundWorkerParameters)e.Argument;

            BitmapEncoder wvEncoder;
            Boolean wvQRCodeFound;
            

            // create a png bitmap encoder which knows how to save a .png file
            wvEncoder = new PngBitmapEncoder();

            // create frame from the writable bitmap and add to encoder
            wvEncoder.Frames.Add(BitmapFrame.Create(this.attColorBitmap));


            // create frame from the writable bitmap and add to encoder
            wvEncoder.Frames.Add(BitmapFrame.Create(this.attColorBitmap));

            attSavePath = Helpers.ResourcesHelper.CurrentDirectory + Helpers.ResourcesHelper.ImagesDirectory + "\\" + attScreen;

            //creao uno stream per convertire writablebitmap in bitmap, in questo modo posso usare subito l'immagine
            Stream wvMemoryImege = new MemoryStream();
            wvEncoder.Save(wvMemoryImege);
            attImage = new Bitmap(wvMemoryImege);

            attPlayerChecker.CheckPlayer(Helpers.QRReaderHelper.QRCode(out wvQRCodeFound, attImage));
            
            wvMemoryImege.Close();

            // write the new file to disk
            try
            {
                // FileStream is IDisposable
                FileStream fs = new FileStream(attSavePath, FileMode.Create);
                wvEncoder.Save(fs);
                fs.Close();
                //this.StatusText = string.Format(Properties.Resources.SavedScreenshotStatusTextFormat, path);
            }
            catch (IOException)
            {
                //this.StatusText = string.Format(Properties.Resources.FailedScreenshotStatusTextFormat, path);
            }

            throw new NotImplementedException("Mettere i controlli sul kinect disponibile");
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
            ColorFrame wvColorFrame = e.FrameReference.AcquireFrame();

            if (wvColorFrame == null)
                return;
            
            FrameDescription colorFrameDescription = wvColorFrame.FrameDescription;

            KinectBuffer wvColorBuffer = wvColorFrame.LockRawImageBuffer();
            
            this.attColorBitmap.Lock();

            // verify data and write the new color frame data to the display bitmap
            if ((colorFrameDescription.Width == this.attColorBitmap.PixelWidth) && (colorFrameDescription.Height == this.attColorBitmap.PixelHeight))
            {
                wvColorFrame.CopyConvertedFrameDataToIntPtr(
                    this.attColorBitmap.BackBuffer,
                    (uint)(colorFrameDescription.Width * colorFrameDescription.Height * 4),
                    ColorImageFormat.Bgra);

                this.attColorBitmap.AddDirtyRect(new Int32Rect(0, 0, this.attColorBitmap.PixelWidth, this.attColorBitmap.PixelHeight));
            }

            this.attColorBitmap.Unlock();
            
        }

        public class BackgroundWorkerParameters
        {

        }

    }
}
