using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

using ZXing;

namespace KillerWearsPrada.Helpers
{
    class QRReaderHelper
    {

        #region attributi
        /// <summary>
        /// Da vedere se riposizionare nei singoli metodi
        /// </summary>
        private static String attImagePath;
        #endregion
        

        /// <summary>
        /// Imposta indirizzo immagine da cui estrarre BarCode e QRCode
        /// NB: indirizzo deve essere ASSOLUTO
        /// </summary>
        public  static String ImagePath
        {
            set { attImagePath = value; }
        }

        /// <summary>
        /// Resturn a <see cref="String"/> which represent the value of the BarCode found in an image
        /// <param name="BarCodeFound">True if the BarCode was find, False if was generate an error code</param>
        /// <returns>The BarCode found or an error string</returns>
        /// </summary>
        public static String BarCode (out Boolean BarCodeFound)
        {
            Bitmap wvImage;
            BarcodeReader wvBarCodeReader;

            List<BarcodeFormat> wvBarCodesList = new List<BarcodeFormat>();
            wvBarCodesList.Add(BarcodeFormat.All_1D);
            /*for (int i = 1; i< 65536; i*=2)
            {
                BarcodeFormat.
            }

            br_list.Add(BarcodeFormat.EAN_13);
            br_list.Add(BarcodeFormat.EAN_8);
            br_list.Add(BarcodeFormat.);

            */
            wvBarCodeReader = new BarcodeReader { AutoRotate = true };
            wvBarCodeReader.Options.PossibleFormats = wvBarCodesList;
            wvBarCodeReader.Options.TryHarder = true;
            wvBarCodeReader.TryInverted = true;

            Result r;
            wvImage = (Bitmap)Image.FromFile(attImagePath, true);
            r = wvBarCodeReader.Decode(wvImage);

            if (r==null)
            {
                BarCodeFound = false;
                return "BarCodeFound=False";
            }

            wvImage.Dispose();
            BarCodeFound = true;
            return r.Text.ToString();
          
        }

        /// <summary>
        /// Resturn a <see cref="String"/> which represent the value of the QRCode found in an image
        /// </summary>
        /// <param name="QRCodeFound">True if the QRCode was find, False if was generate an error code</param>
        /// <returns>The QRCode found or an error string</returns>
        public static String QRCode(out Boolean QRCodeFound)
        {
            Bitmap wvImage;
            BarcodeReader wvQRCodeReader;

            List<BarcodeFormat> wvQRlist = new List<BarcodeFormat>();
            wvQRlist.Add(BarcodeFormat.QR_CODE);

            wvQRCodeReader = new BarcodeReader { AutoRotate = true };
            wvQRCodeReader.Options.PossibleFormats = wvQRlist;
            wvQRCodeReader.Options.TryHarder = true;
            wvQRCodeReader.TryInverted = true;

            Result r;
            wvImage = (Bitmap)Image.FromFile(attImagePath, true);
            r = wvQRCodeReader.Decode(wvImage);

            if (r == null)
            {
                QRCodeFound = false;
                return "QRCodeFound=False";
            }

            wvImage.Dispose();
            QRCodeFound = true;
            return r.Text.ToString();

        }

        /// <summary>
        /// Resturn a <see cref="String"/> which represent the value of the QRCode found in an image
        /// </summary>
        /// <param name="QRCodeFound">True if the QRCode was find, False if was generate an error code</param>
        /// <param name="Image">The <see cref="Bitmap"/> image in which search the QRCode</param>
        /// <returns>The QRCode found or an error string</returns>
        public static String QRCode(out Boolean QRCodeFound, Bitmap Image)
        {
            BarcodeReader wvQRCodeReader;

            List<BarcodeFormat> wvQRlist = new List<BarcodeFormat>();
            wvQRlist.Add(BarcodeFormat.QR_CODE);

            wvQRCodeReader = new BarcodeReader { AutoRotate = true };
            wvQRCodeReader.Options.PossibleFormats = wvQRlist;
            wvQRCodeReader.Options.TryHarder = true;
            wvQRCodeReader.TryInverted = true;

            Result r;
            r = wvQRCodeReader.Decode(Image);

            if (r == null)
            {
                QRCodeFound = false;
                return "QRCodeFound=False";
            }
            
            QRCodeFound = true;
            return r.Text.ToString();
        }

        public static void GenerateQRCode (String Text)
        {
            Bitmap wvImage;
            BarcodeWriter wvQRCodeWriter = new BarcodeWriter();
            wvQRCodeWriter.Format = BarcodeFormat.QR_CODE;
            ZXing.Common.EncodingOptions encOptions = new ZXing.Common.EncodingOptions() { Width = 500, Height = 500, Margin = 1 };
            wvQRCodeWriter.Options = encOptions;

            wvImage = wvQRCodeWriter.Write(Text);

            ///implementare per il salvataggio/stampa del QRCode

        }

    }
}
