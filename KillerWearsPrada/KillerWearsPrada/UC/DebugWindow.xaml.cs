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
using System.Windows.Shapes;


namespace KillerWearsPrada.UC
{
    /// <summary>
    /// Interaction logic for DebugWindow.xaml
    /// </summary>
    public partial class DebugWindow : Window
    {
        Helpers.DBHelper db;
        public DebugWindow()
        {
            InitializeComponent();
            db = new Helpers.DBHelper();
        }

        private void btnTestDB_Click(object sender, RoutedEventArgs e)
        {
            db = new Helpers.DBHelper();
            txtDisplay.Text = db.TestConnection();
        }

        private void btnQueryProva_Click(object sender, RoutedEventArgs e)
        {
            string r;
            db = new Helpers.DBHelper();
            try
            {
                db.GetItemFromClues(true, true, Model.E_Texture.RIGHE.ToString(), Model.E_ItemKind.CAPPELLO.ToString());
            }
            catch (Exception ex)
            {
                txtDisplay.Text = ex.Message;
            }
            

        }
    }
}
