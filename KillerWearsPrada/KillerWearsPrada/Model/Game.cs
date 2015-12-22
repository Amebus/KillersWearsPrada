using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KillerWearsPrada.Model
{
    [Serializable]
    class Game
    {

        #region Attributes
        private Player attPlayer;
        private List<Room> attRooms;

        #endregion


        public Game ()
        {

        }

        public string PlayerID
        {
            get { return attPlayer.ID; }
        }

    }
}
