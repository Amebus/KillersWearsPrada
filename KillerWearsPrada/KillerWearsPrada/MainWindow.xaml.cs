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


        public string provamw { get { return "ciaone"; } }

        Window attDebug;




        public MainWindow()
        {
            Thread.CurrentThread.Name = "KillersWearsPrada-Main Thread";
            Helpers.ResourcesHelper.SaveCurrentDirectory();
            Helpers.ResourcesHelper.ModifyMainBackgroundPath();

            //imposta già tutti i path giusti, di tutte le immagini, forse è da mettere altrove
            modifyAllPath();
            Helpers.SketchHelper.SetDirectories();
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

            ResumeGameFinto();

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
            //devo eliminare tutte le immagini nella directory degli sketches TODO
            txtDisplay.IsEnabled = true;
            txtDisplay.Visibility = Visibility.Visible;
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
            //  backgroundPath = Application.Current.Resources[E_GenericImages.Application_Start_Image.ToString()].ToString();
            this.Background.Opacity = 1;

            Delete_Sketches_Files();
        }

        private void UpdateInventory(GameController.UpdateInventory.Arguments Parameters)
        {
            //se la ricerca del tizio è completato
            string cosamostrare = "Item added in Inventory";
            // attGameController.Game.GetItem()
            //  attGameController.Game.Rooms[]
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
                            cosamostrare = "Item " + item.ItemName + " added in Inventory\r\n\r\n";
                            Popup lastc = new Popup(cosamostrare);
                            mainGrid.Children.Add(lastc);
                            lastc.Focus();
                        }

                    }
                if (r.IsRoomCompleted == true && r.IsLastClueAlreadyShown == false)
                {
                    cosamostrare += r.LastClue;
                    Popup lastc = new Popup(cosamostrare);
                    mainGrid.Children.Add(lastc);
                    lastc.Focus();
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

        //Dovrebbe passare alla user control StartingRoom ma non lo fa e non so perchè
        private void btnEntrance_Click(object sender, RoutedEventArgs e)
        {

            attGameController.SetGameStarted();
            this.Background.Opacity = 0;
            
            disable_Buttons_Labels();
            startRoom.Visibility = Visibility.Visible;
            
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

        private void ResumeGameFinto()
        {
         //     attGameController.LoadGame("26-01-2016-11-40-50_Giocatore1");
            attGameController.LoadGame("26-01-2016-11-40-50_Giocatore1conitemininventory");

            //Player_Name = attGameController.NamePlayer;
            //name_player.Content = "Player Username" + "!";
            name_player.Content = attGameController.Game.PlayerName + "!";
            // qui faccio allocare tutti gli user control?
            // con i rispettivi oggetti giusti!!!
            // This method load all usercontrols and put their visibility to hidden
            allocate_All_UC();

            txtDisplay.Visibility = Visibility.Collapsed;
            recognise.IsEnabled = false;
            recognise.Visibility = Visibility.Collapsed;

            //guardo se ha già iniziato a giocare o no
            //quello giusto
            if (attGameController.Game.IsGameStarted == false)
            //vai alla welcome home
            //devo bindare lo username...
            //  bool t = false;
            //  if (t == false)
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
            //   string temp = "Livingroom";
            switch (attGameController.Game.ActualRoom.Name)
            //  switch (temp)
            {
                case Model.E_RoomsName.START_ROOM:
                    {
                        /*    StartRoom.change_Buttons_Status(true);
                            changeDoorColor();*/
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

                        //qui devo abilitare solo i giusti bottoni...
                        Room.change_KitchenButtons_Status(true);
                        Room.change_CommonButtons_Status(true);
                    }
                    break;
                case Model.E_RoomsName.LIVINGROOM:
                    {
                        room.Visibility = Visibility.Visible;
                        room.Livingroom_Image.Visibility = Visibility;
                        Room.setBackgroundCanvas(Application.Current.Resources[E_RoomsImages.Livingroom_Image.ToString()].ToString());

                        Room.change_LivingroomButtons_Status(true);
                        Room.change_CommonButtons_Status(true);
                        
                    }
                    break;
                default:
                    {
                        room.Visibility = Visibility.Visible;
                        room.Bedroom_Image.Visibility = Visibility;
                        Room.setBackgroundCanvas(Application.Current.Resources[E_RoomsImages.Bedroom_Image.ToString()].ToString());
                        Room.change_BedroomButtons_Status(true);
                        Room.change_CommonButtons_Status(true);
                    }
                    break;
            }
        }

        private void homepage(object sender, RoutedEventArgs e)
        {

            //   attGameController.GetResumeGame.RaiseEvent();
            ResumeGameFinto();
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
            
        }


        private void modifyAllPath()
        {
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
            Helpers.ResourcesHelper.ModifyGenericImagesPath(E_GenericImages.Trash_Full);
            Helpers.ResourcesHelper.ModifyGenericImagesPath(E_GenericImages.Fumetto);

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
            Helpers.ResourcesHelper.ModifyGenericImagesPath(E_GenericImages.Sagoma);
            Helpers.ResourcesHelper.ModifyGenericImagesPath(E_GenericImages.Pergamena);


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

        private void allocate_All_UC()
        {

            startRoom = new StartingRoom();
            room = new Room();
            //   changeDoorColor();
            // se uso hidden al posto di collapsed carica prima!


            // aggiungo tutti gli usercontrol come figli della mainGrid
            mainGrid.Children.Add(startRoom);
            mainGrid.Children.Add(room);
            startRoom.Visibility = Visibility.Hidden;
            room.Visibility = Visibility.Hidden;



            /*       GC.Collect();
                   GC.WaitForPendingFinalizers();*/
        }

        public void changeDoorColor()
        {
            foreach (Model.Room r in MainWindow.attGameController.Game.Rooms)
            {
                switch (r.Name)
                //  switch (temp)
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


        private void EnterKeyCommand(object sender, EventArgs e)
        {
            ((TextBox)sender).SelectionLength = 0;
        }

        private void prova_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
