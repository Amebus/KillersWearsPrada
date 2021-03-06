﻿using System;
using System.Data.OleDb;
using System.Data.SqlClient;

using KillerWearsPrada.Model;
using System.Collections.Generic;

namespace KillerWearsPrada.Helpers
{
    /// <summary>
    /// This class managest the communication between Application and DataBase
    /// </summary>
    class DBHelper
    {
        private string connectionString;
        private OleDbConnection DBConnection;

        public DBHelper()
        {
            connectionString = Properties.Settings.Default.KWP_DB_Test_Connection;
            
            DBConnection = new OleDbConnection(connectionString);
        }

        /// <summary>
        /// This method tests the connectivity
        /// </summary>
        /// <returns>string that states the result of the try ("Succesfull cnnection" vs exception message)</returns>
        public string TestConnection()
        {
            try
            {
                DBConnection.Open();

            }
            catch (Exception e)
            {
                return e.Message;
            }

            DBConnection.Close();
            return "Successfull connection";
        }


        #region Prof
        private enum E_Items
        {
            AOne=19,
            BOne=18,
            ATwo=71,
            BTwo=68,
            AThree=127,
            BThree=121
        }

        public Game GetGameForProf()
        {
            string wvPlayerID = "15-01-2016-10-50-42_alberto";
            string wvPlayerName = "alberto";

            ItemGraficalProperty wvProperty = new ItemGraficalProperty();
            Solution wvSolution = new Solution();
            List<Room> wvRooms = new List<Room>();
            List<Item> wvItems;

            AbstractItem wvAbstract;
            
            wvRooms.Add(new Room(E_RoomsName.START_ROOM));

            #region Room = LIVINGROOM
            wvItems = new List<Item>();

            #region A -> ID 19
            wvAbstract = new AbstractItem(E_ItemType.A, E_ItemKind.TROUSERS);
            wvProperty.SetContent(E_PropertiesKind.COLOR, E_Color.BLUE);
            wvAbstract.AddProperty(wvProperty);

            wvProperty = new ItemGraficalProperty();
            wvProperty.SetContent(E_PropertiesKind.GRADIATION, E_Gradiation.LIGHT);
            wvAbstract.AddProperty(wvProperty);

            wvProperty = new ItemGraficalProperty();
            wvProperty.SetContent(E_PropertiesKind.TEXTURE, E_Texture.SCOTTISH);
            wvAbstract.AddProperty(wvProperty);

            wvItems.Add(GetItem(wvAbstract, E_Items.AOne));
            wvSolution.AddItem(wvAbstract);
            #endregion

            #region B One -> 18
            wvAbstract = new AbstractItem(E_ItemType.B, E_ItemKind.TROUSERS);
            wvProperty = new ItemGraficalProperty();
            wvProperty.SetContent(E_PropertiesKind.COLOR, E_Color.BLUE);
            wvAbstract.AddProperty(wvProperty);

            wvProperty = new ItemGraficalProperty();
            wvProperty.SetContent(E_PropertiesKind.GRADIATION, E_Gradiation.LIGHT);
            wvAbstract.AddProperty(wvProperty);

            wvProperty = new ItemGraficalProperty();
            wvProperty.SetContent(E_PropertiesKind.TEXTURE, E_Texture.PLAINCOLOR);
            wvAbstract.AddProperty(wvProperty);

            wvItems.Add(GetItem(wvAbstract, E_Items.BOne));
            #endregion

            wvRooms.Add(new Room(E_RoomsName.LIVINGROOM, wvItems));
            #endregion

            #region Room = KITCHEN
            wvItems = new List<Item>();

            #region A Two -> 71
            wvAbstract = new AbstractItem(E_ItemType.A, E_ItemKind.HAT);
            wvProperty = new ItemGraficalProperty();
            wvProperty.SetContent(E_PropertiesKind.COLOR, E_Color.GREEN);
            wvAbstract.AddProperty(wvProperty);

            wvProperty = new ItemGraficalProperty();
            wvProperty.SetContent(E_PropertiesKind.GRADIATION, E_Gradiation.DARK);
            wvAbstract.AddProperty(wvProperty);

            wvProperty = new ItemGraficalProperty();
            wvProperty.SetContent(E_PropertiesKind.TEXTURE, E_Texture.STRIPES);
            wvAbstract.AddProperty(wvProperty);

            wvSolution.AddItem(wvAbstract);
            wvItems.Add(GetItem(wvAbstract, E_Items.ATwo));
            #endregion

            #region B Two  -> 68
            wvAbstract = new AbstractItem(E_ItemType.B, E_ItemKind.HAT);
            wvProperty = new ItemGraficalProperty();
            wvProperty.SetContent(E_PropertiesKind.COLOR, E_Color.GREEN);
            wvAbstract.AddProperty(wvProperty);

            wvProperty = new ItemGraficalProperty();
            wvProperty.SetContent(E_PropertiesKind.GRADIATION, E_Gradiation.DARK);
            wvAbstract.AddProperty(wvProperty);

            wvProperty = new ItemGraficalProperty();
            wvProperty.SetContent(E_PropertiesKind.TEXTURE, E_Texture.POIS);
            wvAbstract.AddProperty(wvProperty);

            wvItems.Add(GetItem(wvAbstract, E_Items.BTwo));
            #endregion

            wvRooms.Add(new Room(E_RoomsName.KITCHEN, wvItems));
            #endregion

            #region Room = BEDROOM
            wvItems = new List<Item>();

            #region A Three  -> 127
            wvAbstract = new AbstractItem(E_ItemType.A, E_ItemKind.T_SHIRT);
            wvProperty = new ItemGraficalProperty();
            wvProperty.SetContent(E_PropertiesKind.COLOR, E_Color.PURPLE);
            wvAbstract.AddProperty(wvProperty);

            wvProperty = new ItemGraficalProperty();
            wvProperty.SetContent(E_PropertiesKind.GRADIATION, E_Gradiation.LIGHT);
            wvAbstract.AddProperty(wvProperty);

            wvProperty = new ItemGraficalProperty();
            wvProperty.SetContent(E_PropertiesKind.TEXTURE, E_Texture.POIS);
            wvAbstract.AddProperty(wvProperty);

            wvSolution.AddItem(wvAbstract);
            wvItems.Add(GetItem(wvAbstract, E_Items.AThree));
            #endregion

            #region B Three -> 121
            wvAbstract = new AbstractItem(E_ItemType.B, E_ItemKind.T_SHIRT);
            wvProperty = new ItemGraficalProperty();
            wvProperty.SetContent(E_PropertiesKind.COLOR, E_Color.PURPLE);
            wvAbstract.AddProperty(wvProperty);

            wvProperty = new ItemGraficalProperty();
            wvProperty.SetContent(E_PropertiesKind.GRADIATION, E_Gradiation.LIGHT);
            wvAbstract.AddProperty(wvProperty);

            wvProperty = new ItemGraficalProperty();
            wvProperty.SetContent(E_PropertiesKind.TEXTURE, E_Texture.FLOWERS);
            wvAbstract.AddProperty(wvProperty);

            wvItems.Add(GetItem(wvAbstract, E_Items.BThree));
            #endregion

            wvRooms.Add(new Room(E_RoomsName.BEDROOM, wvItems));
            #endregion

            //return null;
            return new Game(wvPlayerID, wvPlayerName, wvRooms, wvSolution);
        }

