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

        private Int32 attScore;
        private Solution attSolution;
        #endregion

        /// <summary>
        /// Inizialize a new game
        /// </summary>
        /// <param name="ID">ID of the Game</param>
        /// <param name="PlayerName">Name of the player</param>
        /// <param name="Rooms">Number of Rooms in the game</param>
        /// <param name="ItemsPerRoom">Number of items in each room</param>
        public Game (String ID, String PlayerName, Int32 Rooms, Int32 ItemsPerRoom)
        {
            attSolution = new Solution();
            attPlayer = new Player(ID, PlayerName);
            attScore = 0;
            attActualRoom = 0;

            attRooms = new List<Room>(Rooms);

            //creo singole stanze
            attRooms[0] = null;

            for(int i = 1; i < Rooms; i++)
            {
                attRooms[i] = new Room(CreateItems(ItemsPerRoom));
            }

            //


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

        public Room ActualRoom
        {
            get { return attRooms[attActualRoom]; }
        }
        
        public List<Item> GetRoomItems(Int32 RoomIndex)
        {
            return attRooms[RoomIndex].Items;
        }

        public Item GetItemByCode (Int32 Room, Int32 ItemCode)
        {
            return attRooms[Room].GetItemByCode(ItemCode);
        }

        public Item GetItemByBarCode(Int32 Room, Int32 ItemBarCode)
        {
            return attRooms[Room].GetItemByCode(ItemBarCode);
        }
        #endregion

        #region Methods

        private List<Item> CreateItems(Int32 NumberOfItems)
        {
            Helpers.DBHelper wvDB = new Helpers.DBHelper();
            List<Item> wvItems = new List<Item>();
            //Random wvRND = new Random(NumberOfItems);

            if(!attSolution.CheckItemKind(E_ItemKind.Cappello))
            {
                attSolution.AddItemKind(E_ItemKind.Cappello);
            }
            else if(!attSolution.CheckItemKind(E_ItemKind.Maglietta))
            {
                attSolution.AddItemKind(E_ItemKind.Maglietta);
            }
            else if (!attSolution.CheckItemKind(E_ItemKind.Pantaloni))
            {
                attSolution.AddItemKind(E_ItemKind.Pantaloni);
            }


            //Il primo è quello giusto
            wvItems.Add(wvDB.GetItemByGradation(E_Gradiation.CHIARO, attSolution.LastItemKind));
            NumberOfItems--;
            //mi faccio dare gli altri (per ora sola il secondo)
            wvItems.Add(wvDB.GetItemByGradation(E_Gradiation.SCURO, attSolution.LastItemKind));

            return wvItems;
        }

        #endregion
    }
}
