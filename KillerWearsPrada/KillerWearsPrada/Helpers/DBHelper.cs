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
        private String connectionString;
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
        public String TestConnection()
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

        #region Items
        /// <summary>
        /// This methods implements a query over the db that given some parameters returns a random item
        /// </summary>
        /// <param name="long1"> contains the shape  - look Enum</param>
        /// <param name="light">contains the gradiation - look Enum </param>
        /// <param name="textureKind"> contains the kind of texture needed - look Enum -</param>
        /// <param name="itemKind"> contains the kind of item you need - look Enum </param>
        /// <param name="color"> contains the predominant color - look Enum </param>
        /// <returns> an Item object that contains, in order : 
        ///         1. Item code
        ///         2. Barcode
        ///         3. Item Name
        ///         4. Item Price
        ///         5. Item Description
        ///         6. Item Reparto
        ///         7. Texture file name
        ///         8. Mask file name
        /// </returns>
        public Item GetItemFromClues(E_Shape long1, E_Gradiation light, E_Texture textureKind, E_Color color, E_ItemKind itemKind){

            DBConnection.Open();
            // note : we want items of which we have more than 10 available 
            string query = "SELECT TOP 1 I.ID, I.Barcode, II.ItemName, II.Price, II.Description, II.Reparto, T.FileName, M.FileName, I.Image";
            query += " FROM Item AS I ,Texture AS T, Mask AS M, ItemKind AS IK, TextureKind AS TK, ItemInfo AS II";
            query += " WHERE II.ItemKind = IK.ID AND II.Mask = M.ID AND I.ItemInfo = II.ID AND I.Texture = T.ID AND T.TextureKind = TK.ID";
            query += " AND I.Available > 10 AND II.Long = @p1 AND T.Light = @p2 AND TK.TextureKind = @p3 AND T.MainColor = @p4 AND IK.ItemKind = @p5";
            query += " ORDER BY rnd(C.ID)";

            // parameter @p1 - shape
            if (long1 == E_Shape.LUNGO)
            {
                query = query.Replace("@p1", true.ToString());
            }
            else
            {
                query = query.Replace("@p1", false.ToString());
            }

            // parameter @p2 - gradiation
            if (light == E_Gradiation.CHIARO)
            {
                query = query.Replace("@p2", true.ToString());
            }
            else
            {
                query = query.Replace("@p2", false.ToString());
            }

            // parameter @p3 - texture kind 
            query = query.Replace("@p3", "\'" + textureKind.ToString() + "\'");

            // parameter @p4 - main color 
            query = query.Replace("@p4", "\'" + color.ToString() + "\'");

            // parameter @p5 - item kind 
            query = query.Replace("@p5", "\'" + itemKind.ToString() + "\'");



            OleDbCommand command = new OleDbCommand(query, DBConnection);

            OleDbDataReader result = command.ExecuteReader();

            result.Read();

            int codice = result.GetInt32(0);
            String barcode = result.GetString(1);
            String name = result.GetString(2);
            Double price = result.GetDouble(3);
            String descr = result.GetString(4);
            String rep = result.GetString(5);
            String texture = result.GetString(6);
            String mask = result.GetString(7);
            String image = result.GetString(8);
            

            Item i = new Item(codice, barcode, name, price, descr, rep, texture, mask, image);

            DBConnection.Close();

            return i;
        }

        // test method
        /// <summary>
        /// This methods implements a query over the db that given the item shape (long vs short) returns a random item
        /// </summary>
        /// <param name="long1"> tells if an item il LONG or SHORT</param>        
        /// <param name="itemKind">  containing the kind of item you need</param>
        /// <returns> an Item object that contains, in order : 
        ///         1. Item code
        ///         2. Barcode
        ///         3. Item Name
        ///         4. Item Price
        ///         5. Item Description
        ///         6. Item Reparto
        ///         7. Texture file name
        ///         8. Mask file name
        ///         9. Image file name
        /// </returns>
        public Item GetItemByShape(E_Shape long1, E_ItemKind itemKind)
        {

            DBConnection.Open();

            // note : we want items of which we have more than 10 available 

            string query = "SELECT TOP 1 I.ID, I.Barcode, II.ItemName, II.Price, II.Description, II.Reparto, T.FileName, M.FileName, I.Image";
            query += " FROM Item AS I ,Texture AS T, Mask AS M, ItemKind AS IK, TextureKind AS TK, ItemInfo AS II";
            query += " WHERE II.ItemKind = IK.ID AND II.Mask = M.ID AND I.ItemInfo = II.ID AND I.Texture = T.ID AND T.TextureKind = TK.ID";
            query += " AND I.Available > 10 AND II.Long = @shape AND IK.ItemKind = @kind";
            query += " ORDER BY rnd(C.ID)";

            // parameter @shape
            if (long1 == E_Shape.LUNGO)
            {
                query = query.Replace("@shape", true.ToString());
            }
            else
            {
                query = query.Replace("@shape", false.ToString());
            }
                
            // parameter @kind   - item kind
            query = query.Replace("@kind", "\'" + itemKind.ToString() + "\'");
            
            OleDbCommand command = new OleDbCommand(query, DBConnection);

            OleDbDataReader result = command.ExecuteReader();
            result.Read();

            int codice = result.GetInt32(0);
            String barcode = result.GetString(1);
            String name = result.GetString(2);
            Double price = result.GetDouble(3);
            String descr = result.GetString(4);
            String rep = result.GetString(5);
            String texture = result.GetString(6);
            String mask = result.GetString(7);
            String image = result.GetString(8);
            

            Item i = new Item(codice, barcode, name, price , descr, rep, texture , mask, image);

            DBConnection.Close();

            return i;
        }


        // test method
        /// <summary>
        /// This methods implements a query over the db that given the item's gradation (light vs dark) returns a random item
        /// </summary>
        /// <param name="light">tells if an item is DARK or LIGHT </param>
        /// <param name="itemKind"> containing the kind of item you need</param>
        /// <returns> an Item object that contains, in order : 
        ///         1. Item code
        ///         2. Barcode
        ///         3. Item Name
        ///         4. Item Price
        ///         5. Item Description
        ///         6. Item Reparto
        ///         7. Texture file name
        ///         8. Mask file name
        ///         9. Image file name
        /// </returns>
        public Item GetItemByGradation(E_Gradiation light, E_ItemKind itemKind)
        {

            DBConnection.Open();
            // note : we want items of which we have more than 10 available 
            string query = "SELECT TOP 1 I.ID, I.Barcode, II.ItemName, II.Price, II.Description, II.Reparto, T.FileName, M.FileName, I.Image";
            query += " FROM Item AS I ,Texture AS T, Mask AS M, ItemKind AS IK, TextureKind AS TK, ItemInfo AS II";
            query += " WHERE II.ItemKind = IK.ID AND II.Mask = M.ID AND I.ItemInfo = II.ID AND I.Texture = T.ID AND T.TextureKind = TK.ID";
            query += " AND I.Available > 10 AND T.Light = @grad AND IK.ItemKind = @kind";
            query += " ORDER BY rnd(C.ID)";

            // parameter @p2 - gradiation
            if (light == E_Gradiation.CHIARO)
            {
                query = query.Replace("@grad", true.ToString());
            }
            else
            {
                query = query.Replace("@grad", false.ToString());
            }

            // parameter @kind  - item kind 
            query = query.Replace("@kind", "\'" + itemKind.ToString() + "\'");

            OleDbCommand command = new OleDbCommand(query, DBConnection);

            OleDbDataReader result = command.ExecuteReader();

            result.Read();

            int codice = result.GetInt32(0);
            String barcode = result.GetString(1);
            String name = result.GetString(2);
            Double price = result.GetDouble(3);
            String descr = result.GetString(4);
            String rep = result.GetString(5);
            String texture = result.GetString(6);
            String mask = result.GetString(7);
            String image = result.GetString(8);
            

            Item i = new Item(codice, barcode, name, price, descr, rep, texture, mask, image);

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
        public void SaveScoreForPlayer(String player, int score)
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
        public void AddPlayer(String dateTime, String username)
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
        public void RemovePlayer (String player)
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
