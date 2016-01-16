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

            // una volta finite le stanze scambiare il 3° indizio che sarà dato in un'altra stanza


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
        
        public Int32 Score
        {
            get { return attScore; }
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
        /// <summary>
        /// Compute player's score
        /// </summary>
        public void ComputeScore()
        {

        }

        /// <summary>
        /// Generate a list of <see cref="Item,"/> to be put in a <see cref="Room"/>
        /// </summary>
        /// <param name="NumberOfItems"></param>
        /// <returns></returns>
        private List<Item> CreateItems(Int32 NumberOfItems)
        {
            Helpers.DBHelper wvDB = new Helpers.DBHelper();
            List<Item> wvItems = new List<Item>();
            //Random wvRND = new Random(NumberOfItems);
            /*
            per prima cosa estraggo le caratteristiche che userò per la ricerca del capo
            sicuramente avremo come indizi shape e gradation (sono  facili da gestire)
                    ma non ha senso per i cappelli, per quelli ci vuole per forza colore + texture 
            randGradiation = .. 
            randPositive1 = .. 
            randShape = ..
            randPositive2 = ..
            rand1 = .. <-- decide se estrarre come risolutivo Texture o Color
            rand2 = .. <-- estare a caso da Texture o Color
            randPositive3 = ..
           

             qui estraiamo i valori casuali e li salviamo
             ...
             in pratica vorrei che si potesse salvare in delle variabli l'E_xyz.random corretto per ogni caso
             ....
             
            clue1= E_Gradiation(rand)
            clue2= E_Shape
            clue3=...

            Boolean positive1 = true;
            if(!randPositive1%3)
            {
                positive1 = false;
            }
            Boolean positive2 = true;
            if(!randPositive2%3)
            {
                positive2 = false;
            }
            Boolean positive3 = true;
            if(!randPositive3%3)
            {
                positive3 = false;
            }
            
            //aggiungiamo le nuove clues alla stanza
            al posto di E_xyz sostituiamo il valore rand corretto ed il resto va a NULL
            //la prima clue è per gradation
            Clue c1 = new Clue(positive1, E_Gradiation.CHIARO, E_Shape.NULL, E_Color.NULL, E_Texture.NULL); ;
            this.ActualRoom.AddClue(c1);
            // la seconda è per shape 
            Clue c2 = new Clue(positive2, NULL, E_Shape.LUNGO, E_Color.NULL, E_Texture.NULL); ;
            this.ActualRoom.AddClue(c2);
            // la terza dipende da rand1
            Clue c3 = new Clue(positive3, E_Gradiation.NULL, E_Shape.NULL, E_Color.x, E_Texture.y); ;
            this.ActualRoom.AddClue(c3);

            1° capo -quello giusto - corrisponde a 3 indizi - gradation + shape + 3°
            wvItems.Add(wvDB.GetItemFromClues(clue1,clue2,NULL,clue3,ItemKind));
            // aggiungi la clue all'item 1
            2° capo - corrisponde a 2 indizi gradation + shape
            wvItems.Add(wvDB.GetItemFromClues(clue1,clue2,NULL,!clue3,ItemKind));
            // aggiungi la clue all'item 2
            3° e 4° capo  - solo shape
            wvItems.Add(wvDB.GetItemFromClues(clue1,!clue2,NULL,!clue3,ItemKind));
            // aggiungi la clue all'item 3
            wvItems.Add(wvDB.GetItemFromClues(clue1,!clue2,NULL,!clue3,ItemKind));
            // aggiungi la clue all'item 4
            5° e 6° capo - solo gradiation
            wvItems.Add(wvDB.GetItemFromClues(!clue1,clue2,NULL,!clue3,ItemKind));
            // aggiungi la clue all'item 5
            wvItems.Add(wvDB.GetItemFromClues(!clue2,clue2,NULL,!clue3,ItemKind));
            // aggiungi la clue all'item 6



            ora a parte la traduzione dei metodi e variabili finti in qualcosa di più reale
            doremmo avere una stanza popolata, e clues nelle stanze

            */



            if (!attSolution.CheckItemKind(E_ItemKind.Cappello))
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
