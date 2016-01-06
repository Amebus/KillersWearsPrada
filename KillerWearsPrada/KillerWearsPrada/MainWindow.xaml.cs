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
using KillerWearsPrada.Controller;
using System.Threading;
using static KillerWearsPrada.Helpers.ResourcesHelper;

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
        private delegate void ResumeGameHandler(GameController.ResumeGame.Args Parameters);
        private delegate void UnloadGameHandler(GameController.UnloadGame.Args Parameters);

        private ResumeGameHandler attResumeGameHandlerDelegate;
        private UnloadGameHandler attUnloadGameHandlerDelegate;
        #endregion

        #region User Controls
        private StartingRoom startRoom;
        private Room room;
        private InventoryUC inventory;
        private SelectionDisplay selection_Display;
        #endregion

        Window attDebug;


        public MainWindow()
        {
            Thread.CurrentThread.Name = "KillersWearsPrada-Main Thread";
            Helpers.ResourcesHelper.SaveCurrentDirectory();
            Helpers.ResourcesHelper.ModifyMainBackgroundPath();

            //txtDisplay.AppendText(dir_ok);
            InitializeComponent();

            //imposta già tutti i path giusti, di tutte le immagini, forse è da mettere altrove
            modifyAllPath();

            // This method load all usercontrols and put their visibility to hidden
            allocate_All_UC();

            KinectRegion.SetKinectRegion(this, kinectRegion);

            App wvApp = ((App)Application.Current);
            wvApp.KinectRegion = kinectRegion;

            // Use the default sensor
            this.kinectRegion.KinectSensor = KinectSensor.GetDefault();
            attGameController = new GameController(this.kinectRegion.KinectSensor);
            //attKinectInterrogator = new Controller.KinectInterrogator(this.kinectRegion.KinectSensor, REFRESH_TIME);

            attGameController.GetResumeGame.RaiseResumeGame += CaptureResumeGameEvent;
            attGameController.GetUnloadGame.RaiseUnloadGame += CaptureUnloadGameEvent;

            attResumeGameHandlerDelegate = new ResumeGameHandler(this.ResumeGame);
            attUnloadGameHandlerDelegate = new UnloadGameHandler(this.UnloadGame);
        }
        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            attDebug = new UC.DebugWindow();
            attDebug.Show();

            //attGameController.StartTakingScreenShot()
        }

        private void CaptureResumeGameEvent(object Sender, GameController.ResumeGame.Args Parameters)
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

        private void CaptureUnloadGameEvent(object Sender, GameController.UnloadGame.Args Parameters)
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
        ///  Called by the delegate <see cref="ResumeGameHandler"/> to handle the event <see cref="GameController.ResumeGame"/> 
        /// that occure when the <see cref="Model.Game"/> must loaded
        /// </summary>
        /// <param name="Parameters">Instance of an object repressenting the class <see cref="GameController.ResumeGame.Args"/> 
        /// which contains information passed by the event <see cref="Controller.GameController.ResumeGame"/></param>
        private void ResumeGame(GameController.ResumeGame.Args Parameters)
        {
            txtDisplay.Text = Thread.CurrentThread.Name + " --- Resume";
        }

        /// <summary>
        /// Called by the delegate <see cref="UnloadGameHandler"/> to handle the event <see cref="GameController.UnloadGame"/> 
        /// that occure when the <see cref="Model.Game"/> must be saved and unloaded
        /// </summary>
        /// <param name="Parameters">Instance of an object repressenting the class <see cref="GameController.UnloadGame.Args"/> 
        /// which contains information passed by the event <see cref="GameController.UnloadGame"/></param>
        private void UnloadGame(GameController.UnloadGame.Args Parameters)
        {
            txtDisplay.Text = Thread.CurrentThread.Name + " --- Unload";
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
            catch (Exception)
            {
                attDebug = new DebugWindow();
                attDebug.Show();
            }
            
        }

        //Dovrebbe passare alla user control StartingRoom ma non lo fa e non so perchè
        private void btnEntrance_Click(object sender, RoutedEventArgs e)
        {
            // modificare
            //   startRoom = new StartingRoom();

            //   mainGrid.Children.Add(startRoom);

            //  mainGrid.Background.Opacity = 0;
            this.Background.Opacity = 0;

            startRoom.Visibility = Visibility.Visible;

            GC.Collect();
            GC.WaitForPendingFinalizers();

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            attGameController.StopTakingScreenShot();
            attDebug.Close();
        }

        private void close_button(object sender, RoutedEventArgs e)
        {

           this.Close();
        }

        private void modifyAllPath() {
            // path delle stanze
            Helpers.ResourcesHelper.ModifyRoomBackgroundPath(E_RoomsImages.Doors_Image);
            Helpers.ResourcesHelper.ModifyRoomBackgroundPath(E_RoomsImages.Livingroom_Image);
            Helpers.ResourcesHelper.ModifyRoomBackgroundPath(E_RoomsImages.Kitchen_Image);
            Helpers.ResourcesHelper.ModifyRoomBackgroundPath(E_RoomsImages.Bedroom_Image);

            Helpers.ResourcesHelper.ModifyGenericImagesPath(E_GenericImages.Inventory_Background);

            //path delle immagini delle porte
            Helpers.ResourcesHelper.ModifyDoorsPath(E_DoorsImages.SXdoor_Image);
            Helpers.ResourcesHelper.ModifyDoorsPath(E_DoorsImages.CENTERdoor_Image);
            Helpers.ResourcesHelper.ModifyDoorsPath(E_DoorsImages.DXdoor_Image);
        }

        #region getter of user controls
        public StartingRoom StartRoom
        {
            get { return startRoom; }
        }

        public Room Room
        {
            get { return room; }
        }

        public InventoryUC Inventory
        {
            get { return inventory; }
        }

        public SelectionDisplay Get_SelectionDisplay
        {
            get { return selection_Display; }
        }
        #endregion

        private void allocate_All_UC()
        {
            startRoom = new StartingRoom();

            // ma qui cosa imposto???
            room = new Room();

            //qui ci vorrebbe lo user control di provenienza?
            inventory = new InventoryUC();

            //qui l'id della maglietta di provenienza? non credo più...
            selection_Display = new SelectionDisplay();
            

            // se uso hidden al posto di collapsed carica prima!
            startRoom.Visibility = Visibility.Hidden;
            room.Visibility = Visibility.Hidden;
            inventory.Visibility = Visibility.Hidden;
            selection_Display.Visibility = Visibility.Hidden;

            // aggiungo tutti gli usercontrol come figli della mainGrid
            mainGrid.Children.Add(startRoom);
            mainGrid.Children.Add(room);
            mainGrid.Children.Add(inventory);
            mainGrid.Children.Add(selection_Display);
            
        }
        

    
    }
}
