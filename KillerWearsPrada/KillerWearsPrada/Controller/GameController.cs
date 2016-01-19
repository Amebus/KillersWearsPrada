using System;
using System.Collections.Generic;
using KillerWearsPrada.Model;

namespace KillerWearsPrada.Controller
{
    public class GameController
    {
        private const int REFRESH_TIME = 1000;
        private const int DEBUG_REFRESH_TIME = 10000;
        private const int ITEMS_PER_ROOM = 2;
        private const int NUMBER_OF_ROOMS_WITH_ITEMS = 3;

        private Random attRandom;
        private Helpers.DBHelper attDataBase;
        private Game attGame;
        private KinectInterrogator attKinectInterrogator;

        private ResumeGame attResumeGame;
        private UnloadGame attUnloadGame;

        private readonly bool[][] attPopulationMatrix =
        { 
            new bool[] { true, true, true },
            new bool[] { true, true, false },
            new bool[] { true, false, true },
            new bool[] { true, false, false },
            new bool[] { false, true, true },
            new bool[] { false, true, false }
        };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="KinectSensor">The <see cref="Microsoft.Kinect.KinectSensor"/> associated to the game</param>
        public GameController(Microsoft.Kinect.KinectSensor KinectSensor)
        {
            attRandom = new Random();
            attKinectInterrogator = new KinectInterrogator( KinectSensor, REFRESH_TIME );

            attDataBase = new Helpers.DBHelper();  

            attResumeGame = new ResumeGame();
            attUnloadGame = new UnloadGame();

            attKinectInterrogator.RaisePlayerEnterKinectSensor = HandlePlayerEnterKinectSensor;
            attKinectInterrogator.RaisePlayerLeaveKinectSensor = HandlePlayerLeaveKinectSensor;
            
        }

        #region Proprietà
        public Model.Game Game
        {
            get { return attGame; }
        }

        /// <summary>
        /// The instannce of Class representing the Event ResumeGame
        /// </summary>
        public ResumeGame GetResumeGame
        {
            get { return attResumeGame; }
        }

        /// <summary>
        /// The instannce of Class representing the Event UnloadGame
        /// </summary>
        public UnloadGame GetUnloadGame
        {
            get { return attUnloadGame; }
        }

        /// <summary>
        /// Representing the index of the room in which the player is investigating
        /// </summary>
        public int ActualRoomIndex
        {
            get { return attGame.ActualRoomIndex; }
            set { attGame.ActualRoomIndex = value; }
        }

        /// <summary>
        /// Reperesent the <see cref="Model.Room"/> in which the player is investigating
        /// </summary>
        public Room ActualRoom
        {
            get { return attGame.ActualRoom; }
        }
        
        /// <summary>
        /// Represent a List of <see cref="Model.Item"/> items contained in the <see cref="Model.Room"/> in which the player is invastigating
        /// </summary>
        public List<Item> ActualRoomItems
        {
            get { return attGame.ActualRoom.Items; }
        }

        /// <summary>
        /// Represent a value which indicate if the game is alredy started
        /// </summary>
        public bool IsGameStarted
        {
            get { return attGame.GameStarted; }
        }

        /// <summary>
        /// The name of the player
        /// </summary>
        public string NamePlayer
        {
            get { return attGame.PlayerName; }
        }

        /// <summary>
        /// Represent the List of <see cref="Model.Room"/> defined in the actual game
        /// </summary>
        public List<Room> Rooms
        {
            get { return attGame.Rooms; }
        }

        /// <summary>
        /// Represent a List of <see cref="Model.Item"/> that are in the player's inventory
        /// </summary>
        public List<Item> ItemsInInventory
        {
            get { return attGame.ItemsInInventory; }
        }

        /// <summary>
        /// Represent a List of <see cref="Model.Item"/> that are in the player's tresh
        /// </summary>
        public List<Item> ItemsInTrash
        {
            get { return attGame.ItemsIntrash; }
        }
        #endregion

