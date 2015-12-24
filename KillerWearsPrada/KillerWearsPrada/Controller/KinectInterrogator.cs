using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Threading;


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

        private Boolean attRunThread;
        private Thread attScreenshotSaver;

        private PlayerChecker attPlayerChecker;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Sensor">The Kinect Sensor to use</param>
        /// <param name="WaitingTime">Reppresent the time, in milliseconnds, to wait before taking another screenshot</param>
        public KinectInterrogator(KinectSensor Sensor, Int32 WaitingTime) 
        {
            attRunThread = true;
            attWaitingTime = WaitingTime;
            attSavePath = "";
            this.attKinectSensor = Sensor;
            attColorBitmap = null;
            attColorFrameReader = Sensor.ColorFrameSource.OpenReader();
            attColorFrameReader.FrameArrived += this.Reader_ColorFrameArrived;
            attScreenshotSaver = new Thread(new ThreadStart(TakeScreenshot));
            attPlayerChecker = new PlayerChecker();
        }

        /// <summary>
        /// Return a value that indicate if the KinectSensor is connected
        /// </summary>
        public Boolean IsKinectConnected
        {
            get { return attKinectSensor.IsAvailable; }
        }

        /// <summary>
        /// Set a property that allow or not the periodic saving of a screenshot
        /// </summary>
        public Boolean AllowRun
        {
            set { attRunThread = value; }
        }

        /// <summary>
        /// Start the thread that take periodic screenshot for the kinect sensor
        /// </summary>
        public void StartTakingScreenshot()
        {
            attRunThread = true;
            attScreenshotSaver.Start();
        }

        /// <summary>
        /// Stop the thread that take periodic screenshot for the kinect sensor
        /// </summary>
        public void StoptakingScreenshot()
        {
            attRunThread = false;
        }

        /// <summary>
        /// Save teh screeenshot on a file 
        /// </summary>
        private void TakeScreenshot()
        {
            while (attRunThread)
            {
                //butta il thread per lo screenshot
                if (this.attColorBitmap == null)
                    continue;

            
                // create a png bitmap encoder which knows how to save a .png file
                BitmapEncoder wvEncoder = new PngBitmapEncoder();

                // create frame from the writable bitmap and add to encoder
                wvEncoder.Frames.Add(BitmapFrame.Create(this.attColorBitmap));


                // create frame from the writable bitmap and add to encoder
                wvEncoder.Frames.Add(BitmapFrame.Create(this.attColorBitmap));

                attSavePath = Helpers.ResourcesHelper.CurrentDirectory + Helpers.ResourcesHelper.ImagesDirectory + "\\" + attScreen;

                //creao uno stream per convertire writablebitmap in bitmap, in questo modo posso usare subito l'immagine
                Stream wvMemoryImege = new MemoryStream();
                wvEncoder.Save(wvMemoryImege);


                attImage = new Bitmap(wvMemoryImege);

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

                Thread.Sleep(attWaitingTime);
            }

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

    }
}
