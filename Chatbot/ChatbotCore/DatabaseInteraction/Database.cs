using System
using Chatbot;
using MySql.Data.MySqlClient;

namespace ChatbotCore.DatabaseInteraction
{
    public class Database
    {
        MySql.Data.MySqlClient.MySqlConnection connect;
        MySqlDataAdapter adapter;
        string myConnectString = "server=127.0.0.1;port=3306;Database=irpachatbot;uid=root;pwd=rhit2020;";
        connect = new MySql.Data.MySqlClient.MySqlConnection();
                connect.ConnectionString = myConnectString;
        
        public void connect(){
            try
            {
                connect.Open();
            }
            catch(MySql.Data.MySqlClient.MySqlException ex){
                MessageBox.Show(ex.Message);
            }
        }

        public void fetchAnswer(String question)
        {
            String selectQuery = "select Answer from relevancy where Question = " + question;
            MySqlCommand myCommand = new MySqlCommand(selectQuery, connect);
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
    }

        public void close connect(){
            connect.Close()
        }
    }
}