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
using Microsoft.Kinect;
using static KillerWearsPrada.Helpers.ResourcesHelper;

namespace KillerWearsPrada.UC
{
    /// <summary>
    /// Interaction logic for StartingRoom.xaml
    /// </summary>
    public partial class StartingRoom : UserControl
    {
        public InventoryUC inventory;

        public StartingRoom()
        {
            InitializeComponent();
        }


        private void exit_button(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Do you really want to exit this game?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Application.Current.Windows[0].Close();
            }
        }
        
        /// <summary>
        /// Take the button content and prepare the next room the player has chosen to go
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void door_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            MainWindow yourParentWindow = (MainWindow)Window.GetWindow(this);

            string button_content = b.Content.ToString();
            yourParentWindow.Room.setBackgroundCanvas(Application.Current.Resources[button_content].ToString());
            
            Canvas prepareRoom = (Canvas)yourParentWindow.Room.FindName(button_content);
            prepareRoom.Visibility = Visibility.Visible;

            enable_RightRoom_Buttons(ref prepareRoom);
            yourParentWindow.StartRoom.Visibility = Visibility.Hidden;

            change_Buttons_Status(false);

            yourParentWindow.Room.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Enable buttons and set backgorund related to the room entered
        /// </summary>
        /// <param name="prepareRoom">Canvas related to the room the player has just entered</param>
        private void enable_RightRoom_Buttons(ref Canvas prepareRoom)
        {
            MainWindow yourParentWindow = (MainWindow)Window.GetWindow(this);
            switch (prepareRoom.Name.ToString())
            {
                case "Kitchen_Image":
                    {
                        yourParentWindow.Room.change_KitchenButtons_Status(true);
                        yourParentWindow.Room.change_CommonButtons_Status(true);
                        MainWindow.attGameController.Game.ActualRoomIndex = 2;
                    }
                    break;
                case "Livingroom_Image":
                    {
                        yourParentWindow.Room.change_LivingroomButtons_Status(true);
                        yourParentWindow.Room.change_CommonButtons_Status(true);
                        MainWindow.attGameController.Game.ActualRoomIndex = 1;
                    }

                    break;
                default:
                    {
                        yourParentWindow.Room.change_BedroomButtons_Status(true);
                        yourParentWindow.Room.change_CommonButtons_Status(true);
                        MainWindow.attGameController.Game.ActualRoomIndex = 3;
                    }
                    break;
            }
        }
       
        /// <summary>
        /// Open the Inventory 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void inventory_button(object sender, RoutedEventArgs e)
        {
            //Disable all buttons
            change_Buttons_Status(false);
            inventory = null;
            inventory = new InventoryUC();
            room_Canvas.Children.Add(inventory);
            inventory.Focus();
            inventory.Unloaded += UpdateButtonsStartroom;
        }

        /// <summary>
        /// Called after the Unload of the InventoryUC
        /// Enables all the buttons in StartRoom
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateButtonsStartroom(object sender, RoutedEventArgs e)
        {
            change_Buttons_Status(true);
        }

        /// <summary>
        /// Change the status of the buttons 
        /// <param name="b"> Value true/false for the status button </param>
        /// </summary>
        public void change_Buttons_Status(bool b)
        {
            sxDoorButton.IsEnabled = b;
            centerDoorButton.IsEnabled = b;
            dxDoorButton.IsEnabled = b;
            exit.IsEnabled = b;
            inventory_btn.IsEnabled = b;
        }

        /// <summary>
        /// Called everytime StartingRoom is loaded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            sxDoorButton.Content = E_RoomsImages.Livingroom_Image;
            centerDoorButton.Content = E_RoomsImages.Kitchen_Image;
            dxDoorButton.Content = E_RoomsImages.Bedroom_Image;
            
            MainWindow m = (MainWindow)Window.GetWindow(this);
            m.changeDoorColor();
        }
    }
}
