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

            sxDoorButton.Content = E_RoomsImages.Livingroom_Image;
            centerDoorButton.Content = E_RoomsImages.Kitchen_Image;
            dxDoorButton.Content = E_RoomsImages.Bedroom_Image;
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
            /*
            Room ucroom = new Room("Livingroom_Image");
            Window parentWindow = Window.GetWindow(this);
            Grid maingrid = (Grid)parentWindow.FindName("mainGrid");
            maingrid.Children.Remove(this);
            maingrid.Children.Add(ucroom); */



            Button b = (Button)sender;
            
            MainWindow yourParentWindow = (MainWindow)Window.GetWindow(this);

            // if(b.Content.ToString() == "Livingroom_Image")
            
             
             
            /*
           Canvas prepareRoom = (Canvas)yourParentWindow.Room.FindName(b.Content.ToString());
           prepareRoom.Visibility = Visibility.Visible; */

            String button_content = b.Content.ToString();
            yourParentWindow.Room.setBackgroundCanvas(Application.Current.Resources[button_content].ToString());
/*
            if(button_content != "Livingroom_Canvas" && button_content != "Kitchen_Canvas")
            {
                Canvas prepareRoom1;
                prepareRoom1 = (Canvas)yourParentWindow.Room.FindName("Livingroom_Canvas");
                prepareRoom1.Visibility = Visibility.Hidden;
                prepareRoom1 = (Canvas)yourParentWindow.Room.FindName("Kitchen_Canvas");
                prepareRoom1.Visibility = Visibility.Hidden;

            } else if (button_content != "Livingroom_Canvas" && button_content != "Bedroom_Canvas")
            {
                Canvas prepareRoom2;
                prepareRoom2 = (Canvas)yourParentWindow.Room.FindName("Livingroom_Canvas");
                prepareRoom2.Visibility = Visibility.Hidden;
                prepareRoom2 = (Canvas)yourParentWindow.Room.FindName("Bedroom_Canvas");
                prepareRoom2.Visibility = Visibility.Hidden;
            } else
            {
                Canvas prepareRoom3;
                prepareRoom3 = (Canvas)yourParentWindow.Room.FindName("Kitchen_Canvas");
                prepareRoom3.Visibility = Visibility.Hidden;
                prepareRoom3 = (Canvas)yourParentWindow.Room.FindName("Bedroom_Canvas");
                prepareRoom3.Visibility = Visibility.Hidden;
            }
*/
            // questo va cmq bene, credo
            Canvas prepareRoom = (Canvas)yourParentWindow.Room.FindName(button_content);
            prepareRoom.Visibility = Visibility.Visible;

            yourParentWindow.StartRoom.Visibility = Visibility.Hidden;
            yourParentWindow.Room.Visibility = Visibility.Visible;
            
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        /*

        private void kitchen_Click(object sender, RoutedEventArgs e)
        {
            Room ucroom = new Room("Kitchen_Image");
            Window parentWindow = Window.GetWindow(this);
            Grid maingrid = (Grid)parentWindow.FindName("mainGrid");
            maingrid.Children.Remove(this);
            maingrid.Children.Add(ucroom);
        }

        private void bedroom_Click(object sender, RoutedEventArgs e)
        {
            Room ucroom = new Room("Bedroom_Image");
            Window parentWindow = Window.GetWindow(this);
            Grid maingrid = (Grid)parentWindow.FindName("mainGrid");
            maingrid.Children.Remove(this);
            maingrid.Children.Add(ucroom);
        }


    */
        private void inventory_button(object sender, RoutedEventArgs e)
        {
            MainWindow yourParentWindow = (MainWindow)Window.GetWindow(this);
            yourParentWindow.StartRoom.Visibility = Visibility.Hidden;
            yourParentWindow.Inventory.Visibility = Visibility.Visible;

            /*
            InventoryUC ucinventory = new InventoryUC(this);
            Window parentWindow = Window.GetWindow(this);
            Grid maingrid = (Grid)parentWindow.FindName("mainGrid");
            maingrid.Children.Remove(this);
            maingrid.Children.Add(ucinventory); */
        }
       
    }
}