        private Item GetItem(AbstractItem AI, E_Items ID)
        {
            string wvBaseQuery = "SELECT TOP 1 I.ID, I.Barcode, II.ItemName, II.Price, II.Description, II.Reparto, T.FileName, I.Image";
            wvBaseQuery += " FROM Item AS I ,Texture AS T, ItemKind AS IK, TextureKind AS TK, ItemInfo AS II, Color AS C";
            wvBaseQuery += " WHERE II.ItemKind = IK.ID AND I.ItemInfo = II.ID AND I.Texture = T.ID AND T.TextureKind = TK.ID AND T.MainColor = C.ID";
            wvBaseQuery += " AND I.ID=";

            string query = wvBaseQuery + (int)ID;

            DBConnection.Open();
            OleDbCommand command = new OleDbCommand(query, DBConnection);

            OleDbDataReader result = command.ExecuteReader();

            result.Read();

            int codice = result.GetInt32(0);
            string barcode = result.GetString(1);
            string name = result.GetString(2);
            double price = result.GetDouble(3);
            string descr = result.GetString(4);
            string rep = result.GetString(5);
            string texture = result.GetString(6);
            string image = result.GetString(7);

            DBConnection.Close();
            Item i = new Item(codice, barcode, name, price, descr, rep, texture, image, AI);

            return i;
        }
        #endregion