        /// <summary>
        /// Set a value that the Game has been started
        /// </summary>
        public void SetGameStarted ()
        {
            attGame.GameStarted = true;
        }

        /// <summary>
        /// Save the status of the game in binary format
        /// </summary>
        public void SaveGame()
        {
            string wvPath = Helpers.ResourcesHelper.SavesDirectory;
            wvPath = CombinePath(wvPath, attGame.PlayerID);
            Helpers.SerializerHelper.Serialize(wvPath, attGame);
        }

        /// <summary>
        /// Resume the status of a specified game saved in binary format
        /// </summary>
        public void LoadGame(string ID)
        {
            string wvPath = CombinePath(Helpers.ResourcesHelper.SavesDirectory, ID);
            attGame = (Model.Game)Helpers.SerializerHelper.Deserialize(wvPath);
        }
        
        /// <summary>
        /// Start the routine which is in charge to taking screenshots to recognize whenever a palyer enter or leave the KinectSensor
        /// </summary>
        public void StartTakingScreenshots()
        {
            attKinectInterrogator.StartTakingScreenshots();
        }

        /// <summary>
        /// Stop the routine which is in charge to taking screenshots to recognize whenever a palyer enter or leave the KinectSensor
        /// </summary>
        public void StopTakingScreenshots()
        {
            attKinectInterrogator.StopTakingScreenshots();
        }

        public int ComputeScore()
        {
            attGame.ComputeScore();

            return new Random().Next(16, 31);
        }

        /// <summary>
        /// Create a new <see cref="Model.Game"/> and a new <see cref="Model.Player"/> and save them into a file
        /// </summary>
        /// <param name="PlayerName">Name of the <see cref="Model.Player"/></param>
        public void CreateGameAndPlayer(string PlayerName)
        {
            string wvPath = Helpers.ResourcesHelper.SavesDirectory;
            string wvID="";// = DateTime.Now.ToString();

            //wvID = wvID.Replace(' ', '-');
            //wvID += ("-" + PlayerName);
            //Game wvGame = new Game(wvID, PlayerName);

            attGame = CreateGame(wvID, PlayerName, ITEMS_PER_ROOM);

            //wvPath = System.IO.Path.Combine(wvPath, wvID);
            //Helpers.SerializerHelper.Serialize(wvPath, wvGame);

            //throw new NotImplementedException("Implementare qui la stampa dei QRCODE");
        }

