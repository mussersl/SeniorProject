using System;
using Chatbot;
using MySql.Data.MySqlClient;

namespace ChatbotCore.DatabaseInteraction
{
    public class Database
    {
        public MySqlDataAdapter adapter;
        string myConnectString;
        public MySql.Data.MySqlClient.MySqlConnection connection;

        public Database()
        {
        
            myConnectString = "server=127.0.0.1;port=3306;Database=irpachatbot;uid=root;pwd=rhit2020;";
            connection = new MySql.Data.MySqlClient.MySqlConnection();
            connection.ConnectionString = myConnectString;
        }
        
        public void connect(){
            try
            {
                connection.Open();
            }
            catch(MySql.Data.MySqlClient.MySqlException ex){
                //MessageBox.Show(ex.Message);
            }
        }

        public void fetchAnswer(String keyword)
        {
            String selectQuery = "select Answer from relevancy where Keyword = " + keyword;
            MySqlCommand myCommand = new MySqlCommand(selectQuery, connection);
            MySqlDataReader myReader;
            myReader = myCommand.ExecuteReader();
            // Always call Read before accessing data.
            while (myReader.Read())
            {
                Console.WriteLine(myReader.GetInt32(0) + ", " + myReader.GetString(1));
            }
            // always call Close when done reading.
            myReader.Close();

        }

        public void closeconnect(){
            connection.Close();
        }
    }
}