        #region Items

        /// <summary>
        /// returns an item given a clue ad an item kind
        /// </summary>
        /// <param name="c">a clue object</param>
        /// <param name="itemKind">an itemkind</param>
        /// <returns> an Item object that contains, in order : 
        ///         1. Item code
        ///         2. Barcode
        ///         3. Item Name
        ///         4. Item Price
        ///         5. Item Description
        ///         6. Item Reparto
        ///         7. Texture file name
        ///         8. Image file name
        ///         9. item kind
        /// </returns>
        public Item GetItemFromAbstractItem(AbstractItem Item) //
        {
            try
            {
                DBConnection.Open();
            }
            catch
            {
                DBConnection.Close();
                DBConnection.Open();
            }

            // note : we want items of which we have more than 10 available 
            string query = "SELECT TOP 1 I.ID, I.Barcode, II.ItemName, II.Price, II.Description, II.Reparto, T.FileName, I.Image";
            query += " FROM Item AS I ,Texture AS T, ItemKind AS IK, TextureKind AS TK, ItemInfo AS II, Color AS C";
            query += " WHERE II.ItemKind = IK.ID AND I.ItemInfo = II.ID AND I.Texture = T.ID AND T.TextureKind = TK.ID AND T.MainColor = C.ID";
            query += " AND I.Available > 10 AND IK.ItemKind = @p5";

            string order = " ORDER BY Rnd(-(10000*I.ID)*Time())";

            string whereLong = " AND II.Long = @p1";
            string whereLight = " AND T.Light = @p2";
            string whereTexture = " AND TK.TextureKind = @p3";
            string whereColor = " AND C.Color = @p4";

            // parameter @p1 - shape
            if(Item.CheckPropertyByKind(E_PropertiesKind.SHAPE))
            {
                if (Item.GetProperty(E_PropertiesKind.SHAPE) == E_Shape.LONG.ToString())
                {
                    whereLong = whereLong.Replace("@p1", true.ToString());
                }
                else
                {
                    whereLong = whereLong.Replace("@p1", false.ToString());
                }
                query += whereLong;
            }

            // parameter @p2 - gradiation
            if (Item.CheckPropertyByKind(E_PropertiesKind.GRADIATION))
            {
                if (Item.GetProperty(E_PropertiesKind.GRADIATION) == E_Gradiation.LIGHT.ToString())
                {
                    whereLight = whereLight.Replace("@p2", true.ToString());
                }
                else
                {
                    whereLight = whereLight.Replace("@p2", false.ToString());
                }
                query += whereLight;
            }


            // parameter @p3 - texture kind 
            if (Item.CheckPropertyByKind(E_PropertiesKind.TEXTURE))
            {
                whereTexture = whereTexture.Replace("@p3", "\'" + Item.GetProperty(E_PropertiesKind.TEXTURE).ToLower() + "\'");
                query += whereTexture;
            }

            // parameter @p4 - main color 
            if (Item.CheckPropertyByKind(E_PropertiesKind.COLOR))
            {
                whereColor = whereColor.Replace("@p4", "\'" + Item.GetProperty(E_PropertiesKind.COLOR).ToLower() + "\'");
                query += whereColor;
            }
            
            

            // parameter @p5 - item kind 
            query = query.Replace("@p5", "\'" + Item.ItemKind.ToString().ToLower() + "\'");

            query += order;

            OleDbCommand command = new OleDbCommand(query, DBConnection);

            OleDbDataReader result = command.ExecuteReader();

            result.Read();

            int codice = result.GetInt32(0);
            string barcode = result.GetString(1);
            string name = result.GetString(2);
            double price = result.GetDouble(3);
            string descr = result.GetString(4);
            string rep = result.GetString(5);
            string texture = result.GetString(6);
            string image = result.GetString(7);


            Item i = new Item(codice, barcode, name, price, descr, rep, texture, image, Item);

            DBConnection.Close();

            return i;
        }


