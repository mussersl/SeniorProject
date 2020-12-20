using System
using Chatbot

namspace Chatbot{
    public class getAnswerFromDatabase{
        
        
        public void ConnectToAccess(){
            System.Data.MariaDb.MariaDbConnection wire = new 
                System.Data.MariaDb.MariaDbConnection();
            //connectionString need to be changed
            wire.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;" +
                @"Data source= C:\Documents and Settings\username\" +
                @"My Documents\AccessFile.mdb";
            try
            {
                conn.Open();
                // Insert code to process data.
            }
                catch (Exception ex)
            {
                MessageBox.Show("Failed to connect to data source");
            }
            finally
            {
                conn.Close();
            }
        }
    }
}