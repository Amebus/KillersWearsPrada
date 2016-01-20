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
        const int REFRESH_TIME = 1000;


        //Controller.KinectInterrogator attKinectInterrogator;
        public static GameController attGameController;


        #region Delegates for GameController events
        private delegate void ResumeGameHandler(GameController.ResumeGame.Arguments Parameters);
        private delegate void UnloadGameHandler(GameController.UnloadGame.Arguments Parameters);
        private delegate void UpdateInventorydHandler(GameController.UpdateInventory.Arguments Parameters);

        private ResumeGameHandler attResumeGameHandlerDelegate;
        private UnloadGameHandler attUnloadGameHandlerDelegate;
        private UpdateInventorydHandler attUpdateInventoryDelegate;

        #endregion

        #region User Controls
        private StartingRoom startRoom;
        private Room room;
        private InventoryUC inventory;
        private SelectionDisplay selection_Display;
        string backgroundPath;
        #endregion


        public  string provamw { get { return "ciaone"; } }

        Window attDebug;

        


        public MainWindow()
        {
            Thread.CurrentThread.Name = "KillersWearsPrada-Main Thread";
            Helpers.ResourcesHelper.SaveCurrentDirectory();
            Helpers.ResourcesHelper.ModifyMainBackgroundPath();

            //imposta già tutti i path giusti, di tutte le immagini, forse è da mettere altrove
            modifyAllPath();

            //txtDisplay.AppendText(dir_ok);
            InitializeComponent();

            this.DataContext = this;

            KinectRegion.SetKinectRegion(this, kinectRegion);

            App wvApp = ((App)Application.Current);
            wvApp.KinectRegion = kinectRegion;

            // Use the default sensor
            this.kinectRegion.KinectSensor = KinectSensor.GetDefault();
            attGameController = new GameController(this.kinectRegion.KinectSensor);
            //attKinectInterrogator = new Controller.KinectInterrogator(this.kinectRegion.KinectSensor, REFRESH_TIME);

            attGameController.GetResumeGame.ResumeGameEvent += CaptureResumeGameEvent;
            attGameController.GetUnloadGame.UnloadGameEvent += CaptureUnloadGameEvent;
            attGameController.GetUpdateInventory.UpdateInventoryEvent += CaptureUpdateInventoryEvent;

            attResumeGameHandlerDelegate = new ResumeGameHandler(this.ResumeGame);
            attUnloadGameHandlerDelegate = new UnloadGameHandler(this.UnloadGame);
            attUpdateInventoryDelegate = new UpdateInventorydHandler(this.UpdateInventory);

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            attDebug = new UC.DebugWindow(attGameController);
            attDebug.Show();

            attGameController.StartTakingScreenshots();
        }

        private void CaptureUpdateInventoryEvent(object Sender, GameController.UpdateInventory.Arguments Parameters)
        {
            //object[] wvParameters = new object[] { Parameters };
            //this.Invoke(d, new object[] { e });
            if (this.Dispatcher.CheckAccess())
            {
                //Se siamo su quello dei controlli, chiama il delegato normalmente
                attUpdateInventoryDelegate.Invoke(Parameters);
            }
            else
            {
                //Altrimenti invoca il delegato sul thread corretto
                this.Dispatcher.Invoke(attUpdateInventoryDelegate, Parameters);
            }
        }

        private void CaptureResumeGameEvent(object Sender, GameController.ResumeGame.Arguments Parameters)
        {
            //object[] wvParameters = new object[] { Parameters };
            //this.Invoke(d, new object[] { e });
            if (this.Dispatcher.CheckAccess())
            {
                //Se siamo su quello dei controlli, chiama il delegato normalmente
                attResumeGameHandlerDelegate.Invoke(Parameters);
            }
            else
            {
                //Altrimenti invoca il delegato sul thread corretto
                this.Dispatcher.Invoke(attResumeGameHandlerDelegate, Parameters );
            }
        }

        private void CaptureUnloadGameEvent(object Sender, GameController.UnloadGame.Arguments Parameters)
        {
            //object[] wvParameters = new object[] { Parameters };
            if (this.Dispatcher.CheckAccess())
            {
                attUnloadGameHandlerDelegate.Invoke(Parameters);
            }
            else
            {
                this.Dispatcher.Invoke(attUnloadGameHandlerDelegate, Parameters );
            }
        }

        /// <summary>
        ///  Called by the delegate <see cref="ResumeGameHandler"/> to handle the event <see cref="GameController.ResumeGame"/> 
        /// that occure when the <see cref="Model.Game"/> must loaded
        /// </summary>
        /// <param name="Parameters">Instance of an object repressenting the class <see cref="GameController.ResumeGame.Arguments"/> 
        /// which contains information passed by the event <see cref="Controller.GameController.ResumeGame"/></param>
        private void ResumeGame(GameController.ResumeGame.Arguments Parameters)
        {
            txtDisplay.Text = Thread.CurrentThread.Name + " --- Resume  --------";
            txtDisplay.AppendText("\r\n" + attGameController.Game.GameStarted.ToString());
            attGameController.SetGameStarted();

            // qui faccio allocare tutti gli user control?
            //allocate_All_UC();

            //nome della label di benvenuto
            //name_player.Content = = attGameController.NamePlayer + "!";
        }

        /// <summary>
        /// Called by the delegate <see cref="UnloadGameHandler"/> to handle the event <see cref="GameController.UnloadGame"/> 
        /// that occure when the <see cref="Model.Game"/> must be saved and unloaded
        /// </summary>
        /// <param name="Parameters">Instance of an object repressenting the class <see cref="GameController.UnloadGame.Arguments"/> 
        /// which contains information passed by the event <see cref="GameController.UnloadGame"/></param>
        private void UnloadGame(GameController.UnloadGame.Arguments Parameters)
        {
            txtDisplay.Text = Thread.CurrentThread.Name + " --- Unload";
        }

        private void UpdateInventory(GameController.UpdateInventory.Arguments Parameters)
        {
            throw new NotImplementedException("Qui aggiornare l'inventario");
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
                attDebug = new DebugWindow(attGameController);
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
            // vorrei disallocare questo sfondo ???
            this.Background.Opacity = 0;


            disable_Buttons_Labels();

            startRoom.Visibility = Visibility.Visible;

            GC.Collect();
            GC.WaitForPendingFinalizers();

        }

        private void disable_Buttons_Labels()
        {
            title_game.Visibility = Visibility.Hidden;
            name_player.Visibility = Visibility.Hidden;
            rules.Visibility = Visibility.Hidden;
            Welcome.Visibility = Visibility.Hidden;
            goToEntrance.Visibility = Visibility.Hidden;
            goToEntrance.IsEnabled = false;
            exit.Visibility = Visibility.Hidden;
            exit.IsEnabled = false;

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            attGameController.StopTakingScreenshots();
            attDebug.Close();
        }

        private void close_button(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Do you really want to exit this game?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Application.Current.Windows[0].Close();
            }
        }

        private ImageBrush ib;

        private void ResumeGameFinto(String idUser)
        {
            attGameController.LoadGame("15-01-2016-10-50-42_alberto");
            Helpers.SketchHelper.SetDirectories();
            //Player_Name = attGameController.NamePlayer;
            //name_player.Content = "Player Username" + "!";
            name_player.Content = attGameController.NamePlayer + "!";
            // qui faccio allocare tutti gli user control?
            // con i rispettivi oggetti giusti!!!
            // This method load all usercontrols and put their visibility to hidden
            allocate_All_UC();

            txtDisplay.Visibility = Visibility.Collapsed;
            recognise.IsEnabled = false;
            recognise.Visibility = Visibility.Collapsed;

            //guardo se ha già iniziato a giocare o no
            //quello giusto
            //       if (attGameController.IsGameStarted == false)
            //vai alla welcome home
            //devo bindare lo username...
            bool t = false;
            if (t == false)
            {
                backgroundPath = Application.Current.Resources[E_GenericImages.Welcome_Background.ToString()].ToString();
                ib = new ImageBrush();
                ib.ImageSource = new BitmapImage(new Uri(@backgroundPath, UriKind.Absolute));
                this.Background = ib;
                Enable_Welcome_Obj();
            }
            else
            {
                this.Background.Opacity = 0;
                disable_Buttons_Labels();

                //guardo in che stanza era e carico quella, con le cose giuste...
                loadRoom();
            }
        }

        private void loadRoom()
        {
            string temp = "Livingroom";
            //   switch (attGameController.ActualRoom.Name)
            switch (temp)
            {
                case "StartingRoom":
                    {
                        startRoom.Visibility = Visibility.Visible;
                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                    }
                    break;
                case "Kitchen":
                    {
                        room.Visibility = Visibility.Visible;
                        room.Kitchen_Image.Visibility = Visibility;
                        Room.setBackgroundCanvas(Application.Current.Resources[E_RoomsImages.Kitchen_Image.ToString()].ToString());

                        //qui devo abilitare solo i giusti bottoni...
                        Room.change_KitchenButtons_Status(true);
                        Room.change_CommonButtons_Status(true);
                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                    }
                    break;
                case "Livingroom":
                    {
                        room.Visibility = Visibility.Visible;
                        room.Livingroom_Image.Visibility = Visibility;
                        Room.setBackgroundCanvas(Application.Current.Resources[E_RoomsImages.Livingroom_Image.ToString()].ToString());

                        Room.change_LivingroomButtons_Status(true);
                        Room.change_CommonButtons_Status(true);

                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                    }
                    break;
                default:
                    {
                        room.Visibility = Visibility.Visible;
                        room.Bedroom_Image.Visibility = Visibility;
                        Room.setBackgroundCanvas(Application.Current.Resources[E_RoomsImages.Bedroom_Image.ToString()].ToString());
                        Room.change_BedroomButtons_Status(true);
                        Room.change_CommonButtons_Status(true);
                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                    }
                    break;
            }
        }

        private void homepage(object sender, RoutedEventArgs e)
        {

         //   attGameController.GetResumeGame.RaiseEvent();
            ResumeGameFinto("ciao");  
            /*
            
            backgroundPath = Application.Current.Resources[E_GenericImages.Welcome_Background.ToString()].ToString();
            ib = new ImageBrush();
            ib.ImageSource = new BitmapImage(new Uri(@backgroundPath, UriKind.Absolute));
            this.Background = ib;

            
            txtDisplay.Visibility = Visibility.Collapsed;
            recognise.IsEnabled = false;
            recognise.Visibility = Visibility.Collapsed;

            Enable_Welcome_Obj();
            */
        }

        private void Enable_Welcome_Obj()
        {
            name_player.Visibility = Visibility.Visible;
            rules.Visibility = Visibility.Visible;
            Welcome.Visibility = Visibility.Visible;
            goToEntrance.Visibility = Visibility.Visible;
            goToEntrance.IsEnabled = true;

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }


        private void modifyAllPath() {
            // path delle stanze
            Helpers.ResourcesHelper.ModifyRoomBackgroundPath(E_RoomsImages.Doors_Image);
            Helpers.ResourcesHelper.ModifyRoomBackgroundPath(E_RoomsImages.Livingroom_Image);
            Helpers.ResourcesHelper.ModifyRoomBackgroundPath(E_RoomsImages.Kitchen_Image);
            Helpers.ResourcesHelper.ModifyRoomBackgroundPath(E_RoomsImages.Bedroom_Image);

            // background welcome e inventary e selection display
            Helpers.ResourcesHelper.ModifyGenericImagesPath(E_GenericImages.Inventory_Background);
            Helpers.ResourcesHelper.ModifyGenericImagesPath(E_GenericImages.Welcome_Background);
            Helpers.ResourcesHelper.ModifyGenericImagesPath(E_GenericImages.Selection_Crime);
            Helpers.ResourcesHelper.ModifyGenericImagesPath(E_GenericImages.Selection_Background);
            Helpers.ResourcesHelper.ModifyGenericImagesPath(E_GenericImages.Trash_Empty);

            //path delle immagini delle porte
            Helpers.ResourcesHelper.ModifyDoorsPath(E_DoorsImages.SXdoor_Image);
            Helpers.ResourcesHelper.ModifyDoorsPath(E_DoorsImages.CENTERdoor_Image);
            Helpers.ResourcesHelper.ModifyDoorsPath(E_DoorsImages.DXdoor_Image);
            Helpers.ResourcesHelper.ModifyDoorsPath(E_DoorsImages.SXdoorDisabled_Image);
            Helpers.ResourcesHelper.ModifyDoorsPath(E_DoorsImages.CENTERdoorDisabled_Image);
            Helpers.ResourcesHelper.ModifyDoorsPath(E_DoorsImages.DXdoorDisabled_Image);

            //immagini della welcome page
            Helpers.ResourcesHelper.ModifyGenericImagesPath(E_GenericImages.Start_Image);
            Helpers.ResourcesHelper.ModifyGenericImagesPath(E_GenericImages.StartOver_Image);
            Helpers.ResourcesHelper.ModifyGenericImagesPath(E_GenericImages.StartPressed_Image);
            Helpers.ResourcesHelper.ModifyGenericImagesPath(E_GenericImages.Welcome_Image);

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
            room = new Room();
            
            // se uso hidden al posto di collapsed carica prima!
            startRoom.Visibility = Visibility.Collapsed;
            room.Visibility = Visibility.Collapsed;

            // aggiungo tutti gli usercontrol come figli della mainGrid
            mainGrid.Children.Add(startRoom);
            mainGrid.Children.Add(room);

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        private void EnterKeyCommand(object sender, EventArgs e)
        {
            ((TextBox)sender).SelectionLength = 0;
        }

        private void prova_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
