using System;
using System.Collections.Generic;
using KillerWearsPrada.Model;

namespace KillerWearsPrada.Controller
{
    public class GameController
    {
        private const int REFRESH_TIME = 3;
        private const int DEBUG_REFRESH_TIME = 10000;
        private const int ITEMS_PER_ROOM = 2;
        private const int NUMBER_OF_ROOMS_WITH_ITEMS = 3;

        private Random attRandom;
        private Helpers.DBHelper attDataBase;
        private Game attGame;
        private KinectInterrogator attKinectInterrogator;

        private ResumeGame attResumeGame;
        private UnloadGame attUnloadGame;
        private UpdateInventory attUpdateInventory;
        private NotifyItemException attNotifyItemException;

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
            attRandom = new Random(0);
            attKinectInterrogator = new KinectInterrogator( KinectSensor, REFRESH_TIME );

            attDataBase = new Helpers.DBHelper();  

            attResumeGame = new ResumeGame();
            attUnloadGame = new UnloadGame();
            attUpdateInventory = new UpdateInventory();
            attNotifyItemException = new NotifyItemException();

            attKinectInterrogator.PlayerEnterKinectSensorEvent = HandlePlayerEnterKinectSensor;
            attKinectInterrogator.PlayerLeaveKinectSensorEvent = HandlePlayerLeaveKinectSensor;
            attKinectInterrogator.BarCodeRecognizedEvent = HandleBarCodeRecognized;
        }

        #region Proprietà
        public Model.Game Game
        {
            get { return attGame; }
        }

        public NotifyItemException GetNotifyItemException
        {
            get { return attNotifyItemException; }
        }

