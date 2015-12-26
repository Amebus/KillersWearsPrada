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
        }

        private void close_button(object sender, RoutedEventArgs e)
        {

            Application.Current.Windows[0].Close();

        }

        /// <summary>
        /// ad ognuno dei metodi qui sotto bisognerebbe passare l'ID che identifica la stanza su cui si è premuto
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void livingroom_Click(object sender, RoutedEventArgs e)
        {
            Room ucroom = new Room("Livingroom_Image");
            Window parentWindow = Window.GetWindow(this);
            Grid maingrid = (Grid)parentWindow.FindName("mainGrid");
            maingrid.Children.Remove(this);
            maingrid.Children.Add(ucroom);


        }

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
    }
}
