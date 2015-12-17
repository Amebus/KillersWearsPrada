using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;

namespace KillerWearsPrada.Helpers 
{
    /// <summary>
    /// Questa classe serve per gestire la comunicazione tra DB e programma
    /// </summary>
    class DBHelper
    {
        private String connectionString;
        private OleDbConnection DBConection;
        
        public DBHelper()
        {
            connectionString = Properties.Settings.Default.KWP_DB_Test_Connection;

            DBConection = new OleDbConnection(connectionString);

        }

        /// <summary>
        /// Metodo che testa la connessione al DB
        /// </summary>
        /// <returns>stringa che indica se il test è andato a buon fine</returns>
        public String TestConnection()
        {
            try
            {
                DBConection.Open();

            }
            catch(Exception e)
            {
                return e.Message;
            }

            DBConection.Close();

            return "Connessione avvenuta con successo";
        }
    }
}
