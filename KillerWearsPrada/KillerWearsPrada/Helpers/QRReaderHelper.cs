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
        /// Restituisce una stringa che rappresenta il valore del BarCode individuato nell'immagine
        /// <param name="BarCodeFound">Valore che indica se è stato trovato il BarCode o se è stato generato un codice di errore</param>
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
        /// Restituisce una stringa che rappresenta il valore del QRCode individuato nell'immagine
        /// </summary>
        /// <param name="QRCodeFound">Valore che indica se è stato trovato il QRCode o se è stato generato un codice di errore</param>
        /// <returns></returns>
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
                return "ERRORE";
            }

            wvImage.Dispose();
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
