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
            sxDoorButton.Content = E_RoomsImages.Livingroom_Image;
            centerDoorButton.Content = E_RoomsImages.Kitchen_Image;
            dxDoorButton.Content = E_RoomsImages.Bedroom_Image;

        }

        private void close_button(object sender, RoutedEventArgs e)
        {

            Application.Current.Windows[0].Close();

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

            yourParentWindow.Room.setBackgroundCanvas(Application.Current.Resources[b.Content.ToString()].ToString());

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
