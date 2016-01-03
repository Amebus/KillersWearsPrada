using System;
using System.Collections.Generic;
using System.Data.OleDb;
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
using System.Windows.Shapes;


namespace KillerWearsPrada.UC
{
    /// <summary>
    /// Interaction logic for DebugWindow.xaml
    /// </summary>
    public partial class DebugWindow : Window
    {
        public DebugWindow()
        {
            InitializeComponent();
        }

        private void btnTestDB_Click(object sender, RoutedEventArgs e)
        {
            Helpers.DBHelper db = new Helpers.DBHelper();
            txtDisplay.Text = db.TestConnection();
        }

        private void btnQueryProva_Click(object sender, RoutedEventArgs e)
        {
            string r;
            Helpers.DBHelper db = new Helpers.DBHelper();
            try
            {
                db.GetItemFromClues(true, true, Model.Texture.RIGHE.ToString()).Read().ToString();
            }
            catch (OleDbException ex)
            {
                txtDisplay.Text = ex.Message;
            }
            

        }
    }
}
