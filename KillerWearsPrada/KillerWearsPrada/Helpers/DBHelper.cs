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
            string query = "SELECT TOP 1 C.ID,D.Barcode, D.Nome, D.Prezzo, D.Descrizione, D.Reparto, T.FileName, M.FileName, D.Immagine";
            query += " FROM Capo AS C,DatiNegozio AS D, Grafica AS G,Texture AS T, Maschera AS M, TipoCapo AS TC, TipoTexture AS TT";
            query += " WHERE C.DatiNegozio = D.ID AND C.Grafica = G.ID AND G.Texture = T.ID AND G.Maschera = M.ID AND TC.ID=D.Tipo";
            query += " AND T.TipoTexture = TT.ID D.Disponibili > 10 AND D.Lungo=@p1 AND T.Chiaro=@p2 AND  TT.Nome = @p3 AND T.ColoreDominante = @p4 AND TC.Tipo = @p5 ORDER BY rnd(C.ID)";

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

            // parameter @p4 - dominant color 
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
            
            string query = "SELECT TOP 1 C.ID,D.Barcode, D.Nome, D.Prezzo, D.Descrizione, D.Reparto, T.FileName, M.FileName, D.Immagine";
            query += " FROM Capo AS C,DatiNegozio AS D, Grafica AS G,Texture AS T, Maschera AS M, TipoCapo AS TC";
            query += " WHERE C.DatiNegozio = D.ID AND C.Grafica = G.ID AND G.Texture = T.ID AND G.Maschera = M.ID AND TC.ID=D.Tipo";
            query += " AND D.Disponibili > 10 AND D.Lungo=@p1 AND TC.Tipo = @p4 ORDER BY rnd(C.ID)";

            // parameter @p1 - shape
            if (long1 == E_Shape.LUNGO)
            {
                query = query.Replace("@p1", true.ToString());
            }
            else
            {
                query = query.Replace("@p1", false.ToString());
            }
                
            // parameter @p4 - item kind 
            query = query.Replace("@p4", "\'" + itemKind.ToString() + "\'");
            
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
            string query = "SELECT TOP 1 C.ID,D.Barcode, D.Nome, D.Prezzo, D.Descrizione, D.Reparto, T.FileName, M.FileName, D.Immagine";
            query += " FROM Capo AS C,DatiNegozio AS D, Grafica AS G,Texture AS T, Maschera AS M, TipoCapo AS TC";
            query += " WHERE C.DatiNegozio = D.ID AND C.Grafica = G.ID AND G.Texture = T.ID AND G.Maschera = M.ID AND TC.ID=D.Tipo";
            query += " AND D.Disponibili > 10 AND T.Chiaro=@p2 AND TC.Tipo = @p4 ORDER BY rnd(C.ID)";

            // parameter @p2 - gradiation
            if (light == E_Gradiation.CHIARO)
            {
                query = query.Replace("@p2", true.ToString());
            }
            else
            {
                query = query.Replace("@p2", false.ToString());
            }

            // parameter @p4 - item kind 
            query = query.Replace("@p4", "\'" + itemKind.ToString() + "\'");

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

        /// <summary>
        /// this method updates the DB adding a score to a player
        /// </summary>
        /// <param name="player">integer, player ID</param>
        /// <param name="score">integer, player's score</param>
        public void SaveScoreForPlayer (int player, int score)
        {
            DBConnection.Open();
            
            string query = "UPDATE Utente SET Punteggio = @value WHERE ID = @id;";


            /*OleDbCommand command = new OleDbCommand(query, DBConnection);
            command.Parameters.Add("@value", OleDbType.Integer).Value = score;
            command.Parameters.Add("@id", OleDbType.Integer).Value = player;*/

            query = query.Replace("@value", score.ToString());
            query = query.Replace("@id", player.ToString());


            OleDbCommand command = new OleDbCommand(query, DBConnection);

            command.ExecuteNonQuery();

            DBConnection.Close();

        }


        
    }
}
