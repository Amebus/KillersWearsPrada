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
        public Item GetItemFromClues(E_Shape long1, E_Gradiation light, E_Texture texture, E_ItemKind itemKind){

            DBConnection.Open();
            // note : we want items of which we have more than 10 available 
            string query = "SELECT TOP 1 C.ID,D.Barcode, D.Nome, D.Prezzo, D.Descrizione, D.Reparto, T.FileName, M.FileName, D.Immagine FROM Capo AS C,DatiNegozio AS D, Grafica AS G,Texture AS T, Maschera AS M, TipoCapo AS TC   WHERE C.DatiNegozio = D.ID AND C.Grafica = G.ID AND G.Texture = T.ID AND G.Maschera = M.ID AND TC.ID = C.Tipo AND D.Disponibilità > 10 AND (((D.[Lungo/Corto])=@p1)) AND (((T.[Chiaro/Scuro])=@p2)) AND T.TipoTexture = @p3 AND TC.Tipo = @p4 ORDER BY rnd(C.ID); ";
            //string query = "SELECT * FROM Capo";
            OleDbCommand command = new OleDbCommand(query, DBConnection);
            // add parameters
            // long parameter - @p1  
            Boolean shape = false;
            if (long1 == E_Shape.LUNGO)
                shape = true; 
                    
            command.Parameters.Add("@p1", OleDbType.Boolean).Value = shape;
            // dark parameter - @p2
            Boolean grad = false;
            if (light == E_Gradiation.CHIARO)
                grad = true;

            command.Parameters.Add("@p2", OleDbType.Boolean).Value = grad;
            // texture parameter - @p3
            command.Parameters.Add("@p3", OleDbType.VarChar, 255).Value = texture.ToString();
            // item kind parameter -@p4
            command.Parameters.Add("@p4", OleDbType.VarChar, 255).Value = itemKind.ToString();

            OleDbDataReader result = command.ExecuteReader();

            Item i = new Item((int)result.GetValue(0),result.GetValue(1).ToString(), result.GetValue(2).ToString(), 
                (float)result.GetValue(3), result.GetValue(4).ToString(), result.GetValue(5).ToString(), 
                result.GetValue(6).ToString(), result.GetValue(7).ToString(),result.GetValue(8).ToString());

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
        /// </returns>
        public Item GetItemByShape(E_Shape long1, E_ItemKind itemKind)
        {

            DBConnection.Open();
            // note : we want items of which we have more than 10 available 
            string query = "SELECT TOP 1 C.ID,D.Barcode, D.Nome, D.Prezzo, D.Descrizione, D.Reparto, T.FileName, M.FileName, D.Immagine FROM Capo AS C,DatiNegozio AS D, Grafica AS G,Texture AS T, Maschera AS M, TipoCapo AS TC   WHERE C.DatiNegozio = D.ID AND C.Grafica = G.ID AND G.Texture = T.ID AND G.Maschera = M.ID AND TC.ID = C.Tipo AND D.Disponibilità > 10 AND (((D.[Lungo/Corto])=@p1)) AND TC.Tipo = @p4 ORDER BY rnd(C.ID); ";
            //string query = "SELECT * FROM Capo";
            OleDbCommand command = new OleDbCommand(query, DBConnection);
            // add parameters
            // long parameter - @p1    
            Boolean shape = false;
            if (long1 == E_Shape.LUNGO)
                shape = true;      
            command.Parameters.Add("@p1", OleDbType.Boolean).Value = shape;
            
            // item kind parameter -@p4
            command.Parameters.Add("@p4", OleDbType.VarChar, 255).Value = itemKind.ToString();

            OleDbDataReader result = command.ExecuteReader();

            Item i = new Item((int)result.GetValue(0), result.GetValue(1).ToString(), result.GetValue(2).ToString(),
                (float)result.GetValue(3), result.GetValue(4).ToString(), result.GetValue(5).ToString(),
                result.GetValue(6).ToString(), result.GetValue(7).ToString(),result.GetValue(8).ToString());

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
        /// </returns>
        public Item GetItemByGradation(E_Gradiation light, E_ItemKind itemKind)
        {

            DBConnection.Open();
            // note : we want items of which we have more than 10 available 
            string query = "SELECT TOP 1 C.ID,D.Barcode, D.Nome, D.Prezzo, D.Descrizione, D.Reparto, T.FileName, M.FileName, D.Immagine FROM Capo AS C,DatiNegozio AS D, Grafica AS G,Texture AS T, Maschera AS M, TipoCapo AS TC   WHERE C.DatiNegozio = D.ID AND C.Grafica = G.ID AND G.Texture = T.ID AND G.Maschera = M.ID AND TC.ID = C.Tipo AND D.Disponibilità > 10 AND (((T.[Chiaro/Scuro])=@p2)) TC.Tipo = @p4 ORDER BY rnd(C.ID); ";
            //string query = "SELECT * FROM Capo";
            OleDbCommand command = new OleDbCommand(query, DBConnection);
            // add parameters
            // dark parameter - @p2
            Boolean grad = false;
            if (light == E_Gradiation.CHIARO)
                grad = true;
            command.Parameters.Add("@p2", OleDbType.Boolean).Value = grad;
            
            // item kind parameter -@p4
            command.Parameters.Add("@p4", OleDbType.VarChar, 255).Value = itemKind.ToString();

            OleDbDataReader result = command.ExecuteReader();

            Item i = new Item((int)result.GetValue(0), result.GetValue(1).ToString(), result.GetValue(2).ToString(),
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
