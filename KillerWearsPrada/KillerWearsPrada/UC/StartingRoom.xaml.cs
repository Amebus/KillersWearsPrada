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
        //   private static int ok = 0;

        public InventoryUC inventory;

        public StartingRoom()
        {
            /*
            if(ok==0)
            {
                Helpers.ResourcesHelper.ModifyRoomBackgroundPath("Doors_Image");
                ok =  1;
            } */
               
            InitializeComponent();

            //metti i contenuti dei bottoni!!!
            /*
            sxDoorButton.Content = E_RoomsImages.Livingroom_Image;
            centerDoorButton.Content = E_RoomsImages.Kitchen_Image;
            dxDoorButton.Content = E_RoomsImages.Bedroom_Image;
            */

           
        }

        private void exit_button(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Do you really want to exit this game?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Application.Current.Windows[0].Close();
            }


        }

        // Unico metodo per il click dei bottoni delle porte
        //manda nelle varie stanze a seconda del contenuto del bottone


        /// <summary>
        /// Unico metodo che gestisce la stanza in cui si va a seconda del bottone premuto
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
        /// 
        /// </summary>
        /// <param name="prepareRoom">Canvas related to the room the player has just entered </param>
        private void enable_RightRoom_Buttons(ref Canvas prepareRoom)
        {
            MainWindow yourParentWindow = (MainWindow)Window.GetWindow(this);
            switch (prepareRoom.Name.ToString())
            {
                case "Kitchen_Image":
                    {
                        yourParentWindow.Room.change_KitchenButtons_Status(true);
                        yourParentWindow.Room.change_CommonButtons_Status(true);
                        //imposto l'indice della room in cui sta andando il player
                        MainWindow.attGameController.Game.ActualRoomIndex = 2;
                        // 2 credo sia la cucina
                        //   MainWindow.attGameController.ActualRoomIndex = 2;
                        //imposto anche la actualroom?
                    }
                    break;
                case "Livingroom_Image":
                    {
                        yourParentWindow.Room.change_LivingroomButtons_Status(true);
                        yourParentWindow.Room.change_CommonButtons_Status(true);
                        //imposto l'indice della room in cui sta andando il player
                        MainWindow.attGameController.Game.ActualRoomIndex = 1;
                        // 1 credo sia la cucina
                        //     MainWindow.attGameController.ActualRoomIndex = 1;
                    }

                    break;
                default:
                    {
                        yourParentWindow.Room.change_BedroomButtons_Status(true);
                        yourParentWindow.Room.change_CommonButtons_Status(true);
                        //imposto l'indice della room in cui sta andando il player
                        MainWindow.attGameController.Game.ActualRoomIndex = 3;
                        // 3 credo sia la cucina
                        //  MainWindow.attGameController.ActualRoomIndex = 3;
                    }
                    break;
            }
        }
       
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
