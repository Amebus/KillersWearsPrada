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
        private const int NUMBER_OF_ROOMS = 3;

        private static Random attRandom;
        private static Helpers.DBHelper attDataBase;
        private Game attGame;
        private KinectInterrogator attKinectInterrogator;

        private ResumeGame attResumeGame;
        private UnloadGame attUnloadGame;
        
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



        /// <summary>
        /// Create a new <see cref="Model.Game"/> and a new <see cref="Model.Player"/> and save them into a file
        /// </summary>
        /// <param name="PlayerName">Name of the <see cref="Model.Player"/></param>
        public static void CreateGameAndPlayer(string PlayerName)
        {
            string wvPath = Helpers.ResourcesHelper.SavesDirectory;
            string wvID="";// = DateTime.Now.ToString();

            //wvID = wvID.Replace(' ', '-');
            wvID += ("-" + PlayerName);
            Model.Game wvGame = new Model.Game(wvID, PlayerName);

            CreateGame(ITEMS_PER_ROOM);

            wvPath = System.IO.Path.Combine(wvPath, wvID);
            Helpers.SerializerHelper.Serialize(wvPath, wvGame);

            //throw new NotImplementedException("Implementare qui la stampa dei QRCODE");
        }

        #region Generation, inizialization and elimination of the games
        /// <summary>
        /// Generate a list of <see cref="Item,"/> to be put in a <see cref="Room"/>
        /// </summary>
        /// <param name="NumberOfItems"></param>
        /// <returns></returns>
        private static void CreateGame(int NumberOfItems)
        {
            Solution wvSolution = new Solution();
            List<Item> wvItems = new List<Item>();
            Clue wvCorrectClue;
            E_ItemKind wvItemKind;
            Item wvCorrectItem;
            List<Clue> wvWrongClues;

            wvItemKind = E_ItemKind.hat;
            wvCorrectClue = GenerateCorrectClue(wvItemKind);
            //wvCorrectItem = attDataBase.GetItemFromClues(wvCorrectClue, wvItemKind);
            //wvSolution.AddItem(wvCorrectClue, wvCorrectItem);
            
            wvWrongClues = GenerateCluesFromCorrectClue(wvCorrectClue, ITEMS_PER_ROOM - 1);

            //Room r = new Room()


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
            
        }

        /// <summary>
        /// Return a <see cref="Clue"/> generated starting from the specified <see cref="E_ItemKind"/>
        /// </summary>
        /// <param name="ItemKind">The kind of item to be use to generate the clue</param>
        /// <returns></returns>
        private static Clue GenerateCorrectClue(E_ItemKind ItemKind)
        {
            E_Gradiation wvGradiation = (E_Gradiation)attRandom.Next(1 + (int)E_Gradiation.NULL, 1 + (int)E_Gradiation.DARK);
            E_Shape wvShape = (E_Shape)attRandom.Next(1 + (int)E_Shape.NULL, 1 + (int)E_Shape.LONG);
            E_Color wvColor = (E_Color)attRandom.Next(1 + (int)E_Color.NULL, 1 + (int)E_Color.YELLOW);
            E_Texture wvTexture = (E_Texture)attRandom.Next(1 + (int)E_Texture.NULL, 1 + (int)E_Texture.PLAINCOLOR);
            
            if (ItemKind != E_ItemKind.trousers && ItemKind != E_ItemKind.t_shirt)
            {
                int wvNull = attRandom.Next(4);

                switch (wvNull)
                {
                    case 0:
                        wvGradiation = E_Gradiation.NULL;
                        break;
                    case 1:
                        wvShape = E_Shape.NULL;
                        break;
                    case 2:
                        wvColor = E_Color.NULL;
                        break;
                    default:
                        wvTexture = E_Texture.NULL;
                        break;

                }
            }
            else
            {
                wvShape = E_Shape.NULL;
            }
            
            return new Clue(true, wvGradiation, wvShape, wvColor, wvTexture);
        }

        private static List<Clue> GenerateCluesFromCorrectClue(Clue CorrectClue, int Times)
        {
            List<Clue> wvClues = new List<Clue>();
            Clue wvTemp;
            wvClues.Add(GenerateFirstIncorrectClue(CorrectClue));
            bool wvSecondClueOk = true; 
            for (int i = 1; i < Times; i++)
            {
                do
                {
                    wvSecondClueOk = true;
                    wvTemp = GenerateSecondsIncorrectClue(CorrectClue, wvClues[0]);
                    
                    for (int j = 1; j<wvClues.Count; j++)
                    {
                        if(wvTemp.EqualsTo(wvClues[j]))
                        {
                            wvSecondClueOk = false;
                            break;
                        }
                    }


                } while (!wvSecondClueOk);


                wvClues.Add(wvTemp);
            }


            return wvClues;
        }

        private static Clue GenerateFirstIncorrectClue(Clue Correct)
        {
            List<E_PropertiesKind> wvProperties = new List<E_PropertiesKind>();
            List<E_Texture> wvTextures = new List<E_Texture>();
            List<E_Color> wvColors = new List<E_Color>();

            E_Gradiation wvGradiation = Correct.Gradiation;
            E_Shape wvShape = Correct.Shape;
            E_Color wvColor = Correct.Color;
            E_Texture wvTexture = Correct.Texture;

            if (Correct.Gradiation != E_Gradiation.NULL)
                wvProperties.Add(E_PropertiesKind.GRADIATION);
            if (Correct.Shape != E_Shape.NULL)
                wvProperties.Add(E_PropertiesKind.SHAPE);
            if (Correct.Color != E_Color.NULL)
            {
                wvProperties.Add(E_PropertiesKind.COLOR);

                Array array = Enum.GetValues(typeof(E_Color));
                foreach (E_Color t in array)
                {
                    if (t != E_Color.NULL && t != Correct.Color)
                        wvColors.Add(t);
                }
            }
            if (Correct.Texture != E_Texture.NULL)
            {
                wvProperties.Add(E_PropertiesKind.TEXTURE);

                Array array = Enum.GetValues(typeof(E_Texture));
                foreach (E_Texture t in array)
                {
                    if (t != E_Texture.NULL && t != Correct.Texture)
                        wvTextures.Add(t);
                }
            }

            int wvNull = attRandom.Next(wvProperties.Count);
            int wvIndex = 0;

            switch (wvProperties[wvNull])
            {
                case E_PropertiesKind.GRADIATION:
                    wvGradiation = (Correct.Gradiation == E_Gradiation.DARK) ? E_Gradiation.LIGHT : E_Gradiation.DARK;
                    break;
                case E_PropertiesKind.SHAPE:
                    wvShape = (Correct.Shape == E_Shape.SHORT) ? E_Shape.LONG : E_Shape.SHORT;
                    break;
                case E_PropertiesKind.COLOR:
                    wvIndex = attRandom.Next(wvColors.Count);
                    wvColor = wvColors[wvIndex];
                    break;
                case E_PropertiesKind.TEXTURE:
                    wvIndex = attRandom.Next(wvTextures.Count);
                    wvTexture = wvTextures[wvIndex];
                    break;
            }

            return new Clue(true, wvGradiation, wvShape, wvColor, wvTexture);

        }

        private static Clue GenerateSecondsIncorrectClue( Clue Correct, Clue FirsIncorrectClue)
        {
            List<E_PropertiesKind> wvProperties = new List<E_PropertiesKind>();
            List<E_Texture> wvTextures = new List<E_Texture>();
            List<E_Color> wvColors = new List<E_Color>();

            E_Gradiation wvGradiation= Correct.Gradiation;
            E_Shape wvShape = Correct.Shape;
            E_Color wvColor = Correct.Color;
            E_Texture wvTexture = Correct.Texture;

            if (Correct.Gradiation != E_Gradiation.NULL && Correct.Gradiation==FirsIncorrectClue.Gradiation)
                wvProperties.Add(E_PropertiesKind.GRADIATION);
            if (Correct.Shape != E_Shape.NULL && Correct.Shape==FirsIncorrectClue.Shape)
                wvProperties.Add(E_PropertiesKind.SHAPE);
            if (Correct.Color != E_Color.NULL)
            {
                wvProperties.Add(E_PropertiesKind.COLOR);

                Array array = Enum.GetValues(typeof(E_Color));
                foreach (E_Color t in array)
                {
                    if (t != E_Color.NULL && t != Correct.Color && t!=FirsIncorrectClue.Color)
                        wvColors.Add(t);
                }
            }
            if (Correct.Texture != E_Texture.NULL)
            {
                wvProperties.Add(E_PropertiesKind.TEXTURE);

                Array array = Enum.GetValues(typeof(E_Texture));
                foreach (E_Texture t in array)
                {
                    if (t != E_Texture.NULL && t != Correct.Texture && t!= FirsIncorrectClue.Texture)
                        wvTextures.Add(t);
                }
            }

            int wvNull = 0;
            int wvIndex = 0;

            for (int i = 0; i < 2; i++)
            {
                wvNull = attRandom.Next(wvProperties.Count);
                switch (wvProperties[wvNull])
                {
                    case E_PropertiesKind.GRADIATION:
                        wvGradiation = (Correct.Gradiation == E_Gradiation.DARK) ? E_Gradiation.LIGHT : E_Gradiation.DARK;
                        break;
                    case E_PropertiesKind.SHAPE:
                        wvShape = (Correct.Shape == E_Shape.SHORT) ? E_Shape.LONG : E_Shape.SHORT;
                        break;
                    case E_PropertiesKind.COLOR:
                        wvIndex = attRandom.Next(wvColors.Count);
                        wvColor = wvColors[wvIndex];
                        wvColors.RemoveAt(wvIndex);
                        break;
                    case E_PropertiesKind.TEXTURE:
                        wvIndex = attRandom.Next(wvTextures.Count);
                        wvTexture = wvTextures[wvIndex];
                        wvTextures.RemoveAt(wvIndex);
                        break;

                }
                wvProperties.RemoveAt(wvNull);

            }

            return new Clue(true, wvGradiation, wvShape, wvColor, wvTexture);

        }

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
