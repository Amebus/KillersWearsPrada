using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KillerWearsPrada.Model
{
    [Serializable]
    class Game : ISerializable
    {

        #region Attributes
        private Player attPlayer;
        private List<Room> attRooms;

        #endregion


        public Game (String ID, String PlayerName)
        {
            attPlayer = new Player(ID, PlayerName);
        }

        public String PlayerID
        {
            get { return attPlayer.ID; }
        }

        public String PlayerName
        {
            get { return PlayerName; }
        }

        public void prova()
        {
            attRooms[0].Items[0].Clue.ToString();    
        }
    }
}
