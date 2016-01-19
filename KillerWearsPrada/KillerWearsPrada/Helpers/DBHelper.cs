using System;
using System.Data.OleDb;
using System.Data.SqlClient;

using KillerWearsPrada.Model;

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
        public Game GetGameForProf()
        {
            string wvPlayerID = "15/01/2016-10:50:42-alberto";
            string wvPlayerName = "alberto";








            return null;
            //return new Game(wvPlayerID, wvPlayerName, );
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
