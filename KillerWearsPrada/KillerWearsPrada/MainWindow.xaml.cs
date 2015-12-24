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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

using Microsoft.Kinect;
using Microsoft.Kinect.Wpf.Controls;


namespace KillerWearsPrada
{   
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const Int32 REFRESH_TIME = 1000;


        //Controller.KinectInterrogator attKinectInterrogator;
        Controller.GameController attGameController;


        Window attDebug;


        public MainWindow()
        {

            Helpers.ResourcesHelper.SaveCurrentDirectory();
            Helpers.ResourcesHelper.ModifyMainBackgroundPath();
            //txtDisplay.AppendText(dir_ok);
            InitializeComponent();

            KinectRegion.SetKinectRegion(this, kinectRegion);

            App wvApp = ((App)Application.Current);
            wvApp.KinectRegion = kinectRegion;

            // Use the default sensor
            this.kinectRegion.KinectSensor = KinectSensor.GetDefault();
            attGameController = new Controller.GameController(this.kinectRegion.KinectSensor);
            //attKinectInterrogator = new Controller.KinectInterrogator(this.kinectRegion.KinectSensor, REFRESH_TIME);

        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            attDebug = new UC.DebugWindow();
            attDebug.Show();
            //txtDisplay.Text = Application.Current.Resources["Application_Start_Image"].ToString();
            //txtDisplay.Visibility = Visibility.Visible;
            //txtDisplay.AppendText(@"\n\r");
            /*String s;
            Boolean b;
            Helpers.QRReaderHelper.IndirizzoImmagine = @"C:\Users\Monica\Downloads\2\DSC_0011.jpg";
            s = Helpers.QRReaderHelper.BarCode(out b);
            
            txtDisplay.Text = s;*/

        }

        private void Window_Initialized(object sender, EventArgs e)
        {
           
        }

        private void btnDebug_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                attDebug.Show();
            }
            catch (Exception ex)
            {
                attDebug = new UC.DebugWindow();
                attDebug.Show();
            }
            
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            attDebug.Close();
        }
    }
}
