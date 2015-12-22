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
        private Model.Game attGame;
        private BinaryFormatter attSerializer;
        private Stream attStream;

        /// <summary>
        /// 
        /// </summary>
        public GameController()
        {
            attSerializer = new BinaryFormatter();
        }


        /// <summary>
        /// Save the status of the game in binary format
        /// </summary>
        public void SaveStatus()
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
        public void ResumeStatus()
        {
            String wvPath = Helpers.ResourcesHelper.CurrentDirectory;
            //sistemare la logica per creare il path giusto
            attStream = new FileStream(wvPath, FileMode.Open, FileAccess.Read);
            attGame = (Model.Game)attSerializer.Deserialize(attStream);
            attStream.Close();
        }
    }
}
