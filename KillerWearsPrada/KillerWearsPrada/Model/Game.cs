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

        private const int SCORE_STEP = 5;

        /// <summary>
        /// Rappresenta la lista di stanze incluso l'ingresso
        /// </summary>
        private List<Room> attRooms;
        /// <summary>
        /// rappresenta la stanza in cui si trova il giocatore
        /// </summary>
        private int attActualRoom;

        private bool attStarted;
        private bool attFinished;
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
            this.Player = new Player(ID, PlayerName);
            attScore = 0;
            attActualRoom = 0;
            attRooms = Rooms;
            this.attSolution = Solution;
        }

        #region Properties
        private Player Player { get; set; }

        public string PlayerID
        {
            get { return Player.ID; }
        }

        public string PlayerName
        {
            get { return Player.Name; }
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
        
        public bool IsFinisced
        {
            get { return attFinished; }

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

        public List<string> DisclosedClues
        {
            get
            {
                List<string> wvDisclossedClues = new List<string>();

                foreach(Room r in attRooms)
                {
                    if (r.Name == E_RoomsName.START_ROOM)
                        continue;

                    wvDisclossedClues.AddRange(r.DisclosedItemsClues);
                    if (r.IsLastClueAlreadyShown)
                        wvDisclossedClues.Add(r.LastClue);

                }

                return wvDisclossedClues;
            }
        }

        public List<Item> ItemsDressed
        {
            get
            {
                List<Item> wvItems = new List<Item>();

                foreach (Room r in attRooms)
                {
                    wvItems.AddRange(r.ItemsDressed);
                }

                return wvItems;
            }
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
                    wvItems.AddRange(r.ItemsInInventory);
                }

                return wvItems;
            }
        }

        public List<Item> ItemsNotInInventory
        {
            get
            {
                List<Item> wvItems = new List<Item>();
                foreach (Room r in attRooms)
                {
                    wvItems.AddRange(r.ItemsNotInInventory);
                }

                return wvItems;
            }
        }

        /// <summary>
        /// Represent a List of the Items placed in the player's trash
        /// </summary>
        public List<Item> ItemsInTrash
        {
            get
            {
                List<Item> wvItems = new List<Item>();

                foreach (Room r in attRooms)
                {
                    wvItems.AddRange(r.ItemsTrashed);
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
        public void SetAsFinished()
        {
            attFinished = true;
        }

        private Room GetRoom(E_RoomsName RoomName)
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

        public Item GetItem(E_RoomsName RoomName, int ItemCode)
        {
            return GetRoom(RoomName).GetItem(ItemCode);
        }

        public Item GetItem(E_RoomsName RoomName, string ItemBarCode)
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
            int wvScore = 0;

            foreach(Item i in ItemsDressed)
            {
                if (attSolution.CheckInSolution(i))
                    wvScore += SCORE_STEP;
            }

            attScore = wvScore;
        }

        public void EmptyTrash ()
        {
            foreach(Room r in attRooms)
            {
                r.EmptyTrash();
            }
        }

        /// <summary>
        /// Return true if the item had been added to the inventory
        /// </summary>
        /// <param name="BarCode"></param>
        /// <returns></returns>
        public bool SetInInventory(string BarCode)
        {

            foreach(Item i in ItemsNotInInventory)
            {
                if (i.BarCode == BarCode)
                {
                    i.SetAsInInventory();
                    return true;
                }
            }

            return false;

        }

        #endregion
    }
}
