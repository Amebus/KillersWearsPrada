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
        private ImageBrush ib;


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
            ib = new ImageBrush();
            ib.ImageSource = new BitmapImage(new Uri(@roomImagePath, UriKind.Absolute));
            room_Canvas.Background = ib;
        }

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

            MainWindow yourParentWindow = (MainWindow)Window.GetWindow(this);
            yourParentWindow.Room.Visibility = Visibility.Hidden;
            yourParentWindow.StartRoom.Visibility = Visibility.Visible;

        }
    }
}
