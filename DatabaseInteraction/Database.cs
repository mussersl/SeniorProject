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
            string myConnectString = "server=137.112.235.229; port=3300;Database=body;uid=root;pwd=test;";
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
            try
            {
                connection();
                Dictionary<string, Answer> answers = new Dictionary<string, Answer>();
                foreach (string keyword in keywords)
                {
                    string selectKeywordId = "SELECT ID FROM keywords WHERE Keyword = '" + keyword + "';";
                                        
                    MySqlDataReader myReader;
                    
                    //Find ID
                    MySqlCommand myCommand = new MySqlCommand(selectKeywordId, connect);
                    myReader = myCommand.ExecuteReader();
                    string id = "-1";
                    if (myReader.Read())
                    {
                        id = myReader.GetString(0);
                    }
                    myReader.Close();

                    string getAnswerIdsQuery = "SELECT AnswerID FROM keyword_relevancy WHERE KeywordID = '" + id + "';";

                    //get ansIDs
                    MySqlCommand values = new MySqlCommand(getAnswerIdsQuery, connect);
                    myReader = values.ExecuteReader();
                    List<string> answerIDs = new List<string>();
                    while(myReader.Read()){
                        answerIDs.Add(myReader.GetString(0));
                    }
                    myReader.Close();
                    
                    foreach(string ansId in answerIDs)
                    {
                        if (!answers.ContainsKey(ansId))
                        {
                            answers.Add(ansId, new Answer());
                            answers[ansId].ID = ansId;

                            string selectQuestionQuery = "SELECT Question FROM answers WHERE ID ='" + ansId + "';";
                            string selectAnswerQuery = "SELECT Answer FROM answers WHERE ID ='" + ansId + "';";

                            myCommand = new MySqlCommand(selectAnswerQuery, connect);
                            myReader = myCommand.ExecuteReader();
                            if (myReader.Read())
                            {
                                answers[ansId].answer = myReader.GetString(0);
                            }
                            myReader.Close();

                            myCommand = new MySqlCommand(selectQuestionQuery, connect);
                            myReader = myCommand.ExecuteReader();
                            if (myReader.Read())
                            {
                                answers[ansId].question = myReader.GetString(0);
                            }

                            myReader.Close();
                        }

                        string getKeywordRelevancy = "SELECT Relevance FROM keyword_relevancy WHERE KeywordID = '" + id + "' AND AnswerID = '" + ansId + "';";
                        myCommand = new MySqlCommand(getKeywordRelevancy, connect);
                        myReader = myCommand.ExecuteReader();
                        double relevancy = -1;
                        if (myReader.Read())
                        {
                            relevancy = myReader.GetInt32(0);
                        }
                        myReader.Close();
                        try
                        {
                            answers[ansId].keywords.Add(keyword, relevancy);
                        }
                        catch { }

                    }

                }
                connect.Close();

                List<Answer> output = new List<Answer>();
                foreach (KeyValuePair<string, Answer> a in answers)
                {
                    output.Add(a.Value);
                }
                return output;
               

            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }
        public List<Answer> TotalOutput()
        {
            try
            {
                connection();

                String selectAllAnswersIDs = "SELECT ID FROM answers";

                MySqlDataReader holdReader;

                //read ansString
                MySqlCommand holdCommand = new MySqlCommand(selectAllAnswersIDs, connect);
                holdReader = holdCommand.ExecuteReader();
                List<string> ansIDs = new List<string>();
                List<string> ansString = new List<string>();
                List<string> question = new List<string>();
                while (holdReader.Read())
                {
                    ansIDs.Add(holdReader.GetString(0));
                }
                holdReader.Close();

                foreach (string id in ansIDs)
                {
                    string selectQuestionQuery = "SELECT Question FROM answers WHERE ID ='" + id + "';";
                    string selectAnswerQuery = "SELECT Answer FROM answers WHERE ID ='" + id + "';";


                    MySqlDataReader myReader;

                    //read question
                    MySqlCommand myCommand = new MySqlCommand(selectAnswerQuery, connect);
                    myReader = myCommand.ExecuteReader();
                    if (myReader.Read())
                    {
                        ansString.Add(myReader.GetString(0));
                    }
                    myReader.Close();

                    //read question
                    myCommand = new MySqlCommand(selectQuestionQuery, connect);
                    myReader = myCommand.ExecuteReader();
                    if (myReader.Read())
                    {
                        question.Add(myReader.GetString(0));
                    }

                    myReader.Close();

                }
                connect.Close();

                Answer ans;
                List<Answer> output = new List<Answer>();
                for (int i = 0; i < ansString.Count; i++)
                {
                    ans = new Answer(ansIDs[i], question[i], ansString[i]);
                    output.Add(ans);
                }
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

        