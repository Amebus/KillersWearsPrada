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
        /// <summary>
        /// Rappresenta la lista di stanze incluso l'ingresso
        /// </summary>
        private List<Room> attRooms;
        /// <summary>
        /// rappresenta la stanza in cui si trova il giocatore
        /// </summary>
        private Int32 attActualRoom;

        private Boolean attStarted;
        private Int32 attScore;
        private Solution attSolution;
        #endregion

        /// <summary>
        /// Inizialize a new game
        /// </summary>
        /// <param name="ID">ID of the Game</param>
        /// <param name="PlayerName">Name of the player</param>
        public Game (String ID, String PlayerName)
        {
            attStarted = false;
            attSolution = new Solution();
            attPlayer = new Player(ID, PlayerName);
            attScore = 0;
            attActualRoom = 0;
            

        }

        #region Properties
        public String PlayerID
        {
            get { return attPlayer.ID; }
        }

        public String PlayerName
        {
            get { return PlayerName; }
        }

        public Int32 ActualRoomIndex
        {
            get { return attActualRoom; }
            set
            {
                if (value >= attRooms.Count)
                    attActualRoom = attRooms.Count - 1;
                else if (value < 0)
                    attActualRoom = 0;
                else
                    attActualRoom = value;
            }
        }

        public Boolean GameStarted
        {
            get { return attStarted; }
            set { attStarted = value; }
        }

        public Room ActualRoom
        {
            get { return attRooms[attActualRoom]; }
        }
        
        public Int32 Score
        {
            get { return attScore; }
        }

        public List<Item> ItemsInInventory
        {
            get
            {
                List<Item> wvItems = new List<Item>();

                foreach(Room r in attRooms)
                {
                    foreach(Item i in r.Items)
                    {
                        if (i.IsInInventory)
                            wvItems.Add(i);
                    }
                }

                return wvItems;
            }
        }

        public List<Item> ItemsIntrash
        {
            get
            {
                List<Item> wvItems = new List<Item>();

                foreach (Room r in attRooms)
                {
                    foreach (Item i in r.Items)
                    {
                        if (i.IsTrashed)
                            wvItems.Add(i);
                    }
                }

                return wvItems;
            }
        }

        public List<Room> Rooms
        {
            get { return attRooms; }
            set { attRooms = value; }
        }
        
        public Solution Solution
        {
            get { return Solution; }
            set { Solution = value; }
        }

        #endregion

        #region Methods

        public List<Item> GetRoomItems(Int32 RoomIndex)
        {
            return attRooms[RoomIndex].Items;
        }

        public Item GetItemByCode(Int32 Room, Int32 ItemCode)
        {
            return attRooms[Room].GetItemByCode(ItemCode);
        }

        public Item GetItemByBarCode(Int32 Room, Int32 ItemBarCode)
        {
            return attRooms[Room].GetItemByCode(ItemBarCode);
        }

        /// <summary>
        /// Compute player's score
        /// </summary>
        public void ComputeScore()
        {

        }

        #endregion
    }
}
