using System;
using System.Collections.Generic;
using Chatbot;
using MySql.Data.MySqlClient;

namespace Chatbot
{
    public class Database : DatabaseQueryInterface, DatabaseEditor
    {
        public MySql.Data.MySqlClient.MySqlConnection connect;
        public Database()
        {
            connect = new MySql.Data.MySqlClient.MySqlConnection();
        }
        
        public void connection(){
            string myConnectString = "server=137.112.232.187; port=3300;Database=body;uid=root;pwd=test;";
            connect.ConnectionString = myConnectString;
            try
            {
                connect.Open();
                Console.WriteLine("MariaDB connected: " + myConnectString);
            }
            catch(MySql.Data.MySqlClient.MySqlException ex){
                Console.WriteLine("Error received: "+ex);
            }
        }


        bool DatabaseEditor.addAnswer(Answer ans)
        {
            try
            {
                connection();
                String SQLQuery = "INSERT INTO answers VALUES(@name1, @name2, @name3); ";
                
                MySqlCommand myCommand = new MySqlCommand(SQLQuery, connect);
                myCommand.Parameters.AddWithValue("name1", ans.answerID);
                myCommand.Parameters.AddWithValue("name2", ans.questionString);
                myCommand.Parameters.AddWithValue("name3", ans.ansString);

                int i = myCommand.ExecuteNonQuery();
                Console.WriteLine(1);
                connect.Close();
                return true;

            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        bool DatabaseEditor.editAnswer(Answer ans)
        {
            try
            {
                connection();
                String SQLQuery = "UPDATE answers SET Answer = @name3, Question = @name2 " +
                    "WHERE ID = @name1; ";

                MySqlCommand myCommand = new MySqlCommand(SQLQuery, connect);
                myCommand.Parameters.AddWithValue("name1", ans.answerID);
                myCommand.Parameters.AddWithValue("name2", ans.questionString);
                myCommand.Parameters.AddWithValue("name3", ans.ansString);

                int i = myCommand.ExecuteNonQuery();
                Console.WriteLine(1);
                connect.Close();
                return true;

            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }



        //I feel like this should maybe be one answer, rather than a list of them.
        List<Answer> DatabaseQueryInterface.queryDatabaseOnAnswer(Answer ans)
        {
            throw new NotImplementedException();
        }

        List<Answer> DatabaseQueryInterface.queryDatabaseOnKeywords(List<string> keywords)
        {
            List<Answer> output = new List<Answer>();
            try
            {
                connection();

                foreach (String keyword in keywords)
                {
                    String selectAnsStringQuery = "SELECT Answer FROM answers WHERE ID " +
                        "= (SELECT AnswerID FROM keyword_relevancy WHERE KeywordID = (SELECT ID FROM keywords WHERE" +
                        " Keyword = '" + keyword + "')); ";
                    String selectAnsIDQuery = "SELECT AnswerID FROM keyword_relevancy WHERE KeywordID = (SELECT ID FROM keywords WHERE" +
                        " Keyword = '" + keyword + "'); ";
                    String selectQuestionQuery = "SELECT Question FROM answers WHERE ID " +
                        "= (SELECT AnswerID FROM keyword_relevancy WHERE KeywordID = (SELECT ID FROM keywords WHERE" +
                        " Keyword = '" + keyword + "')); ";
                    
                    
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
                Console.WriteLine(ex);
            }
            return null;
        }
    }
}

        