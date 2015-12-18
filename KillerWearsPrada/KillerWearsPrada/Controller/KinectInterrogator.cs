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

namespace KillerWearsPrada.Controller
{
    

    /// <summary>
    /// Implementa il thread per il polling sul kinect
    /// </summary>
    class KinectInterrogator
    {
        private const string screen = "screenshot.png";
        KinectSensor kinectSensor;
        WriteableBitmap colorBitmap;

        ColorFrameReader colorFrameReader;

        public KinectInterrogator(KinectSensor ks)
        {
            kinectSensor = ks;
            colorBitmap = null;
            colorFrameReader = ks.ColorFrameSource.OpenReader();
            colorFrameReader.FrameArrived += this.Reader_ColorFrameArrived;
        }


        public bool IsKinectConnected
        {
            get { return Helpers.KinectHelper.IsKinectConected; }
        }



        public String TakeScreenshot(KinectSensor ks)
        {
            //butta il thread per lo screenshot

            if (this.colorBitmap != null)
            {
                // create a png bitmap encoder which knows how to save a .png file
                BitmapEncoder encoder = new PngBitmapEncoder();

                // create frame from the writable bitmap and add to encoder
                encoder.Frames.Add(BitmapFrame.Create(this.colorBitmap));


                // create frame from the writable bitmap and add to encoder
                encoder.Frames.Add(BitmapFrame.Create(this.colorBitmap));

                String path = Helpers.ResourcesHelper.CurrentDirectory + Helpers.ResourcesHelper.ImagesDirectory + "\\" + screen;

                // write the new file to disk
                try
                {
                    // FileStream is IDisposable
                    using (FileStream fs = new FileStream(path, FileMode.Create))
                    {
                        encoder.Save(fs);
                    }

                    //this.StatusText = string.Format(Properties.Resources.SavedScreenshotStatusTextFormat, path);
                }
                catch (IOException)
                {
                    //this.StatusText = string.Format(Properties.Resources.FailedScreenshotStatusTextFormat, path);
                }
                return path;
            }

            return null;

        }

        private void Reader_ColorFrameArrived(object sender, ColorFrameArrivedEventArgs e)
        {
            using (ColorFrame colorFrame = e.FrameReference.AcquireFrame())
            {
                if (colorFrame != null)
                {
                    FrameDescription colorFrameDescription = colorFrame.FrameDescription;

                    using (KinectBuffer colorBuffer = colorFrame.LockRawImageBuffer())
                    {
                        this.colorBitmap.Lock();

                        // verify data and write the new color frame data to the display bitmap
                        if ((colorFrameDescription.Width == this.colorBitmap.PixelWidth) && (colorFrameDescription.Height == this.colorBitmap.PixelHeight))
                        {
                            colorFrame.CopyConvertedFrameDataToIntPtr(
                                this.colorBitmap.BackBuffer,
                                (uint)(colorFrameDescription.Width * colorFrameDescription.Height * 4),
                                ColorImageFormat.Bgra);

                            this.colorBitmap.AddDirtyRect(new Int32Rect(0, 0, this.colorBitmap.PixelWidth, this.colorBitmap.PixelHeight));
                        }

                        this.colorBitmap.Unlock();
                    }
                }
            }

        }

    }
}
