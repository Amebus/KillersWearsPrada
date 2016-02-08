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
using Microsoft.Kinect;

namespace KillerWearsPrada.UC
{


    /// <summary>
    /// Interaction logic for DebugWindow.xaml
    /// </summary>
    public partial class DebugWindow : Window
    {
        KinectSensor attks;
        BodyFrameReader attBodyFrameReader;
        Controller.GameController attGameController;
        Helpers.DBHelper db;
        public DebugWindow(Controller.GameController GameController)
        {
            InitializeComponent();
            db = new Helpers.DBHelper();
            attGameController = GameController;

            attks = KinectSensor.GetDefault();
            attks.Open();
            attBodyFrameReader = attks.BodyFrameSource.OpenReader();
            attBodyFrameReader.FrameArrived += this.Reader_BodyFrameArrived;

        }

        private void btnTestDB_Click(object sender, RoutedEventArgs e)
        {
            db = new Helpers.DBHelper();
            txtDisplay.Text = db.TestConnection();
        }

        private void btnQueryProva_Click(object sender, RoutedEventArgs e)
        {
            //Model.Game wvGame = null;
            //Model.Room wvRoom = wvGame.ActualRoom;

            db = new Helpers.DBHelper();
            try
            {
                /*
                //db.GetItemFromClues(Model.E_Shape.CORTO, Model.E_Gradiation.CHIARO, Model.E_Texture.SCOZZESE, Model.E_ItemKind.Cappello);
                //txtDisplay.Text = db.GetItemByShape(Model.E_Shape.SHORT, Model.E_ItemKind.t_shirt).ToString();
                //txtDisplay.Text = db.GetItemByGradation(Model.E_Gradiation.LIGHT, Model.E_ItemKind.t_shirt).Code.ToString();
                //txtDisplay.Text = db.GetItemFromClues(new Model.Clue(true, Model.E_Gradiation.DARK, Model.E_Shape.LONG, Model.E_Color.BROWN, Model.E_Texture._NULL), Model.E_ItemKind.hat).Code.ToString();
                //txtDisplay.Text = db.GetGameForProf().ToString();
                string wvPlayerID = "15-01-2016-10-50-42_alberto";
                //string wvPlayerName = "alberto";
                //wvPlayerID += ("_" + wvPlayerName);
                string wvPath = Helpers.ResourcesHelper.SavesDirectory + "\\" + wvPlayerID;
                Model.Game wvGame = Helpers.SerializerHelper.Deserialize(wvPath);
                txtDisplay.Text = wvGame.PlayerID;
                txtDisplay.AppendText("\r\n" + wvGame.ActualRoomIndex);
                txtDisplay.AppendText("\r\n" + wvGame.GameStarted);
                txtDisplay.AppendText("\r\n" + wvGame.PlayerName);
                txtDisplay.AppendText("\r\n" + wvGame.Score);
                */
                db = new Helpers.DBHelper();
                txtDisplay.Text = db.GetShape("190114771213").ToString();
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
                attGameController.CreateGameAndPlayer("Giocatore1");
                //attGameController.CreateProfGame();
                //attGameController.LoadGame("-Giocatore1");
                txtDisplay.Text = ("Game correctly populated");

            }
            catch (Exception ex)
            {
                txtDisplay.Text = ex.Message;
            }


        }

        private void Reader_BodyFrameArrived(object sender, BodyFrameArrivedEventArgs e)
        {
            bool wvDataReceived = false;
            BodyFrame wvBodyFrame = e.FrameReference.AcquireFrame();

            if (wvBodyFrame == null)
                return;

            Body[] wvBodies = new Body[wvBodyFrame.BodyCount];

            wvBodyFrame.GetAndRefreshBodyData(wvBodies);
            wvDataReceived = true;


            if (!wvDataReceived)
                return;

            int attBodyCount = 0;
            foreach (Body b in wvBodies)
            {
                if (b.IsTracked)
                    attBodyCount++;
            }

            txtDisplay.Text = attBodyCount.ToString();
        }

    }
}
