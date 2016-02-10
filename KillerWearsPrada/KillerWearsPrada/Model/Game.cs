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
        public bool IsGameStarted
        {
            get { return attStarted; }
        }
        
        public bool IsFinished
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

        /// <summary>
        /// Return a <see cref="List{string}"/> of strings which represents the disclosed clues of the game
        /// </summary>
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

        /// <summary>
        /// Reurn the <see cref="List{T}"/> of <see cref="Item"/> which represents the items dressed by the killer
        /// </summary>
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
        /// Return a <see cref="List{T}"/> of <see cref="Item"/> which represents the items placed in the player's inventory
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

        /// <summary>
        /// Return a <see cref="List{T}"/> of <see cref="Item"/> which represents the items not added to playre's inventory
        /// </summary>
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
        /// Return a <see cref="List{T}"/> of <see cref="Item"/> which represents the Items placed in the player's trash
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
            get { return attSolution; }
        }

        #endregion

        #region Methods
        public void SetAsStarted()
        {
            attStarted = true;
        }
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
        /// Return true if the <see cref="Item"/> associated to the specified BarCode had been added to the inventory
        /// </summary>
        /// <param name="BarCode"><see cref="string"/> that represent the barcode of the <see cref="Item"/> to be added</param>
        /// <returns></returns>
        public bool SetInInventory(string BarCode)
        {
            foreach(Room r in Rooms)
            {
                foreach(Item i in r.Items)
                {
                    if (i.BarCode == BarCode)
                    {
                        if (i.IsInInventory)
                            throw new ItemAlreadyInInvetory(i.ItemName, i.BarCode);
                        i.SetAsInInventory();
                        return true;
                    }

                }
            }

            throw new ItemNotInGameException();
        }

        #endregion
    }

    /// <summary>
    /// Exception that must be raised when the item that the user is trying to add an item already contained in the inventory
    /// </summary>
    public class ItemAlreadyInInvetory : Exception
    {
        private const string BASE = "You alredy added @p1 to the inventory";

        public ItemAlreadyInInvetory(string ItemName, string BarCode)
        {
            this.ItemName = ItemName;
            this.BarCode = BarCode;
        }

        public string BarCode { get; private set; }
        public string ItemName { get; private set; }

        public override string Message
        {
            get
            {
                return BASE.Replace("@p1", ItemName);
            }
        }
    }

    /// <summary>
    /// Exception that must be raised when the item that the user is trying to add is not part of the game
    /// </summary>
    public class ItemNotInGameException : Exception
    {
        private const string BASE = "The item you are attempting to add to your inventory is not part of the Game";

        public ItemNotInGameException()
        {
        }


        public override string Message
        {
            get
            {
                return BASE;
            }
        }
    }
}
