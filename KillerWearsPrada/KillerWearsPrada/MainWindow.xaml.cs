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
        // const int REFRESH_TIME = 1000;


        //Controller.KinectInterrogator attKinectInterrogator;
        public static GameController attGameController;


        #region Delegates for GameController events
        private delegate void ResumeGameHandler(GameController.ResumeGame.Arguments Parameters);
        private delegate void UnloadGameHandler(GameController.UnloadGame.Arguments Parameters);
        private delegate void UpdateInventorydHandler(GameController.UpdateInventory.Arguments Parameters);
        private delegate void NotifyItemExceptionHandler(GameController.NotifyItemException.Arguments Parameters);

        private ResumeGameHandler attResumeGameHandlerDelegate;
        private UnloadGameHandler attUnloadGameHandlerDelegate;
        private UpdateInventorydHandler attUpdateInventoryDelegate;
        private NotifyItemExceptionHandler attNotifyItemExceptionDelegate;

        #endregion

        #region User Controls
        private StartingRoom startRoom;
        private Room room;

        //perchè mi da questo errore???!
        string backgroundPath;
        #endregion

        //Background of room
        private ImageBrush ib;

        Window attDebug;
        
        public MainWindow()
        {
            Thread.CurrentThread.Name = "KillersWearsPrada-Main Thread";
            Helpers.ResourcesHelper.SaveCurrentDirectory();
            Helpers.ResourcesHelper.ModifyMainBackgroundPath();
            
            modifyAllPath();
            Helpers.SketchHelper.SetDirectories();
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
            attGameController.GetNotifyItemException.NotifyItemExceptionEvent += CaptureNotifyItemException;

            attResumeGameHandlerDelegate = new ResumeGameHandler(this.ResumeGame);
            attUnloadGameHandlerDelegate = new UnloadGameHandler(this.UnloadGame);
            attUpdateInventoryDelegate = new UpdateInventorydHandler(this.UpdateInventory);
            attNotifyItemExceptionDelegate = new NotifyItemExceptionHandler(this.NotifyItemException);
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            attDebug = new UC.DebugWindow(attGameController);
            //attDebug.Show();

            attGameController.StartTakingScreenshots();

           //recognise.Visibility = Visibility.Visible;
        }

        private void CaptureNotifyItemException(object sender, GameController.NotifyItemException.Arguments Parameters)
         {
             if (this.Dispatcher.CheckAccess())
             {
                 //Se siamo su quello dei controlli, chiama il delegato normalmente
                 attNotifyItemExceptionDelegate.Invoke(Parameters);
             }
             else
             {
                 //Altrimenti invoca il delegato sul thread corretto
                 this.Dispatcher.Invoke(attNotifyItemExceptionDelegate, Parameters);
            }
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
                this.Dispatcher.Invoke(attResumeGameHandlerDelegate, Parameters);
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
                this.Dispatcher.Invoke(attUnloadGameHandlerDelegate, Parameters);
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
            Delete_Sketches_Files();
            txtDisplay.IsEnabled = true;
            txtDisplay.Visibility = Visibility.Visible;
            txtDisplay.Text = Thread.CurrentThread.Name + " --- Resume  --------";
            txtDisplay.AppendText("\r\n" + attGameController.Game.IsGameStarted.ToString());

            ResumeGameMethod();
            
        }

        /// <summary>
        /// Called by the delegate <see cref="UnloadGameHandler"/> to handle the event <see cref="GameController.UnloadGame"/> 
        /// that occure when the <see cref="Model.Game"/> must be saved and unloaded
        /// </summary>
        /// <param name="Parameters">Instance of an object repressenting the class <see cref="GameController.UnloadGame.Arguments"/> 
        /// which contains information passed by the event <see cref="GameController.UnloadGame"/></param>
        private void UnloadGame(GameController.UnloadGame.Arguments Parameters)
        {
            txtDisplay.IsEnabled = true;
          //  txtDisplay.Visibility = Visibility.Visible;
            txtDisplay.Text = Thread.CurrentThread.Name + " --- Unload";

            disable_Buttons_Labels();

            title_game.Visibility = Visibility.Visible;

            ib = null;
            backgroundPath = Application.Current.Resources[E_GenericImages.Application_Start_Image.ToString()].ToString();
            ib = new ImageBrush();
            ib.ImageSource = new BitmapImage(new Uri(@backgroundPath, UriKind.Absolute));
            this.Background = ib;

            mainGrid.Children.Remove(startRoom);
            mainGrid.Children.Remove(room);
 
            startRoom = null;
            room = null;

            GC.Collect();
            GC.WaitForPendingFinalizers();

            this.Background.Opacity = 1;
            
            Delete_Sketches_Files();

            System.Windows.Forms.Application.Restart();
            Thread.Sleep(2000);
            Application.Current.Shutdown();
        }

        /// <summary>
        /// Called by the delegate <see cref="UpdateInventorydHandler"/> to handle the event <see cref="GameController.UpdateInventory"/> 
        /// that occure when an item is added to the inventory
        /// </summary>
        /// <param name="Parameters">Instance of an object repressenting the class <see cref="GameController.UpdateInventory.Arguments"/> 
        /// which contains information passed by the event <see cref="GameController.UpdateInventory"/></param>
        private void UpdateInventory(GameController.UpdateInventory.Arguments Parameters)
        {
            string cosamostrare = "Item added in Inventory";
            bool wasActualRoom = false;
            foreach (Model.Room r in attGameController.Game.Rooms)
            {
                foreach (Model.Item item in r.Items)
                    if (item.BarCode == Parameters.BarCode)
                    {
                        //Find the button to animate
                        foreach (Button b in room.listOfButtons)
                        {
                            if (b.Tag.ToString() == Parameters.BarCode && attGameController.Game.ActualRoom.Name == r.Name)
                            {
                                //animation!!!
                                VisualStateManager.GoToState(b, "Moving", true);
                                wasActualRoom = true;
                                break;
                            }

                        }

                        //if the animation cannot be performed, a popup is shown
                        if (wasActualRoom == false)
                        {
                            cosamostrare = "Item " + item.ItemName + " added in Inventory";
                            Popup lastc = new Popup(cosamostrare);
                            mainGrid.Children.Add(lastc);
                            lastc.Focus();
                        }

                    }
                if (r.IsRoomCompleted == true && r.IsLastClueAlreadyShown == false)
                {
                    cosamostrare = r.LastClue;
                    Popup lastc = new Popup(cosamostrare);
                    mainGrid.Children.Add(lastc);
                    lastc.Focus();
                    changeDoorColor();
                }
            }
        }

        private void NotifyItemException(GameController.NotifyItemException.Arguments Parameters)
        {
            Popup it = new Popup(Parameters.Messagge);
            mainGrid.Children.Add(it);
            it.Focus();
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

        /// <summary>
        /// Go to the <see cref="StartingRoom"/>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEntrance_Click(object sender, RoutedEventArgs e)
        {
            attGameController.SetGameStarted();
            this.Background.Opacity = 0;
            
            disable_Buttons_Labels();
            startRoom.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// 
        /// </summary>
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
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            attGameController.StopTakingScreenshots();
            attDebug.Close();
        }

        #region da eliminare quando tolgo bottone exit
        private void close_button(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Do you really want to exit this game?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Application.Current.Windows[0].Close();
            }
        }
        #endregion

        /// <summary>
        /// Resume the game from where it was unload, or a new game if there is a new user
        /// </summary>
        private void ResumeGameMethod()
        {
            #region da eliminare quando non devo più giocare senza kinect
            //      attGameController.LoadGame("26-01-2016-11-40-50_Giocatore1");
            //   attGameController.LoadGame("26-01-2016-11-40-50_Giocatore1conitemininventory");
         //   attGameController.LoadGame("10-02-2016-10-08-23_Anacleto");
            #endregion

            name_player.Content = attGameController.Game.PlayerName + "!";
           
            allocate_All_UC();

            #region da eliminare dopo aver rimosso txtdisplay e bottone Giocatore riconosciuto
            txtDisplay.Visibility = Visibility.Collapsed;
            recognise.IsEnabled = false;
            recognise.Visibility = Visibility.Collapsed;
            #endregion

            //Check if she is a new player of if she's already started playing
            if (attGameController.Game.IsGameStarted == false)
            {
                //New player
                //Load the Welcome Page
                backgroundPath = Application.Current.Resources[E_GenericImages.Welcome_Background.ToString()].ToString();
                ib = new ImageBrush();
                ib.ImageSource = new BitmapImage(new Uri(@backgroundPath, UriKind.Absolute));
                this.Background = ib;
                Enable_Welcome_Obj();
            }
            else
            {
                //Old player
                this.Background.Opacity = 0;
                disable_Buttons_Labels();
                
                loadRoom();
            }
        }

        /// <summary>
        /// Resume the last room in which the player was before left the game
        /// </summary>
        private void loadRoom()
        { 
            switch (attGameController.Game.ActualRoom.Name)
            {
                case Model.E_RoomsName.START_ROOM:
                    {
                        startRoom.Visibility = Visibility.Visible;
                        StartRoom.change_Buttons_Status(true);
                        changeDoorColor();
                    }
                    break;
                case Model.E_RoomsName.KITCHEN:
                    {
                        room.Visibility = Visibility.Visible;
                        room.Kitchen_Image.Visibility = Visibility;
                        Room.setBackgroundCanvas(Application.Current.Resources[E_RoomsImages.Kitchen_Image.ToString()].ToString());

                        //Enable buttons in room
                        Room.change_KitchenButtons_Status(true);
                        Room.change_CommonButtons_Status(true);
                    }
                    break;
                case Model.E_RoomsName.LIVINGROOM:
                    {
                        room.Visibility = Visibility.Visible;
                        room.Livingroom_Image.Visibility = Visibility;
                        Room.setBackgroundCanvas(Application.Current.Resources[E_RoomsImages.Livingroom_Image.ToString()].ToString());

                        //Enable buttons in room
                        Room.change_LivingroomButtons_Status(true);
                        Room.change_CommonButtons_Status(true);
                        
                    }
                    break;
                default:
                    {
                        room.Visibility = Visibility.Visible;
                        room.Bedroom_Image.Visibility = Visibility;
                        Room.setBackgroundCanvas(Application.Current.Resources[E_RoomsImages.Bedroom_Image.ToString()].ToString());

                        //Enable buttons in room
                        Room.change_BedroomButtons_Status(true);
                        Room.change_CommonButtons_Status(true);
                    }
                    break;
            }
        }


        #region da eliminare quando tolgo bottone "giocatore riconosciuto
        private void homepage(object sender, RoutedEventArgs e)
        {
            ResumeGameMethod();
        }
        #endregion

        /// <summary>
        /// Set all welcome objects to visible
        /// </summary>
        private void Enable_Welcome_Obj()
        {
            name_player.Visibility = Visibility.Visible;
            rules.Visibility = Visibility.Visible;
            Welcome.Visibility = Visibility.Visible;
            goToEntrance.Visibility = Visibility.Visible;
            goToEntrance.IsEnabled = true;
            
        }

        /// <summary>
        /// Set the resource images to their absolute path
        /// </summary>
        private void modifyAllPath()
        {
            //Background rooms paths
            Helpers.ResourcesHelper.ModifyRoomBackgroundPath(E_RoomsImages.Doors_Image);
            Helpers.ResourcesHelper.ModifyRoomBackgroundPath(E_RoomsImages.Livingroom_Image);
            Helpers.ResourcesHelper.ModifyRoomBackgroundPath(E_RoomsImages.Kitchen_Image);
            Helpers.ResourcesHelper.ModifyRoomBackgroundPath(E_RoomsImages.Bedroom_Image);

            // background images of welcome page, inventary and selection display
            Helpers.ResourcesHelper.ModifyGenericImagesPath(E_GenericImages.Inventory_Background);
            Helpers.ResourcesHelper.ModifyGenericImagesPath(E_GenericImages.Welcome_Background);
            Helpers.ResourcesHelper.ModifyGenericImagesPath(E_GenericImages.Selection_Crime);
            Helpers.ResourcesHelper.ModifyGenericImagesPath(E_GenericImages.Selection_Background);
            Helpers.ResourcesHelper.ModifyGenericImagesPath(E_GenericImages.Trash_Empty);
            Helpers.ResourcesHelper.ModifyGenericImagesPath(E_GenericImages.Trash_Full);
            Helpers.ResourcesHelper.ModifyGenericImagesPath(E_GenericImages.Fumetto);

            //images paths of door
            Helpers.ResourcesHelper.ModifyDoorsPath(E_DoorsImages.SXdoor_Image);
            Helpers.ResourcesHelper.ModifyDoorsPath(E_DoorsImages.CENTERdoor_Image);
            Helpers.ResourcesHelper.ModifyDoorsPath(E_DoorsImages.DXdoor_Image);
            Helpers.ResourcesHelper.ModifyDoorsPath(E_DoorsImages.SXdoorDisabled_Image);
            Helpers.ResourcesHelper.ModifyDoorsPath(E_DoorsImages.CENTERdoorDisabled_Image);
            Helpers.ResourcesHelper.ModifyDoorsPath(E_DoorsImages.DXdoorDisabled_Image);

            //Images of welcome page
            Helpers.ResourcesHelper.ModifyGenericImagesPath(E_GenericImages.Start_Image);
            Helpers.ResourcesHelper.ModifyGenericImagesPath(E_GenericImages.StartOver_Image);
            Helpers.ResourcesHelper.ModifyGenericImagesPath(E_GenericImages.StartPressed_Image);
            Helpers.ResourcesHelper.ModifyGenericImagesPath(E_GenericImages.Welcome_Image);
            Helpers.ResourcesHelper.ModifyGenericImagesPath(E_GenericImages.Sagoma);
            Helpers.ResourcesHelper.ModifyGenericImagesPath(E_GenericImages.Pergamena);
            Helpers.ResourcesHelper.ModifyGenericImagesPath(E_GenericImages.TrashBackground);
            Helpers.ResourcesHelper.ModifyGenericImagesPath(E_GenericImages.FireworksBackground);

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
        #endregion

        /// <summary>
        /// Load all usercontrols and put their visibility to hidden
        /// </summary>
        private void allocate_All_UC()
        {
            startRoom = new StartingRoom();
            room = new Room();
            
            // Add usercontrol as sons of MainWindow
            mainGrid.Children.Add(startRoom);
            mainGrid.Children.Add(room);
            startRoom.Visibility = Visibility.Hidden;
            room.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Change the colors of the doors in StartingRoom according to the state of each room
        /// </summary>
        public void changeDoorColor()
        {
            foreach (Model.Room r in MainWindow.attGameController.Game.Rooms)
            {
                switch (r.Name)
                {
                    case Model.E_RoomsName.BEDROOM:
                        {
                            if (r.IsRoomCompleted == true)
                            {
                                VisualStateManager.GoToState(StartRoom.dxDoorButton, "RoomCompleted", false);
                            }
                            else if (r.DisclosedItemsClues.Count() > 0)
                            {
                                VisualStateManager.GoToState(StartRoom.dxDoorButton, "Started", false);
                            }
                        }
                        break;
                    case Model.E_RoomsName.KITCHEN:
                        {
                            if (r.IsRoomCompleted == true)
                            {
                                VisualStateManager.GoToState(StartRoom.centerDoorButton, "RoomCompleted", false);
                            }
                            else if (r.DisclosedItemsClues.Count() > 0)
                            {
                                VisualStateManager.GoToState(StartRoom.centerDoorButton, "Started", false);
                            }
                        }
                        break;
                    case Model.E_RoomsName.LIVINGROOM:
                        {
                            if (r.IsRoomCompleted == true)
                            {
                                VisualStateManager.GoToState(StartRoom.sxDoorButton, "RoomCompleted", false);
                            }
                            else if(r.DisclosedItemsClues.Count() >0)
                            {
                                VisualStateManager.GoToState(StartRoom.sxDoorButton, "Started", false);
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Deletes all the sketches files in directory Sketches
        /// </summary>
        private void Delete_Sketches_Files()
        {
            //delete all sketches files
            System.IO.DirectoryInfo di = new DirectoryInfo(SketchesPathsFile());

            foreach (FileInfo file in di.GetFiles())
            {
                try
                {
                    File.Delete(file.FullName);
                    //file.Delete();
                }
                catch { }
            }
            /*
            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                try
                {
                    dir.Delete(true);
                }
                catch
                {

                }

            }*/
        }

        /// <summary>
        /// Disable the selection in TextBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EnterKeyCommand(object sender, EventArgs e)
        {
            ((TextBox)sender).SelectionLength = 0;
        }
    }
}