        #region Generation, inizialization and elimination of the games
        /// <summary>
        /// Generate a list of <see cref="Item,"/> to be put in a <see cref="Room"/>
        /// </summary>
        /// <param name="NumberOfItems"></param>
        /// <returns></returns>
        private Game CreateGame(string ID, string PalyerName,int NumberOfItems)
        {

            Solution wvSolution = new Solution();
            List<Room> wvRooms = new List<Room>();

            wvRooms.Add(new Room("Start_Room"));

            for (int i = 1; i <= NUMBER_OF_ROOMS_WITH_ITEMS; i++)
            {
                AbstractItem wvCorrectAbstractItem = GenerateCorrectAbstractItem((E_ItemKind._NULL + i));
                Item wvCorrectItem = attDataBase.GetItemFromAbstractItem(wvCorrectAbstractItem);

                wvSolution.AddItem(GenerateCorrectAbstractItem((E_ItemKind._NULL + i)));

                //wvRooms.Add(GenerateRoom("name", wvCorrectItem));

            }

            return new Game(ID, PalyerName, wvRooms, wvSolution);
            
            //wvCorrectClue = GenerateCorrectClue(wvItemKind);
            //wvCorrectItem = attDataBase.GetItemFromClues(wvCorrectClue, wvItemKind);
            //wvSolution.AddItem(wvCorrectClue, wvCorrectItem);

            //wvWrongClues = GenerateCluesFromCorrectClue(wvCorrectClue, ITEMS_PER_ROOM - 1);

            //Room r = new Room()

            /*
            wvItemKind = E_ItemKind.trousers;
            wvCorrectClue = GenerateCorrectClue(wvItemKind);
            wvCorrectItem = attDataBase.GetItemFromClues(wvCorrectClue, wvItemKind);
            wvSolution.AddItem(wvCorrectClue, wvCorrectItem);

            wvWrongClues = GenerateCluesFromCorrectClue(wvCorrectClue, ITEMS_PER_ROOM - 1);


            wvItemKind = E_ItemKind.t_shirt;
            wvCorrectClue = GenerateCorrectClue(wvItemKind);
            wvCorrectItem = attDataBase.GetItemFromClues(wvCorrectClue, wvItemKind);
            wvSolution.AddItem(wvCorrectClue, wvCorrectItem);

            wvWrongClues = GenerateCluesFromCorrectClue(wvCorrectClue, ITEMS_PER_ROOM - 1);
            
            //riprendere da qui

            //Il primo è quello giusto
            //wvItems.Add(attDataBase.GetItemByGradation(Model.E_Gradiation.LIGHT, wvSolution.LastItemKind));

            NumberOfItems--;
            //mi faccio dare gli altri (per ora sola il secondo)
            //wvItems.Add(attDataBase.GetItemByGradation(Model.E_Gradiation.DARK, wvSolution.LastItemKind));


            wvItems = GenerateItemsByItemKind(wvItemKind);
            */

        }
        /*
        private Room GenerateRoom(string Name, Item CorrectItem)
        {
            Room wvRoom = null;
            List<Item> wvItems = new List<Item>();

            for(int i = (int)E_ItemType.A; i<= (int)E_ItemType.F; i++)
            {
                AbstractItem wvAbstractItem = new AbstractItem((E_ItemType)i, CorrectItem.ItemKind);

                switch((E_ItemType)i)
                {
                    case E_ItemType.A:
                        wvItems.Add(CorrectItem);
                        break;
                    case E_ItemType.B:
                        wvAbstractItem = InvertByItemType(CorrectItem, E_ItemType.B);

                        break;
                    case E_ItemType.C:
                        break;
                    case E_ItemType.D:
                        break;
                    case E_ItemType.E:
                        break;
                    case E_ItemType.F:
                        break;
                    default:
                        break;
                }


            }








            return wvRoom;
        }*/
        /*
        private AbstractItem InvertByItemType(AbstractItem CorrectItem, E_ItemType ItemTypeToGenerate)
        {
            ItemGraficalProperty wvIgp;
            AbstractItem wvInvertedAbstractAbstractItem = new AbstractItem(ItemTypeToGenerate, CorrectItem.ItemKind);
            int wvRandom;
            int n = 0;
            for(int i = 0;  i<CorrectItem.PropertiesCount; i++)
            {
                ItemGraficalProperty wvProperty = new ItemGraficalProperty();
                switch(wvIgp.PropertyKind)
                {
                    case E_PropertiesKind.COLOR:

                        #region Color from Gradiation
                        List<E_Color> wvColors = new List<E_Color>();
                        if (CorrectItem.CheckPropertyByKind(E_PropertiesKind.GRADIATION))
                        {
                            for (int i = (int)E_Color._NULL + 1; i < (int)E_Color._END; i++)
                            {
                                if(((E_Color)i).ToString()!= CorrectItem.GetProperty(E_PropertiesKind.COLOR))
                                    wvColors.Add((E_Color)i);
                            }
                        }
                        else
                        {
                            for (int i = (int)E_Color._NULL + 1; i < (int)E_Color._END; i++)
                            {
                                if ((E_Color)i != E_Color.BLACK && ((E_Color)i).ToString() != CorrectItem.GetProperty(E_PropertiesKind.COLOR)) //TODO E_Color.WHITE
                                    wvColors.Add((E_Color)i);
                            }
                        }
                        wvRandom = attRandom.Next(wvColors.Count);
                        wvProperty.SetProperty(wvIgp.PropertyKind, wvColors[wvRandom]);
                        #endregion

                        wvInvertedAbstractAbstractItem.AddProperty(wvProperty);
                        break;
                    case E_PropertiesKind.GRADIATION:
                        E_Gradiation wvGradiation = E_Gradiation.DARK;

                        if (CorrectItem.GetProperty(E_PropertiesKind.GRADIATION) == E_Gradiation.DARK.ToString())
                            wvGradiation = E_Gradiation.LIGHT;
                        wvProperty.SetProperty(wvIgp.PropertyKind, wvGradiation);
                        wvInvertedAbstractAbstractItem.AddProperty(wvProperty);
                        break;
                    case E_PropertiesKind.SHAPE:
                        E_Shape wvShape = E_Shape.LONG;

                        if (CorrectItem.GetProperty(E_PropertiesKind.SHAPE) == E_Shape.LONG.ToString())
                            wvShape = E_Shape.SHORT;
                        wvProperty.SetProperty(wvIgp.PropertyKind, wvShape);
                        wvInvertedAbstractAbstractItem.AddProperty(wvProperty);
                        break;
                    case E_PropertiesKind.TEXTURE:



                        List<E_Texture> wvTextures = new List<E_Texture>();

                        for (int i = (int)E_Texture._NULL + 1; i < (int)E_Texture._END; i++)
                        {
                            if (((E_Texture)i).ToString() != CorrectItem.GetProperty(E_PropertiesKind.TEXTURE))
                                wvTextures.Add((E_Texture)i);
                        }
                        
                        wvRandom = attRandom.Next(wvTextures.Count);
                        wvProperty.SetProperty(wvIgp.PropertyKind, wvTextures[wvRandom]);
                        
                        break;

                    default:
                        break;
                }


                n++;
            }

            return wvInvertedAbstractAbstractItem;   
        }
        */
        private AbstractItem GenerateCorrectAbstractItem(E_ItemKind ItemKind)
        {
            int wvRandom;
            E_Gradiation wvGradiation;
            E_Shape wvShape;
            E_Color wvColor;
            E_Texture wvTexture;

            AbstractItem wvAbstractItem = new AbstractItem(E_ItemType.A, ItemKind);
            ItemGraficalProperty wvProperty = new ItemGraficalProperty();
            List <E_PropertiesKind> wvPropertiesKind = new List<E_PropertiesKind>();
            
            wvRandom = attRandom.Next((int)E_PropertiesKind._NULL + 1 ,(int)E_PropertiesKind._END);

            if (ItemKind == E_ItemKind.HAT)
            {
                for (int i = (int)E_PropertiesKind._NULL + 1; i < (int)E_PropertiesKind._END ; i++)
                {
                    if (i != wvRandom)
                        wvPropertiesKind.Add((E_PropertiesKind)i);
                }
            }
            else
            {
                for (int i = (int)E_PropertiesKind._NULL + 1; i < (int)E_PropertiesKind._END; i++)
                {
                    if (i != (int)E_PropertiesKind.SHAPE)
                        wvPropertiesKind.Add((E_PropertiesKind)i);
                }
            }

            wvGradiation = E_Gradiation._NULL;
            foreach (E_PropertiesKind pk in wvPropertiesKind)
            {
                wvProperty = new ItemGraficalProperty();
                switch (pk)
                {
                    case E_PropertiesKind.COLOR:

                        #region Color from Gradiation
                        List<E_Color> wvColors = new List<E_Color>();
                        if(wvGradiation == E_Gradiation._NULL)
                        {
                            for (int i = (int)E_Color._NULL + 1; i < (int)E_Color._END; i++)
                            {
                                wvColors.Add((E_Color)i);
                            }
                        }
                        else
                        {
                            for (int i = (int)E_Color._NULL + 1; i < (int)E_Color._END; i++)
                            {
                                if ((E_Color)i != E_Color.BLACK) //TODO E_Color.WHITE
                                    wvColors.Add((E_Color)i);
                            }
                        }
                        wvRandom = attRandom.Next(wvColors.Count);
                        wvColor = wvColors[wvRandom];
                        #endregion


                        wvProperty.SetProperty(pk, wvColor);
                        break;
                    case E_PropertiesKind.GRADIATION:
                        wvRandom = attRandom.Next(1 + (int)E_Gradiation._NULL, (int)E_Gradiation._END);
                        wvGradiation = (E_Gradiation)wvRandom;
                        wvProperty.SetProperty(pk, wvGradiation);
                        break;
                    case E_PropertiesKind.SHAPE:
                        wvRandom = attRandom.Next(1 + (int)E_Shape._NULL, (int)E_Shape._END);
                        wvShape = (E_Shape)wvRandom;
                        wvProperty.SetProperty(pk, wvShape);
                        break;
                    default: //TEXTURE
                        wvRandom = attRandom.Next(1 + (int)E_Texture._NULL, (int)E_Texture._END);
                        wvTexture = (E_Texture)wvRandom;
                        wvProperty.SetProperty(pk, wvTexture);
                        break;

                }
                wvAbstractItem.AddProperty(wvProperty);
            }

            return wvAbstractItem;
        }

