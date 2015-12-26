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
using System.Threading;

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
        #region Delegates for GameController events
        private delegate void ResumeGameHandler(Controller.GameController.ResumeGame.Args Parameters);
        private delegate void UnloadGameHandler(Controller.GameController.UnloadGame.Args Parameters);

        private ResumeGameHandler attResumeGameHandlerDelegate;
        private UnloadGameHandler attUnloadGameHandlerDelegate;
        #endregion

        Window attDebug;


        public MainWindow()
        {
            Thread.CurrentThread.Name = "KillersWearsPrada-Main Thread";
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
            attGameController.GetResumeGame.RaiseResumeGame += CaptureResumeGameEvent;
            attGameController.GetUnloadGame.RaiseUnloadGame += CaptureUnloadGameEvent;

            attResumeGameHandlerDelegate = new ResumeGameHandler(this.ResumeGame);
            attUnloadGameHandlerDelegate = new UnloadGameHandler(this.UnloadGame);

        }

        private void CaptureResumeGameEvent(object Sender, Controller.GameController.ResumeGame.Args Parameters)
        {
            object[] wvParameters = new object[] { Parameters };
            //this.Invoke(d, new object[] { e });
            if (this.Dispatcher.CheckAccess())
            {
                //Se siamo su quello dei controlli, chiama il delegato normalmente
                attResumeGameHandlerDelegate.Invoke(Parameters);
            }
            else
            {
                //Altrimenti invoca il delegato sul thread corretto
                this.Dispatcher.Invoke(attResumeGameHandlerDelegate, new object[] { wvParameters });
            }
        }

        private void CaptureUnloadGameEvent(object Sender, Controller.GameController.UnloadGame.Args Parameters)
        {
            object[] wvParameters = new object[] { Parameters };
            if (this.Dispatcher.CheckAccess())
            {
                attUnloadGameHandlerDelegate.Invoke(Parameters);
            }
            else
            {
                this.Dispatcher.Invoke(attUnloadGameHandlerDelegate, new object[] { wvParameters });
            }
        }

        /// <summary>
        ///  Called by the delegate <see cref="ResumeGameHandler"/> to handle the event <see cref="Controller.GameController.ResumeGame"/> 
        /// that occure when the <see cref="Model.Game"/> must loaded
        /// </summary>
        /// <param name="Parameters">Instance of an object repressenting the class <see cref="Controller.GameController.ResumeGame.Args"/> 
        /// which contains information passed by the event <see cref="Controller.GameController.ResumeGame"/></param>
        private void ResumeGame (Controller.GameController.ResumeGame.Args Parameters)
        {
            txtDisplay.Text = Thread.CurrentThread.Name + " --- Resume";
        }

        /// <summary>
        /// Called by the delegate <see cref="UnloadGameHandler"/> to handle the event <see cref="Controller.GameController.UnloadGame"/> 
        /// that occure when the <see cref="Model.Game"/> must be saved and unloaded
        /// </summary>
        /// <param name="Parameters">Instance of an object repressenting the class <see cref="Controller.GameController.UnloadGame.Args"/> 
        /// which contains information passed by the event <see cref="Controller.GameController.UnloadGame"/></param>
        private void UnloadGame(Controller.GameController.UnloadGame.Args Parameters)
        {
            txtDisplay.Text = Thread.CurrentThread.Name + " --- Unload";
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

        

    
    }
}
