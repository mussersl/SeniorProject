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

        public void connection()
        {
            string myConnectString = "server=137.112.237.187; port=3300;Database=body;uid=root;pwd=test;";
            connect.ConnectionString = myConnectString;
            try
            {
                connect.Open();
                Console.WriteLine("MariaDB connected: " + myConnectString);
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                Console.WriteLine("Error received: " + ex);
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
                        "IN (SELECT AnswerID FROM keyword_relevancy WHERE KeywordID = (SELECT ID FROM keywords WHERE" +
                        " Keyword = '" + keyword + "')); ";
                    String selectAnsIDQuery = "SELECT AnswerID FROM keyword_relevancy WHERE KeywordID = (SELECT ID FROM keywords WHERE" +
                        " Keyword = '" + keyword + "'); ";
                    String selectQuestionQuery = "SELECT Question FROM answers WHERE ID " +
                        "IN (SELECT AnswerID FROM keyword_relevancy WHERE KeywordID = (SELECT ID FROM keywords WHERE" +
                        " Keyword = '" + keyword + "')); ";
                    String valueQuery = "SELECT SUM(Relevance) AS Relevance FROM (keyword_relevancy WHERE KeywordID = (SELECT ID FROM keywords WHERE" +
                        " Keyword = '" + keyword + "') AND AnswerID IN (SELECT AnswerID FROM keyword_relevancy WHERE KeywordID = (SELECT ID FROM keywords WHERE" +
                        " Keyword = '" + keyword + "'))); ";

                    MySqlDataReader myReader;

                    //read ansString
                    MySqlCommand myCommand = new MySqlCommand(selectAnsStringQuery, connect);
                    myReader = myCommand.ExecuteReader();
                    List<string> ansString = new List<string>();
                    while (myReader.Read())
                    {
                        ansString.Add(myReader.GetString(0));
                    }
                    Console.WriteLine(ansString);
                    myReader.Close();


                    MySqlCommand values = new MySqlCommand(valueQuery, connect);
                    myReader = values.ExecuteReader();
                    List<double> allValues = new List<double>();
                    if (myReader.HasRows)
                    {
                        myReader.Read();
                        allValues.Add(Convert.ToDouble(myReader["Relevance"]));
                    }
                    Console.WriteLine(allValues);
                    myReader.Close();

                    //read ansID
                    myCommand = new MySqlCommand(selectAnsIDQuery, connect);
                    myReader = myCommand.ExecuteReader();
                    List<string> ansID = new List<string>();
                    while (myReader.Read())
                    {
                        ansID.Add(myReader.GetString(0));
                    }
                    Console.WriteLine(ansID);
                    myReader.Close();

                    //read question
                    myCommand = new MySqlCommand(selectQuestionQuery, connect);
                    myReader = myCommand.ExecuteReader();
                    List<string> question = new List<string>();
                    while (myReader.Read())
                    {
                        question.Add(myReader.GetString(0));
                    }
                    Console.WriteLine(question);

                    myReader.Close();

                    Answer ans;

                    for (int i = 0; i < ansString.Count; i++)
                    {
                        ans = new Answer(ansID[i], question[i], ansString[i]);
                        ans.relevency = allValues[i];
                        output.Add(ans);
                    }


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
        List<Answer> TotalOutput()
        {
            List<Answer> output = new List<Answer>();
            try
            {
                connection();

                String selectAllKeywords = "SELECT * keyword FROM keywords";

                MySqlDataReader holdReader;

                //read ansString
                MySqlCommand holdCommand = new MySqlCommand(selectAllKeywords, connect);
                holdReader = holdCommand.ExecuteReader();
                List<string> keywords = new List<string>();
                while (holdReader.Read())
                {
                    keywords.Add(holdReader.GetString(0));
                }
                Console.WriteLine(keywords);
                holdReader.Close();

                foreach (String keyword in keywords)
                {
                    String selectAnsStringQuery = "SELECT Answer FROM answers WHERE ID " +
                        "IN (SELECT AnswerID FROM keyword_relevancy WHERE KeywordID = (SELECT ID FROM keywords WHERE" +
                        " Keyword = '" + keyword + "')); ";
                    String selectAnsIDQuery = "SELECT AnswerID FROM keyword_relevancy WHERE KeywordID = (SELECT ID FROM keywords WHERE" +
                        " Keyword = '" + keyword + "'); ";
                    String selectQuestionQuery = "SELECT Question FROM answers WHERE ID " +
                        "IN (SELECT AnswerID FROM keyword_relevancy WHERE KeywordID = (SELECT ID FROM keywords WHERE" +
                        " Keyword = '" + keyword + "')); ";


                    MySqlDataReader myReader;

                    //read ansString
                    MySqlCommand myCommand = new MySqlCommand(selectAnsStringQuery, connect);
                    myReader = myCommand.ExecuteReader();
                    List<string> ansString = new List<string>();
                    while (myReader.Read())
                    {
                        ansString.Add(myReader.GetString(0));
                    }
                    Console.WriteLine(ansString);
                    myReader.Close();

                    //read ansID
                    myCommand = new MySqlCommand(selectAnsIDQuery, connect);
                    myReader = myCommand.ExecuteReader();
                    List<string> ansID = new List<string>();
                    while (myReader.Read())
                    {
                        ansID.Add(myReader.GetString(0));
                    }
                    Console.WriteLine(ansID);
                    myReader.Close();

                    //read question
                    myCommand = new MySqlCommand(selectQuestionQuery, connect);
                    myReader = myCommand.ExecuteReader();
                    List<string> question = new List<string>();
                    while (myReader.Read())
                    {
                        question.Add(myReader.GetString(0));
                    }
                    Console.WriteLine(question);

                    myReader.Close();

                    Answer ans;

                    for (int i = 0; i < ansString.Count; i++)
                    {
                        ans = new Answer(ansID[i], question[i], ansString[i]);
                        output.Add(ans);
                    }


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

