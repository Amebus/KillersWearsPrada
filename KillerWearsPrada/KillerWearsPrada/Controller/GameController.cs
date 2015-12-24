using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace KillerWearsPrada.Controller
{
    class GameController
    {
        private const Int32 REFRESH_TIME = 1000;
        private Model.Game attGame;
        private BinaryFormatter attSerializer;
        private Stream attStream;
        private KinectInterrogator attKinectInterrogator;

        private PlayerChecker.PlayerStillOnKinectSensor attPlayerStillOnKinectSensor;
        
        /// <summary>
        /// 
        /// </summary>
        public GameController(Microsoft.Kinect.KinectSensor KinectSensor)
        {
            attPlayerStillOnKinectSensor = new PlayerChecker.PlayerStillOnKinectSensor();
            PlayerChecker.PlayerStillOnKinectSensor.PlayerStillOnKinectSensorChanged += new PlayerChecker.PlayerStillOnKinectSensor.PlayerStillOnKinectSensorEventHandler(HandlePlayerChange);
            attSerializer = new BinaryFormatter();
            attKinectInterrogator = new KinectInterrogator( KinectSensor, REFRESH_TIME);
        }

        /// <summary>
        /// Save the status of the game in binary format
        /// </summary>
        public void SaveGame()
        {
            String wvPath = Helpers.ResourcesHelper.CurrentDirectory;
            wvPath += ("\\" + attGame.PlayerID);
            attStream = new FileStream(wvPath, FileMode.Create, FileAccess.Write);
            attSerializer.Serialize(attStream, attGame);
            attStream.Close();
        }

        /// <summary>
        /// Resume the status of a specified game saved in binary format
        /// </summary>
        public void ResumeGame()
        {
            String wvPath = Helpers.ResourcesHelper.CurrentDirectory;
            //sistemare la logica per creare il path giusto
            attStream = new FileStream(wvPath, FileMode.Open, FileAccess.Read);
            attGame = (Model.Game)attSerializer.Deserialize(attStream);
            attStream.Close();
        }

        public void HandlePlayerChange(object sender, PlayerChecker.PlayerStillOnKinectSensorArgs e)
        {
            //TODO Implementare gestione del cambio di giocatore
        }

    }
}
