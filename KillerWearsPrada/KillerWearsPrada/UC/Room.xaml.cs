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

namespace KillerWearsPrada.UC
{
    /// <summary>
    /// Interaction logic for Room.xaml
    /// </summary>
    public partial class Room : UserControl
    {
        private ImageBrush ib1;
        private ImageBrush ib2;
        private ImageBrush ib3;


        public Room(String roomID)
        {
            // roomID contiene l'ID della stanza che devo caricare
            // in questa prova contiene il nome dell'immagine di sfondo della stanza che devo caricare
            // viene passata nel momento del click sulla relativa porta (e quindi creazione)
            
            InitializeComponent();

       //     setBackgroundCanvas(Application.Current.Resources[roomID].ToString());

            //    setBackgroundCanvas(roomID);


        }

        public Room()
        {
            InitializeComponent();
        }

        

        /// <summary>
        /// Set canvas background acconrdingly to the door selected
        /// </summary>
        /// <param name="roomImagePath"></param>
        public void setBackgroundCanvas(String roomImagePath)
        {
            ib1 = new ImageBrush();
            ib1.ImageSource = new BitmapImage(new Uri(@roomImagePath, UriKind.Absolute));
            room_Canvas.Background = ib1;
        }

        /*
        public void setImageBrush1(String roomImagePath)
        {
            ib1 = new ImageBrush();
            ib1.ImageSource = new BitmapImage(new Uri(@roomImagePath, UriKind.Absolute));
        }

        public void setImageBrush2(String roomImagePath)
        {
            ib1 = new ImageBrush();
            ib1.ImageSource = new BitmapImage(new Uri(@roomImagePath, UriKind.Absolute));
        }

        public void setImageBrush3(String roomImagePath)
        {
            ib1 = new ImageBrush();
            ib1.ImageSource = new BitmapImage(new Uri(@roomImagePath, UriKind.Absolute));
        }

            */

        private void close_button(object sender, RoutedEventArgs e)
        {

            Application.Current.Windows[0].Close();

        }

        private void back_button(object sender, RoutedEventArgs e)
        {
            /*
            StartingRoom ucstart = new StartingRoom();
            Window parentWindow = Window.GetWindow(this);
            Grid maingrid = (Grid)parentWindow.FindName("mainGrid");
            maingrid.Children.Remove(this);
            maingrid.Children.Add(ucstart); */

            Livingroom_Image.Visibility = Visibility.Hidden;
            Kitchen_Image.Visibility = Visibility.Hidden;
            Bedroom_Image.Visibility = Visibility.Hidden;
            
            MainWindow yourParentWindow = (MainWindow)Window.GetWindow(this);
            yourParentWindow.Room.Visibility = Visibility.Hidden;
            yourParentWindow.StartRoom.Visibility = Visibility.Visible;

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
}