        private List<Item> GenerateItemsByItemKind(E_ItemKind wvItemKind)
        {
            //AbstractItem wvAbstractItem = new AbstractItem();
            //wvAbstractItem.AddProperty();
            throw new NotImplementedException("Finire la generazione random");
        }

        /*
        /// <summary>
        /// Return a <see cref="Clue"/> generated starting from the specified <see cref="E_ItemKind"/>
        /// </summary>
        /// <param name="ItemKind">The kind of item to be use to generate the clue</param>
        /// <returns></returns>
        private static Clue GenerateCorrectClue(E_ItemKind ItemKind)
        {
            E_Gradiation wvGradiation = (E_Gradiation)attRandom.Next(1 + (int)E_Gradiation._NULL, 1 + (int)E_Gradiation.DARK);
            E_Shape wvShape = (E_Shape)attRandom.Next(1 + (int)E_Shape._NULL, 1 + (int)E_Shape.LONG);
            E_Color wvColor = (E_Color)attRandom.Next(1 + (int)E_Color._NULL, 1 + (int)E_Color.YELLOW);
            E_Texture wvTexture = (E_Texture)attRandom.Next(1 + (int)E_Texture._NULL, 1 + (int)E_Texture.PLAINCOLOR);
            
            if (ItemKind != E_ItemKind.trousers && ItemKind != E_ItemKind.t_shirt)
            {
                int wvNull = attRandom.Next(4);

                switch (wvNull)
                {
                    case 0:
                        wvGradiation = E_Gradiation._NULL;
                        break;
                    case 1:
                        wvShape = E_Shape._NULL;
                        break;
                    case 2:
                        wvColor = E_Color._NULL;
                        break;
                    default:
                        wvTexture = E_Texture._NULL;
                        break;

                }
            }
            else
            {
                wvShape = E_Shape._NULL;
            }
            
            return new Clue(true, wvGradiation, wvShape, wvColor, wvTexture);
        }
        */
        public static void DeleteAllGames()
        {
            System.IO.DirectoryInfo wvDirInfo = new System.IO.DirectoryInfo(Helpers.ResourcesHelper.SavesDirectory);
            System.IO.FileInfo[] wvFileInfos = wvDirInfo.GetFiles();

            foreach (System.IO.FileInfo wvFile in wvFileInfos)
                wvFile.Delete();
        }
        #endregion

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

        private string CombinePath(string Path1, string Path2)
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
