using System;
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

    class Queries
    {
        public const String q1 = "query";
    }

    /* parametrized query v1
    using (AdventureWorksEntities context =
    new AdventureWorksEntities())
    {
    // Create a query that takes two parameters.
    string queryString =
        @"SELECT VALUE Contact FROM AdventureWorksEntities.Contacts 
                AS Contact WHERE Contact.LastName = @ln AND
                Contact.FirstName = @fn";

    ObjectQuery<Contact> contactQuery =
        new ObjectQuery<Contact>(queryString, context);

    // Add parameters to the collection.
    contactQuery.Parameters.Add(new ObjectParameter("ln", "Adams"));
    contactQuery.Parameters.Add(new ObjectParameter("fn", "Frances"));

    // Iterate through the collection of Contact items.
    foreach (Contact result in contactQuery)
        Console.WriteLine("Last Name: {0}; First Name: {1}",
        result.LastName, result.FirstName);
    }
    */
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
