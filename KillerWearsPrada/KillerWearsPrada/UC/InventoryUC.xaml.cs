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
    /// Logica di interazione per InventoryUC.xaml
    /// </summary>
    public partial class InventoryUC : UserControl
    {
        private UserControl provenienza;
        public InventoryUC(UserControl ucProvenienza)
        {
            provenienza = ucProvenienza;
            InitializeComponent();
            txtDisplay.Text = provenienza.ToString();

        }

        private void close_button(object sender, RoutedEventArgs e)
        {

            Application.Current.Windows[0].Close();

        }

        private void back_button(object sender, RoutedEventArgs e)
        {

            StartingRoom ucstart = new StartingRoom();
            Window parentWindow = Window.GetWindow(this);
            Grid maingrid = (Grid)parentWindow.FindName("mainGrid");
            maingrid.Children.Remove(this);
            maingrid.Children.Add(ucstart);

        }
    }
}
