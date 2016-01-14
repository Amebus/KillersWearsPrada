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


        /// <summary>
        /// This methods implements a query over the db that given some parameters returns a random item
        /// </summary>
        /// <param name="long1"> boolean, true = long, false = short</param>
        /// <param name="light">boolean, true = dark, false = light </param>
        /// <param name="texture"> string containing the kind of texture needed - look Clue enum Texture -</param>
        /// <param name="itemKind"> string containing the kind of item you need</param>
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
        public Item GetItemFromClues(bool long1, bool light, string texture, string itemKind){

            DBConnection.Open();
            // note : we want items of which we have more than 10 available 
            string query = "SELECT TOP 1 C.ID,D.Barcode, D.Nome, D.Prezzo, D.Descrizione, D.Reparto, T.FileName, M.FileName, D.Immagine FROM Capo AS C,DatiNegozio AS D, Grafica AS G,Texture AS T, Maschera AS M, TipoCapo AS TC   WHERE C.DatiNegozio = D.ID AND C.Grafica = G.ID AND G.Texture = T.ID AND G.Maschera = M.ID AND TC.ID = C.Tipo AND D.Disponibilità > 10 AND (((D.[Lungo/Corto])=@p1)) AND (((T.[Chiaro/Scuro])=@p2)) AND T.TipoTexture = @p3 AND TC.Tipo = @p4 ORDER BY rnd(C.ID); ";
            //string query = "SELECT * FROM Capo";
            OleDbCommand command = new OleDbCommand(query, DBConnection);
            // add parameters
            // long parameter - @p1            
            command.Parameters.Add("@p1", OleDbType.Boolean).Value = long1;
            // dark parameter - @p2
            command.Parameters.Add("@p2", OleDbType.Boolean).Value = light;
            // texture parameter - @p3
            command.Parameters.Add("@p3", OleDbType.VarChar, 255).Value = texture;
            // item kind parameter -@p4
            command.Parameters.Add("@p4", OleDbType.VarChar, 255).Value = itemKind;

            OleDbDataReader result = command.ExecuteReader();

            Item i = new Item((int)result.GetValue(0),(int)result.GetValue(1), result.GetValue(2).ToString(), 
                (float)result.GetValue(3), result.GetValue(4).ToString(), result.GetValue(5).ToString(), 
                result.GetValue(6).ToString(), result.GetValue(7).ToString(),result.GetValue(8).ToString());

            DBConnection.Close();

            return i;
        }

        // test method
        /// <summary>
        /// This methods implements a query over the db that given the cloath shape (long vs short) returns a random item
        /// </summary>
        /// <param name="long1"> boolean, true = long, false = short</param>        
        /// <param name="itemKind"> string containing the kind of item you need</param>
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
        public Item GetItemByShape(bool long1, string itemKind)
        {

            DBConnection.Open();
            // note : we want items of which we have more than 10 available 
            string query = "SELECT TOP 1 C.ID,D.Barcode, D.Nome, D.Prezzo, D.Descrizione, D.Reparto, T.FileName, M.FileName, D.Immagine FROM Capo AS C,DatiNegozio AS D, Grafica AS G,Texture AS T, Maschera AS M, TipoCapo AS TC   WHERE C.DatiNegozio = D.ID AND C.Grafica = G.ID AND G.Texture = T.ID AND G.Maschera = M.ID AND TC.ID = C.Tipo AND D.Disponibilità > 10 AND (((D.[Lungo/Corto])=@p1)) AND TC.Tipo = @p4 ORDER BY rnd(C.ID); ";
            //string query = "SELECT * FROM Capo";
            OleDbCommand command = new OleDbCommand(query, DBConnection);
            // add parameters
            // long parameter - @p1            
            command.Parameters.Add("@p1", OleDbType.Boolean).Value = long1;
            
            // item kind parameter -@p4
            command.Parameters.Add("@p4", OleDbType.VarChar, 255).Value = itemKind;

            OleDbDataReader result = command.ExecuteReader();

            Item i = new Item((int)result.GetValue(0), (int)result.GetValue(1), result.GetValue(2).ToString(),
                (float)result.GetValue(3), result.GetValue(4).ToString(), result.GetValue(5).ToString(),
                result.GetValue(6).ToString(), result.GetValue(7).ToString(),result.GetValue(8).ToString());

            DBConnection.Close();

            return i;
        }


        // test method
        /// <summary>
        /// This methods implements a query over the db that given the gradation (light vs dark) returns a random item
        /// </summary>
        /// <param name="light">boolean, true = dark, false = light </param>
        /// <param name="itemKind"> string containing the kind of item you need</param>
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
        public Item GetItemByGradation(bool light, string itemKind)
        {

            DBConnection.Open();
            // note : we want items of which we have more than 10 available 
            string query = "SELECT TOP 1 C.ID,D.Barcode, D.Nome, D.Prezzo, D.Descrizione, D.Reparto, T.FileName, M.FileName, D.Immagine FROM Capo AS C,DatiNegozio AS D, Grafica AS G,Texture AS T, Maschera AS M, TipoCapo AS TC   WHERE C.DatiNegozio = D.ID AND C.Grafica = G.ID AND G.Texture = T.ID AND G.Maschera = M.ID AND TC.ID = C.Tipo AND D.Disponibilità > 10 AND (((T.[Chiaro/Scuro])=@p2)) TC.Tipo = @p4 ORDER BY rnd(C.ID); ";
            //string query = "SELECT * FROM Capo";
            OleDbCommand command = new OleDbCommand(query, DBConnection);
            // add parameters
            // dark parameter - @p2
            command.Parameters.Add("@p2", OleDbType.Boolean).Value = light;
            
            // item kind parameter -@p4
            command.Parameters.Add("@p4", OleDbType.VarChar, 255).Value = itemKind;

            OleDbDataReader result = command.ExecuteReader();

            Item i = new Item((int)result.GetValue(0), (int)result.GetValue(1), result.GetValue(2).ToString(),
                (float)result.GetValue(3), result.GetValue(4).ToString(), result.GetValue(5).ToString(),
                result.GetValue(6).ToString(), result.GetValue(7).ToString(), result.GetValue(8).ToString());

            DBConnection.Close();

            return i;
        }

        /// <summary>
        /// this method updates the DB adding a score to a player
        /// </summary>
        /// <param name="player">integer, player ID</param>
        /// <param name="score">integer, player's score</param>
        public void SaveScoreForPlayer (int player, int score)
        {
            DBConnection.Open();
            
            string query = "UPDATE Utente SET Punteggio = @value WHERE ID = @id;";
            OleDbCommand command = new OleDbCommand(query, DBConnection);
            command.Parameters.Add("@value", OleDbType.Integer).Value = score;
            command.Parameters.Add("@id", OleDbType.Integer).Value = player;

            command.ExecuteNonQuery();

            DBConnection.Close();

        }
        
    }
}