        public UpdateInventory GetUpdateInventory
        {
            get { return attUpdateInventory; }
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

        internal void CreateProfGame()
        {
            Game wvGame = attDataBase.GetGameForProf();

            string wvPath = Helpers.ResourcesHelper.SavesDirectory;
            wvPath = CombinePath(wvPath, wvGame.PlayerID);
            Helpers.SerializerHelper.Serialize(wvPath, wvGame);

        }
        
        #endregion

        /// <summary>
        /// Set a value that the Game has been started
        /// </summary>
        public void SetGameStarted ()
        {
            attGame.SetAsStarted();
        }

        public void SetGameFinished()
        {
            attGame.SetAsFinished();
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
            attGame = Helpers.SerializerHelper.Deserialize(wvPath);
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
            string wvID= DateTime.Now.ToString();

            wvID = wvID.Replace(' ', '-');
            wvID = wvID.Replace('/', '-');
            wvID = wvID.Replace(':', '-');
            wvID += ("_" + PlayerName);
            //Game wvGame = new Game(wvID, PlayerName);

            attGame = CreateGame(wvID, PlayerName, ITEMS_PER_ROOM);

            wvPath = System.IO.Path.Combine(wvPath, wvID);
            Helpers.SerializerHelper.Serialize(wvPath, attGame);

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

            wvRooms.Add(new Room(E_RoomsName.START_ROOM));

            for (int i = 1; i <= NUMBER_OF_ROOMS_WITH_ITEMS; i++)
            {
                AbstractItem wvCorrectAbstractItem = GenerateCorrectAbstractItem((E_ItemKind._NULL + i));
                Item wvCorrectItem = attDataBase.GetItemFromAbstractItem(wvCorrectAbstractItem);

                wvSolution.AddItem(wvCorrectItem);

                wvRooms.Add(GenerateRoom((E_RoomsName)(i+1), wvCorrectItem));

            }

            return new Game(ID, PalyerName, wvRooms, wvSolution);
            
        }
        
        private Room GenerateRoom(E_RoomsName Name, Item CorrectItem)
        {
            Room wvRoom = null;
            List<Item> wvItems = new List<Item>();
            wvItems.Add(CorrectItem);

            for (int i = (int)E_ItemType.A + 1; i<= (int)E_ItemType.F; i++)
            {
                AbstractItem wvAbstractItem = new AbstractItem((E_ItemType)i, CorrectItem.ItemKind);

                wvAbstractItem = InvertByItemType(CorrectItem, (E_ItemType)i);

                wvItems.Add(attDataBase.GetItemFromAbstractItem(wvAbstractItem));

            }

            wvRoom = new Room(Name, wvItems);
            return wvRoom;
        }
        
        private AbstractItem InvertByItemType(AbstractItem CorrectItem, E_ItemType ItemTypeToGenerate)
        {
            ItemGraficalProperty wvIgp;
            AbstractItem wvInvertedAbstractItem = new AbstractItem(ItemTypeToGenerate, CorrectItem.ItemKind);
            bool[] wvVctor = attPopulationMatrix[(int)ItemTypeToGenerate];

            for(int i = 0;  i<CorrectItem.PropertiesCount; i++)
            {
                
                ItemGraficalProperty wvProperty = new ItemGraficalProperty();
                wvIgp = CorrectItem.ItemProperties[i];
                if(wvVctor[i])
                {
                    wvProperty.SetContent(wvIgp.PropertyKind, wvIgp.Content);
                }
                else
                {

                    wvProperty.SetContent(InvertProperty(wvIgp));
                    
                }

                wvInvertedAbstractItem.AddProperty(wvProperty);
            }

            return wvInvertedAbstractItem;   
        }

        private ItemGraficalProperty InvertProperty(ItemGraficalProperty IGP)
        {
            int wvRandom;
            ItemGraficalProperty wvProperty = new ItemGraficalProperty();

            switch (IGP.PropertyKind)
            {
                case E_PropertiesKind.COLOR:

                    #region Color from Gradiation
                    List<E_Color> wvColors = new List<E_Color>();
                    if (IGP.PropertyKind == E_PropertiesKind.GRADIATION)
                    {
                        for (int j = (int)E_Color._NULL + 1; j < (int)E_Color._END; j++)
                        {
                            if (((E_Color)j).ToString() != IGP.Content.ToString())
                                wvColors.Add((E_Color)j);
                        }
                    }
                    else
                    {
                        for (int j = (int)E_Color._NULL + 1; j < (int)E_Color._END; j++)
                        {
                            if (
                                    (E_Color)j != E_Color.BLACK &&
                                    (E_Color)j != E_Color.WHITE &&
                                    ((E_Color)j).ToString() != IGP.Content.ToString())
                                wvColors.Add((E_Color)j);
                        }
                    }
                    wvRandom = attRandom.Next(wvColors.Count);
                    wvProperty.SetContent(IGP.PropertyKind, wvColors[wvRandom]);
                    #endregion
                    
                    break;
                case E_PropertiesKind.GRADIATION:
                    E_Gradiation wvGradiation = E_Gradiation.DARK;

                    if (IGP.Content.ToString() == E_Gradiation.DARK.ToString())
                        wvGradiation = E_Gradiation.LIGHT;
                    wvProperty.SetContent(IGP.PropertyKind, wvGradiation);
                    break;
                case E_PropertiesKind.SHAPE:
                    E_Shape wvShape = E_Shape.LONG;

                    if (IGP.Content.ToString() == E_Shape.LONG.ToString())
                        wvShape = E_Shape.SHORT;
                    wvProperty.SetContent(IGP.PropertyKind, wvShape);
                    break;
                case E_PropertiesKind.TEXTURE:
                    List<E_Texture> wvTextures = new List<E_Texture>();

                    for (int j = (int)E_Texture._NULL + 1; j < (int)E_Texture._END; j++)
                    {
                        if (((E_Texture)j).ToString() !=IGP.Content.ToString())
                            wvTextures.Add((E_Texture)j);
                    }

                    wvRandom = attRandom.Next(wvTextures.Count);
                    wvProperty.SetContent(IGP.PropertyKind, wvTextures[wvRandom]);
                    break;

                default:
                    break;
            }

            return wvProperty;
        }

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


                        wvProperty.SetContent(pk, wvColor);
                        break;
                    case E_PropertiesKind.GRADIATION:
                        wvRandom = attRandom.Next(1 + (int)E_Gradiation._NULL, (int)E_Gradiation._END);
                        wvGradiation = (E_Gradiation)wvRandom;
                        wvProperty.SetContent(pk, wvGradiation);
                        break;
                    case E_PropertiesKind.SHAPE:
                        wvRandom = attRandom.Next(1 + (int)E_Shape._NULL, (int)E_Shape._END);
                        wvShape = (E_Shape)wvRandom;
                        wvProperty.SetContent(pk, wvShape);
                        break;
                    default: //TEXTURE
                        wvRandom = attRandom.Next(1 + (int)E_Texture._NULL, (int)E_Texture._END);
                        wvTexture = (E_Texture)wvRandom;
                        wvProperty.SetContent(pk, wvTexture);
                        break;

                }
                wvAbstractItem.AddProperty(wvProperty);
            }

            return wvAbstractItem;
        }

        public static void DeleteAllGames()
        {
            System.IO.DirectoryInfo wvDirInfo = new System.IO.DirectoryInfo(Helpers.ResourcesHelper.SavesDirectory);
            System.IO.FileInfo[] wvFileInfos = wvDirInfo.GetFiles();

            foreach (System.IO.FileInfo wvFile in wvFileInfos)
                wvFile.Delete();
        }
        #endregion

        private void HandleBarCodeRecognized(object Sender, BarCodeRecognized.Arguments Parameters)
        {

            try
            {
                attGame.SetInInventory(Parameters.BarCode);
                attUpdateInventory.RaiseEvent(Parameters.BarCode, attGame.ActualRoomIndex);
            }
            catch (ItemAlreadyInInvetory ex)
            {
                attNotifyItemException.RaiseEvent(ex.Message);
            }
            catch (ItemNotInGameException ex)
            {
                attNotifyItemException.RaiseEvent(ex.Message);
            }
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="Parameters"></param>
        private void HandlePlayerEnterKinectSensor(object Sender, PlayerChecker.PlayerEnterKinectSensor.Arguments Parameters)
        {
            LoadGame(Parameters.ID);
            if (attGame.IsFinished)
                return;
            attResumeGame.RaiseEvent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandlePlayerLeaveKinectSensor(object Sender, PlayerChecker.PlayerLeaveKinectSensor.Arguments Parameters)
        {
            //throw new NotImplementedException("Implementare la logica che gestisce il momento in cui il giocatore lascia la postazione");

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
            public event EventHandler<Arguments> ResumeGameEvent;

            protected virtual void OnResumeGame(Arguments e)
            {
                
                if (ResumeGameEvent != null)
                {
                    ResumeGameEvent(this, e);
                }
            }

            /// <summary>
            /// 
            /// </summary>
            public void RaiseEvent()
            {
                //TODO mettere l'ID del giocatore entrato
                Arguments wvParameters = new Arguments();


                OnResumeGame(wvParameters);
            }

            /// <summary>
            /// 
            /// </summary>
            public class Arguments : EventArgs
            {

            }
        }

        /// <summary>
        /// Event that occur when the game is unload
        /// </summary>
        public class UnloadGame
        {
            public event EventHandler<Arguments> UnloadGameEvent;

            protected virtual void OnUnloadGame(Arguments e)
            {
                if (UnloadGameEvent != null)
                {
                    UnloadGameEvent(this, e);
                }
            }

            /// <summary>
            /// 
            /// </summary>
            public void RaiseEvent()
            {
                //TODO mettere l'ID del giocatore entrato
                Arguments wvParameters = new Arguments();


                OnUnloadGame(wvParameters);
            }

            public class Arguments : EventArgs
            {

            }
        }


        public class UpdateInventory
        {
            public event EventHandler<Arguments> UpdateInventoryEvent;

            protected virtual void OnUpdateInventory(Arguments e)
            {
                if (UpdateInventoryEvent != null)
                    UpdateInventoryEvent(this, e);

            }

            public void RaiseEvent(string BarCode, int RoomIndex)
            {
                Arguments wvParameters = new Arguments(BarCode, RoomIndex);

                OnUpdateInventory(wvParameters);
            }

            public class Arguments : EventArgs
            {
                public Arguments(string BarCode)
                {
                    this.BarCode = BarCode;
                    this.RoomIndex = -1;
                }

                public Arguments (string BarCode, int RoomIndex)
                {
                    this.BarCode = BarCode;
                    this.RoomIndex = RoomIndex;
                }

                public string BarCode { get; private set; }

                public int RoomIndex { get; private set; }
            }
        }

        public class NotifyItemException
        {
            public event EventHandler<Arguments> NotifyItemExceptionEvent;

            protected virtual void OnNotifyItemException(Arguments e)
            {
                if (NotifyItemExceptionEvent != null)
                    NotifyItemExceptionEvent(this, e);
            }

            public void RaiseEvent(string Messagge)
            {
                Arguments wvParameters = new Arguments(Messagge);

                OnNotifyItemException(wvParameters);
            }

            public class Arguments : EventArgs
            {
                public Arguments(string Message)
                {
                    this.Messagge = Messagge;
                }

                public string Messagge {get; private set;}
            }
        }
    }
}
