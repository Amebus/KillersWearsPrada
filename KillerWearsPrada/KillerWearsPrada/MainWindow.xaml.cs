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
using KillerWearsPrada.UC;

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

            //imposta già tutti i path giusti, di tutte le immagini, forse è da mettere altrove
            modifyAllPath();

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

        //Dovrebbe passare alla user control StartingRoom ma non lo fa e non so perchè
        private void btnEntrance_Click(object sender, RoutedEventArgs e)
        {
            StartingRoom startRoom = new StartingRoom();

            mainGrid.Children.Add(startRoom);

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            attDebug.Close();
        }

        private void close_button(object sender, RoutedEventArgs e)
        {

           this.Close();
        }

        private void modifyAllPath() {
            Helpers.ResourcesHelper.ModifyRoomBackgroundPath("Doors_Image");
            Helpers.ResourcesHelper.ModifyRoomBackgroundPath("Livingroom_Image");
            Helpers.ResourcesHelper.ModifyRoomBackgroundPath("Kitchen_Image");
            Helpers.ResourcesHelper.ModifyRoomBackgroundPath("Bedroom_Image");

            //path delle immagini delle porte
            Helpers.ResourcesHelper.ModifyRoomBackgroundPath("SXdoor_Image");
            Helpers.ResourcesHelper.ModifyRoomBackgroundPath("CENTERdoor_Image");
            Helpers.ResourcesHelper.ModifyRoomBackgroundPath("DXdoor_Image");
        }
        

    
    }
}
