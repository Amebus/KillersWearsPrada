using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KillerWearsPrada.Model
{
    [Serializable]
    public class Game : ISerializable
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
        private int attActualRoom;

        private bool attStarted;
        private int attScore;
        private Solution attSolution;
        #endregion

        /// <summary>
        /// Inizialize a new game
        /// </summary>
        /// <param name="ID">ID of the Game</param>
        /// <param name="PlayerName">Name of the player</param>
        public Game (string ID, string PlayerName, List<Room> Rooms, Solution Solution)
        {
            attStarted = false;
            attSolution = new Solution();
            attPlayer = new Player(ID, PlayerName);
            attScore = 0;
            attActualRoom = 0;
            attRooms = Rooms;
            this.attSolution = Solution;
        }

        #region Properties
        public string PlayerID
        {
            get { return attPlayer.ID; }
        }

        public string PlayerName
        {
            get { return PlayerName; }
        }

        /// <summary>
        /// Get or set the Actual Room Index
        /// If the passed value exceed the room count the Actual Room Index is setted to the maximum index available.
        /// If the passed value is less than zero the Actual Room Index is setted to zero.
        /// </summary>
        public int ActualRoomIndex
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

        /// <summary>
        /// Get or Set a value that idicate if the Game is alredy started or not
        /// </summary>
        public bool GameStarted
        {
            get { return attStarted; }
            set { attStarted = value; }
        }

        /// <summary>
        /// Represent the Actual Room
        /// </summary>
        public Room ActualRoom
        {
            get { return attRooms[attActualRoom]; }
        }

        /// <summary>
        /// Represent the player's score 
        /// </summary>
        public int Score
        {
            get { return attScore; }
        }

        /// <summary>
        /// Represent a List of the Items placed in the player's inventory
        /// </summary>
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

        /// <summary>
        /// Represent a List of the Items placed in the player's trash
        /// </summary>
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
        }
        
        /// <summary>
        /// Represent the solution of the game
        /// </summary>
        public Solution Solution
        {
            get { return Solution; }
        }

        #endregion

        #region Methods
        private Room GetRoom(string RoomName)
        {
            foreach (Room r in attRooms)
                if (r.Name == RoomName)
                    return r;
            return null;
        }

        public List<Item> GetRoomItems(int RoomIndex)
        {
            return attRooms[RoomIndex].Items;
        }

        public Item GetItem(string RoomName, int ItemCode)
        {
            return GetRoom(RoomName).GetItem(ItemCode);
        }

        public Item GetItem(string RoomName, string ItemBarCode)
        {
            return GetRoom(RoomName).GetItem(ItemBarCode);
        }

        public Item GetItem(int Room, int ItemCode)
        {
            return attRooms[Room].GetItem(ItemCode);
        }

        public Item GetItem(int Room, string ItemBarCode)
        {
            return attRooms[Room].GetItem(ItemBarCode);
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
