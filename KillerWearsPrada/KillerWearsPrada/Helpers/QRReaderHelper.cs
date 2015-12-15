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
        private static String indirizzoImmagine;
        #endregion
        

        /// <summary>
        /// Imposta indirizzo immagine da cui estrarre BarCode e QRCode
        /// NB: indirizzo deve essere ASSOLUTO
        /// </summary>
        public  static String IndirizzoImmagine
        {
            set { indirizzoImmagine = value; }
        }

        /// <summary>
        /// Restituisce una stringa che rappresenta il valore del BarCode individuato nell'immagine
        /// <param name="BarCodeFound">Valore che indica se è stato trovato il BarCode o se è stato generato un codice di errore</param>
        /// </summary>
        public static String BarCode (out Boolean BarCodeFound)
        {
            Bitmap immagine;
            BarcodeReader barCodeReader;

            List<BarcodeFormat> br_list = new List<BarcodeFormat>();
            br_list.Add(BarcodeFormat.All_1D);
            /*for (int i = 1; i< 65536; i*=2)
            {
                BarcodeFormat.
            }

            br_list.Add(BarcodeFormat.EAN_13);
            br_list.Add(BarcodeFormat.EAN_8);
            br_list.Add(BarcodeFormat.);

            */
            barCodeReader = new BarcodeReader { AutoRotate = true };
            barCodeReader.Options.PossibleFormats = br_list;
            barCodeReader.Options.TryHarder = true;
            barCodeReader.TryInverted = true;

            Result r;
            immagine = (Bitmap)Image.FromFile(indirizzoImmagine, true);
            r = barCodeReader.Decode(immagine);

            if (r==null)
            {
                BarCodeFound = false;
                return "BarCodeFound=False";
            }

            immagine.Dispose();
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
            Bitmap immagine;
            BarcodeReader qrCodeReader;

            List<BarcodeFormat> qr_list = new List<BarcodeFormat>();
            qr_list.Add(BarcodeFormat.QR_CODE);

            qrCodeReader = new BarcodeReader { AutoRotate = true };
            qrCodeReader.Options.PossibleFormats = qr_list;
            qrCodeReader.Options.TryHarder = true;
            qrCodeReader.TryInverted = true;

            Result r;
            immagine = (Bitmap)Image.FromFile(indirizzoImmagine, true);
            r = qrCodeReader.Decode(immagine);

            if (r == null)
            {
                QRCodeFound = false;
                return "ERRORE";
            }

            immagine.Dispose();
            QRCodeFound = true;
            return r.Text.ToString();

        }

        public static void GenerateQRCode (String Testo)
        {
            Bitmap immagine;
            BarcodeWriter qrCodeWriter = new BarcodeWriter();
            qrCodeWriter.Format = BarcodeFormat.QR_CODE;
            ZXing.Common.EncodingOptions encOptions = new ZXing.Common.EncodingOptions() { Width = 500, Height = 500, Margin = 1 };
            qrCodeWriter.Options = encOptions;

            immagine = qrCodeWriter.Write(Testo);

            ///implementare per il salvataggio/stampa del QRCode

        }

    }
}