        /// <summary>
        /// this methods tells if an item is Long (given its code)
        /// </summary>
        /// <param name="code">an integere containing the item's code</param>
        /// <returns>true if the item is LONG, false otherwise</returns>
        public bool GetShape(int code) 
        {
            try
            {
                DBConnection.Open();
            }
            catch
            {
                DBConnection.Close();
                DBConnection.Open();
            }

            string query = "SELECT II.Long";
            query += " FROM Item AS I , ItemInfo AS II";
            query += " WHERE I.ItemInfo = II.ID AND I.ID = @code";                 

            // parameter @code - code
            
            query = query.Replace("@code", code.ToString());

            OleDbCommand command = new OleDbCommand(query, DBConnection);

            OleDbDataReader result = command.ExecuteReader();

            result.Read();

            bool shape = result.GetBoolean(0);
            DBConnection.Close();

            return shape;
        }

        /// <summary>
        /// this methods tells if an item is Long or short given its Barcode
        /// </summary>
        /// <param name="barcode">string containing the item's barcode</param>
        /// <returns>true if the shape il LONG, true otherwise</returns>
        public bool GetShape(string barcode)
        {
            try
            {
                DBConnection.Open();
            }
            catch
            {
                DBConnection.Close();
                DBConnection.Open();
            }

            string query = "SELECT II.Long";
            query += " FROM Item AS I , ItemInfo AS II";
            query += " WHERE I.ItemInfo = II.ID AND I.Barcode = @barcode";
            
            // parameter @barcodecode 

            query = query.Replace("@barcode","'" + barcode + "'");

            OleDbCommand command = new OleDbCommand(query, DBConnection);

            OleDbDataReader result = command.ExecuteReader();

            result.Read();

            bool shape = result.GetBoolean(0);
            DBConnection.Close();

            return shape;
        }



        #endregion

        #region Player
        /// <summary>
        /// this method updates the DB adding a score to a player
        /// </summary>
        /// <param name="player">string, player ID (datetime)</param>
        /// <param name="score">integer, player's score</param>
        public void SaveScoreForPlayer(string player, int score)
        {
            DBConnection.Open();

            string query = "UPDATE Player SET Score = @value WHERE DateTime = @id;";


            /*OleDbCommand command = new OleDbCommand(query, DBConnection);
            command.Parameters.Add("@value", OleDbType.Integer).Value = score;
            command.Parameters.Add("@id", OleDbType.Integer).Value = player;*/

            query = query.Replace("@value", score.ToString());
            query = query.Replace("@id", player);


            OleDbCommand command = new OleDbCommand(query, DBConnection);

            command.ExecuteNonQuery();

            DBConnection.Close();

        }

        /// <summary>
        /// this method inserts a new player in the DB
        /// </summary>
        /// <param name="dateTime">string, player ID (datetime)</param>
        /// <param name="username">string, player's username</param>
        public void AddPlayer(string dateTime, string username)
        {
            DBConnection.Open();

            string query = "INSERT INTO Player (Username,DateTime) VALUES (@username,@dateTime);";
            
            query = query.Replace("@username", username);
            query = query.Replace("@dateTime", dateTime);


            OleDbCommand command = new OleDbCommand(query, DBConnection);

            command.ExecuteNonQuery();

            DBConnection.Close();

        }
        /// <summary>
        /// this method removes a player from the DB
        /// </summary>
        /// <param name="player">string, player ID (datetime)</param>
        public void RemovePlayer (string player)
        {
            DBConnection.Open();
            
            string query = "DELETE * FROM  Player  WHERE DateTime = @id;";
                          
            query = query.Replace("@id", player);
            
            OleDbCommand command = new OleDbCommand(query, DBConnection);

            command.ExecuteNonQuery();

            DBConnection.Close();

        }

        #endregion


    }
}
