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
        Controller.GameController attGameController;
        Helpers.DBHelper db;
        public DebugWindow(Controller.GameController GameController)
        {
            InitializeComponent();
            db = new Helpers.DBHelper();
            attGameController = GameController;
            
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
                //txtDisplay.Text = db.GetItemByGradation(Model.E_Gradiation.LIGHT, Model.E_ItemKind.t_shirt).Code.ToString();
                txtDisplay.Text = db.GetItemFromClues(new Model.Clue(true, Model.E_Gradiation.DARK, Model.E_Shape.LONG, Model.E_Color.BROWN, Model.E_Texture.NULL), Model.E_ItemKind.hat).Code.ToString();
            }
            catch (Exception ex)
            {
                txtDisplay.Text = ex.Message;
            }
            

        }

        private void btnPopolazioneProva_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Controller.GameController.CreateGameAndPlayer("Giocatore1");

                attGameController.LoadGame("-Giocatore1");

            }
            catch (Exception ex)
            {
                txtDisplay.Text = ex.Message;
            }



        }
    }
}
