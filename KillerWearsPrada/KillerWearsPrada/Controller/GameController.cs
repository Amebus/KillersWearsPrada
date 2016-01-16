using System;
using System.Collections.Generic;

namespace KillerWearsPrada.Controller
{
    public class GameController
    {
        private const Int32 REFRESH_TIME = 1000;
        private const Int32 DEBUG_REFRESH_TIME = 10000;
        private const Int32 ITEMS_PER_ROOM = 2;
        private const Int32 NUMBER_OF_ROOMS = 3;

        private Model.Game attGame;
        private Helpers.DBHelper attDataBase;
        private KinectInterrogator attKinectInterrogator;

        private ResumeGame attResumeGame;
        private UnloadGame attUnloadGame;
        
        /// <summary>
        /// 
        /// </summary>
        public GameController(Microsoft.Kinect.KinectSensor KinectSensor)
        {
            attKinectInterrogator = new KinectInterrogator( KinectSensor, REFRESH_TIME );

            attDataBase = new Helpers.DBHelper();  

            attResumeGame = new ResumeGame();
            attUnloadGame = new UnloadGame();

            attKinectInterrogator.RaisePlayerEnterKinectSensor = HandlePlayerEnterKinectSensor;
            attKinectInterrogator.RaisePlayerLeaveKinectSensor = HandlePlayerLeaveKinectSensor;
            
        }
        
        /// <summary>
        /// 
        /// </summary>
        public ResumeGame GetResumeGame
        {
            get { return attResumeGame; }
        }

        /// <summary>
        /// 
        /// </summary>
        public UnloadGame GetUnloadGame
        {
            get { return attUnloadGame; }
        }

        public Int32 ActualRoomIndex
        {
            get { return attGame.ActualRoomIndex; }
            set { attGame.ActualRoomIndex = value; }
        }
        
        public Model.Room ActualRoom
        {
            get { return attGame.ActualRoom; }
        }
        
        public List<Model.Item> ActualRoomItems
        {
            get { return attGame.ActualRoom.Items; }
        }

        public Boolean IsGameStarted
        {
            get { return attGame.GameStarted; }
        }

        public String NamePlayer
        {
            get { return attGame.PlayerName; }
        }

        public List<Model.Room> Rooms
        {
            get { return attGame.Rooms; }
        }

        public List<Model.Item> ItemsInInventory
        {
            get { return attGame.ItemsInInventory; }
        }

        public List<Model.Item> ItemsInTrash
        {
            get { return attGame.ItemsIntrash; }
        }

        public void SetGameStarted ()
        {
            attGame.GameStarted = true;
        }

        /// <summary>
        /// Save the status of the game in binary format
        /// </summary>
        public void SaveGame()
        {
            String wvPath = Helpers.ResourcesHelper.SavesDirectory;
            wvPath = CombinePath(wvPath, attGame.PlayerID);
            Helpers.SerializerHelper.Serialize(wvPath, attGame);
        }

        /// <summary>
        /// Resume the status of a specified game saved in binary format
        /// </summary>
        public void LoadGame(String ID)
        {
            String wvPath = CombinePath(Helpers.ResourcesHelper.SavesDirectory, ID);
            attGame = (Model.Game)Helpers.SerializerHelper.Deserialize(wvPath);
        }
        
        public void StartTakingScreenShot()
        {
            attKinectInterrogator.StartTakingScreenshot();
        }

        public void StopTakingScreenShot()
        {
            attKinectInterrogator.StopTakingScreenshot();
        }



        /// <summary>
        /// Create a new <see cref="Model.Game"/> and a new <see cref="Model.Player"/> and save them into a file
        /// </summary>
        /// <param name="PlayerName">Name of the <see cref="Model.Player"/></param>
        public static void CreateGameAndPlayer(String PlayerName)
        {
            String wvPath = Helpers.ResourcesHelper.SavesDirectory;
            String wvID = DateTime.Now.ToString();

            wvID = wvID.Replace(' ', '-');
            wvID += ("-" + PlayerName);
            Model.Game wvGame = new Model.Game(wvID, PlayerName);
            
            wvPath = System.IO.Path.Combine(wvPath, wvID);
            Helpers.SerializerHelper.Serialize(wvPath, wvGame);

            throw new NotImplementedException("Implementare qui la stampa dei QRCODE");
        }

