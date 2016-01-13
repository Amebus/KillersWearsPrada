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
        private Int32 attActualRoom;

        private Int32 attScore;
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

        public Room ActualRoom
        {
            get { return attRooms[attActualRoom]; }
        }
        
        public List<Item> GetRoomItems(Int32 RoomIndex)
        {
            return attRooms[RoomIndex].
        }

        public Item GetItemByCode (Int32 Room, Int32 ItemCode)
        {
            return attRooms[Room].GetItemByCode(ItemCode);
        }

        public Item GetItemByBarCode(Int32 Room, Int32 ItemBarCode)
        {
            return attRooms[Room].GetItemByCode(ItemBarCode);
        }

    }
}
