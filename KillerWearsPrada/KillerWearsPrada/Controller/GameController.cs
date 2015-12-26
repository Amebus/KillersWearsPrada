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

        private PlayerChecker.PlayerEnterKinectSensor attPlayerEnterKinectSensor;
        private PlayerChecker.PlayerLeaveKinectSensor attPlayerLeaveKinectSensor;

        private ResumeGame attResumeGame;
        private UnloadGame attUnloadGame;
        
        /// <summary>
        /// 
        /// </summary>
        public GameController(Microsoft.Kinect.KinectSensor KinectSensor)
        {
            attSerializer = new BinaryFormatter();
            attKinectInterrogator = new KinectInterrogator( KinectSensor, REFRESH_TIME);

            attResumeGame = new ResumeGame();
            attUnloadGame = new UnloadGame();

            attPlayerEnterKinectSensor = new PlayerChecker.PlayerEnterKinectSensor();
            attPlayerLeaveKinectSensor = new PlayerChecker.PlayerLeaveKinectSensor();

            attPlayerEnterKinectSensor.RaisePlayerEnterKinectSensor += HandlePlayerEnterKinectSensor;
            attPlayerLeaveKinectSensor.RaisePlayerLeaveKinectSensor += HandlePlayerLeaveKinectSensor;

        }
        
        /// <summary>
        /// 
        /// </summary>
        public ResumeGame GetResumeGame
        {
            get { return attResumeGame; }
        }

        /// <summary>
        /// 
        /// </summary>
        public UnloadGame GetUnloadGame
        {
            get { return attUnloadGame; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="Parameters"></param>
        private void HandlePlayerEnterKinectSensor(object Sender, PlayerChecker.PlayerEnterKinectSensor.Args Parameters)
        {
            throw new NotImplementedException();
            //TODO gestire l'evento aggiungendo i delegati anche nella grafica


            LoadGame(Parameters.ID);
            attResumeGame.RaiseEvent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandlePlayerLeaveKinectSensor(object Sender, PlayerChecker.PlayerLeaveKinectSensor.Args Parameters)
        {
            throw new NotImplementedException();
            //TODO gestire l'evento aggiungendo i delegati anche nella grafica

            SaveGame();
            attUnloadGame.RaiseEvent();
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
        public void LoadGame(String ID)
        {
            String wvPath = Helpers.ResourcesHelper.CurrentDirectory;
            //sistemare la logica per creare il path giusto
            attStream = new FileStream(wvPath, FileMode.Open, FileAccess.Read);
            attGame = (Model.Game)attSerializer.Deserialize(attStream);
            attStream.Close();
        }
        

        /// <summary>
        /// Event that occur when the game is resumed
        /// </summary>
        public class ResumeGame
        {
            public event EventHandler<Args> RaiseResumeGame;

            protected virtual void OnResumeGame(Args e)
            {
                EventHandler<Args> wvHendeler = RaiseResumeGame;
                if (wvHendeler != null)
                {
                    wvHendeler(this, e);
                }
            }

            /// <summary>
            /// 
            /// </summary>
            public void RaiseEvent()
            {
                //TODO mettere l'ID del giocatore entrato
                Args wvParameters = new Args();


                OnResumeGame(wvParameters);
            }

            /// <summary>
            /// 
            /// </summary>
            public class Args : EventArgs
            {

            }
        }

        /// <summary>
        /// Event that occur when the game is unload
        /// </summary>
        public class UnloadGame
        {
            public event EventHandler<Args> RaiseUnloadGame;

            protected virtual void OnUnloadGame(Args e)
            {
                EventHandler<Args> wvHendeler = RaiseUnloadGame;
                if (wvHendeler != null)
                {
                    wvHendeler(this, e);
                }
            }

            /// <summary>
            /// 
            /// </summary>
            public void RaiseEvent()
            {
                //TODO mettere l'ID del giocatore entrato
                Args wvParameters = new Args();


                OnUnloadGame(wvParameters);
            }

            public class Args : EventArgs
            {

            }
        }
    }
}
