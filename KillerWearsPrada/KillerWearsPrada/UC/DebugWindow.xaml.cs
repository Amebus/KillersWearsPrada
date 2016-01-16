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
                //db.GetItemFromClues(Model.E_Shape.CORTO, Model.E_Gradiation.CHIARO, Model.E_Texture.SCOZZESE, Model.E_ItemKind.Cappello);
                //txtDisplay.Text = db.GetItemByShape(Model.E_Shape.SHORT, Model.E_ItemKind.t_shirt).ToString();
                txtDisplay.Text = db.GetItemByGradation(Model.E_Gradiation.LIGHT, Model.E_ItemKind.t_shirt).Code.ToString();
            }
            catch (Exception ex)
            {
                txtDisplay.Text = ex.Message;
            }
            

        }
    }
}