        /// <summary>
        /// Generate a list of <see cref="Item,"/> to be put in a <see cref="Room"/>
        /// </summary>
        /// <param name="NumberOfItems"></param>
        /// <returns></returns>
        private void CreateGame(Int32 NumberOfItems)
        {
            Model.Solution wvSolution = new Model.Solution();
            List<Model.Item> wvItems = new List<Model.Item>();
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



            if (!wvSolution.CheckItemKind(Model.E_ItemKind.Cappello))
            {
                wvSolution.AddItemKind(Model.E_ItemKind.Cappello);
            }
            else if (!wvSolution.CheckItemKind(Model.E_ItemKind.Maglietta))
            {
                wvSolution.AddItemKind(Model.E_ItemKind.Maglietta);
            }
            else if (!wvSolution.CheckItemKind(Model.E_ItemKind.Pantaloni))
            {
                wvSolution.AddItemKind(Model.E_ItemKind.Pantaloni);
            }


            //Il primo è quello giusto
            wvItems.Add(attDataBase.GetItemByGradation(Model.E_Gradiation.CHIARO, wvSolution.LastItemKind));

            NumberOfItems--;
            //mi faccio dare gli altri (per ora sola il secondo)
            wvItems.Add(attDataBase.GetItemByGradation(Model.E_Gradiation.SCURO, wvSolution.LastItemKind));
            
        }

        public static void DeleteAllGames()
        {
            System.IO.DirectoryInfo wvDirInfo = new System.IO.DirectoryInfo(Helpers.ResourcesHelper.SavesDirectory);
            System.IO.FileInfo[] wvFileInfos = wvDirInfo.GetFiles();

            foreach (System.IO.FileInfo wvFile in wvFileInfos)
                wvFile.Delete();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="Parameters"></param>
        private void HandlePlayerEnterKinectSensor(object Sender, PlayerChecker.PlayerEnterKinectSensor.Args Parameters)
        {
            LoadGame(Parameters.ID);
            attResumeGame.RaiseEvent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandlePlayerLeaveKinectSensor(object Sender, PlayerChecker.PlayerLeaveKinectSensor.Args Parameters)
        {
            throw new NotImplementedException("Implementare la logica che gestisce il momento in cui il giocatore lascia la postazione");

            SaveGame();
            attUnloadGame.RaiseEvent();
        }

        private string CombinePath(String Path1, String Path2)
        {
            return System.IO.Path.Combine(Path1, Path2);
        }

        /// <summary>
        /// Event that occur when the game is resumed
        /// </summary>
        public class ResumeGame
        {
            public event EventHandler<Args> RaiseResumeGame;

            protected virtual void OnResumeGame(Args e)
            {
                EventHandler<Args> wvHendeler = RaiseResumeGame;
                if (wvHendeler != null)
                {
                    wvHendeler(this, e);
                }
            }

            /// <summary>
            /// 
            /// </summary>
            public void RaiseEvent()
            {
                //TODO mettere l'ID del giocatore entrato
                Args wvParameters = new Args();


                OnResumeGame(wvParameters);
            }

            /// <summary>
            /// 
            /// </summary>
            public class Args : EventArgs
            {

            }
        }

        /// <summary>
        /// Event that occur when the game is unload
        /// </summary>
        public class UnloadGame
        {
            public event EventHandler<Args> RaiseUnloadGame;

            protected virtual void OnUnloadGame(Args e)
            {
                EventHandler<Args> wvHendeler = RaiseUnloadGame;
                if (wvHendeler != null)
                {
                    wvHendeler(this, e);
                }
            }

            /// <summary>
            /// 
            /// </summary>
            public void RaiseEvent()
            {
                //TODO mettere l'ID del giocatore entrato
                Args wvParameters = new Args();


                OnUnloadGame(wvParameters);
            }

            public class Args : EventArgs
            {

            }
        }
    }
}
