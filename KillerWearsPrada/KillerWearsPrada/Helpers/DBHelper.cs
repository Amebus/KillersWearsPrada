using System;
using System.Data.OleDb;
using System.Data.SqlClient;

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
        /// <param name="shortVSlong"> boolean, true = short, false = long</param>
        /// <param name="dark">boolean, true = dark, false = light </param>
        /// <param name="plainColor"> boolean, true = plaincolor</param>
        /// <param name="texture"> string containing the kind of texture needed</param>
        /// <returns> an OleDbDataReader object that contains, in order : 
        ///         1. Item code
        ///         2. Barcode
        ///         3. Item Name
        ///         4. Item Price
        ///         5. Item Reparto
        ///         6. Item Description
        ///         7. Texture file name
        ///         8. Mask file name
        /// </returns>
        public OleDbDataReader getItemFromClues(bool shortVSlong, bool dark, bool plainColor, string texture)
        {

            DBConnection.Open();
            string query = "SELECT TOP 1 C.ID,D.Barcode, D.Nome, D.Prezzo, D.Descrizione, D.Reparto, T.FileName, M.FileName FROM Capo C,DatiNegozio D, Grafica G,Texture T, Maschera M   WHERE C.DatiNegozio = D.ID AND C.Grafica = G.ID AND G.Texture = T.ID AND G.Maschera = M.ID AND D.Disponibilità > 10 AND  ORDER BY rnd(C.ID); ";

            OleDbCommand command = new OleDbCommand(query, DBConnection);

            OleDbDataReader result = command.ExecuteReader();

            return result;
        }
        // le informazioni le ho prese da questo sito : http://www.dotnetperls.com/sqlparameter 


        /*parametrized query v2
        private static void UpdateDemographics(Int32 customerID,
        string demoXml, string connectionString)
        {
        // Update the demographics for a store, which is stored 
        // in an xml column. 
        string commandText = "UPDATE Sales.Store SET Demographics = @demographics "
            + "WHERE CustomerID = @ID;";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            SqlCommand command = new SqlCommand(commandText, connection);
            command.Parameters.Add("@ID", SqlDbType.Int);
            command.Parameters["@ID"].Value = customerID;

            // Use AddWithValue to assign Demographics.
            // SQL Server will implicitly convert strings into XML.
            command.Parameters.AddWithValue("@demographics", demoXml);

            try
            {
                connection.Open();
                Int32 rowsAffected = command.ExecuteNonQuery();
                Console.WriteLine("RowsAffected: {0}", rowsAffected);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        }
        */
    }
}
