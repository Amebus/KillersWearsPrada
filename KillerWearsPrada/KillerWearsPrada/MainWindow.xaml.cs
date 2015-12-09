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


namespace KillerWearsPrada
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {

            Helpers.ResourcesHelper.SaveCurrentDirectory();
            Helpers.ResourcesHelper.ModifyMainBackgroundPath();
            //txtDisplay.AppendText(dir_ok);
            InitializeComponent();

        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtDisplay.Text = Application.Current.Resources["Application_Start_Image"].ToString();
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
           
        }
    }
}
