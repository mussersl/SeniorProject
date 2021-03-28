using System;
using System.Collections.Generic;
using Chatbot;
using MySql.Data.MySqlClient;

namespace ChatbotCore.DatabaseInteraction
{
    public class Database : DatabaseQueryInterface
    {
        
     
        
        public void connection(){
            MySql.Data.MySqlClient.MySqlConnection connect = new MySql.Data.MySqlClient.MySqlConnection();
            string myConnectString = "server=127.0.0.1;port=3306;Database=irpachatbot;uid=root;pwd=rhit2020;";
            MySqlDataReader myReader;
            connect.ConnectionString = myConnectString;
            try
            {
                connect.Open();
                Console.WriteLine(2);
                
                String selectQuery = "select ansString from answers where question = 'what does IRPA stand for?';";
                MySqlCommand myCommand = new MySqlCommand(selectQuery, connect);       
                myReader = myCommand.ExecuteReader();
                // Always call Read before accessing data.
                while (myReader.Read())
                {
                    Console.WriteLine(myReader.GetString(0));
                }
                // always call Close when done reading.
                myReader.Close();
                connect.Close();


            }
            catch(MySql.Data.MySqlClient.MySqlException ex){
                // MessageBox.Show(ex.Message);
                Console.WriteLine(ex);
            }
        }

        List<Answer> DatabaseQueryInterface.queryDatabaseOnAnswer(Answer ans)
        {
            throw new NotImplementedException();
        }

        List<Answer> DatabaseQueryInterface.queryDatabaseOnKeywords(List<string> keywords)
        {
            MySql.Data.MySqlClient.MySqlConnection connect = new MySql.Data.MySqlClient.MySqlConnection();
            string myConnectString = "server=127.0.0.1;port=3306;Database=irpachatbot;uid=root;pwd=rhit2020;";
            List<Answer> output = new List<Answer>();

            connect.ConnectionString = myConnectString;
            try
            {
                connect.Open();
                Console.WriteLine(2);

                foreach (String keyword in keywords)
                {
                    String selectAnsStringQuery = "SELECT ansString FROM answers WHERE ansID " +
                        "= (SELECT ansID FROM relevancy WHERE wordID = (SELECT wordID FROM words WHERE" +
                        " wordString = '" + keyword + "')); ";
                    String selectAnsIDQuery = "SELECT ansID FROM relevancy WHERE wordID = (SELECT wordID FROM words WHERE" +
                        " wordString = '" + keyword + "'); ";
                    String selectQuestionQuery = "SELECT question FROM answers WHERE ansID " +
                        "= (SELECT ansID FROM relevancy WHERE wordID = (SELECT wordID FROM words WHERE" +
                        " wordString = '" + keyword + "')); ";
                    
                    
                    MySqlDataReader myReader;
                    
                    //read ansString
                    MySqlCommand myCommand = new MySqlCommand(selectAnsStringQuery, connect);
                    myReader = myCommand.ExecuteReader();
                    string ansString = "";
                    while (myReader.Read())
                    {
                        ansString += myReader.GetString(0);
                    }
                    Console.WriteLine(ansString);
                    myReader.Close();

                    //read ansID
                    myCommand = new MySqlCommand(selectAnsIDQuery, connect);
                    myReader = myCommand.ExecuteReader();
                    string ansID = "";
                    while (myReader.Read())
                    {
                        ansID += myReader.GetString(0);
                    }
                    Console.WriteLine(ansID);
                    myReader.Close();

                    //read question
                    myCommand = new MySqlCommand(selectQuestionQuery, connect);
                    myReader = myCommand.ExecuteReader();
                    string question = "";
                    while (myReader.Read())
                    {
                        question += myReader.GetString(0);
                    }
                    Console.WriteLine(question);

                    myReader.Close();
                    Answer ans = new Answer(ansID, question, ansString);
                    output.Add(ans);
                    

                }
                connect.Close();
                return output;
               

            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                // MessageBox.Show(ex.Message);
                Console.WriteLine(ex);
            }
            return null;
        }

        //    public void closeConnect()
        //    {
        //        connect.ConnectionString = myConnectString;
        //        connect.Close();
        //    }
    }
}

        