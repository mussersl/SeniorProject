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
                string SQLQuery = "INSERT INTO answers (Question, Answer) VALUES ('" + ans.question + "', '" + ans.answer + "'); ";
                
                MySqlCommand myCommand = new MySqlCommand(SQLQuery, connect);

                int i = myCommand.ExecuteNonQuery();
                Console.WriteLine(1);

                SQLQuery = "SELECT ID FROM answers WHERE Question = '" + ans.question + "' AND Answer = '" + ans.answer + "';";
                myCommand = new MySqlCommand(SQLQuery, connect);
                MySqlDataReader myReader = myCommand.ExecuteReader();
                if (myReader.Read())
                {
                    ans.ID = myReader.GetString(0);
                }

                connect.Close();
                return true;

            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        public bool Delete(string id)
        {
            try
            {
                connection();
                string deleteQuery = "DELETE FROM answers WHERE ID = " + id + ";";
                string deleteRelevancyQuery = "DELETE FROM keyword_relevancy WHERE AnswerID = " + id + ";";

                MySqlCommand myCommand = new MySqlCommand(deleteQuery, connect);
                int i = myCommand.ExecuteNonQuery();
                myCommand = new MySqlCommand(deleteRelevancyQuery, connect);
                i = myCommand.ExecuteNonQuery();
                connect.Close();
                return true;
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        internal bool VerifyLogin(string username, string password)
        {
            try
            {
                connection();
                string LoginQuery = "SELECT Password FROM login where Username = '" + username + "';";

                MySqlCommand myCommand = new MySqlCommand(LoginQuery, connect);
                MySqlDataReader myReader = myCommand.ExecuteReader();
                string passwordCheck = "";
                if (myReader.Read())
                {
                    passwordCheck = myReader.GetString(0);
                }
                myReader.Close();
                connect.Close();

                if (password != passwordCheck)
                {
                    return false;
                }
                return true;

            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        internal bool addKeywordToAnswer(string ansID, string keyword)
        {
            try
            {
                connection();
                MySqlCommand myCommand;
                string SQLQuery = "INSERT INTO keywords (Keyword) VALUES  ('" + keyword + "');";
                try
                {
                    myCommand = new MySqlCommand(SQLQuery, connect);
                    int i = myCommand.ExecuteNonQuery();
                }
                catch (MySql.Data.MySqlClient.MySqlException ex)
                {

                }

                SQLQuery = "SELECT ID FROM keywords WHERE Keyword = '" + keyword + "';";
                myCommand = new MySqlCommand(SQLQuery, connect);
                MySqlDataReader myReader = myCommand.ExecuteReader();
                string keywordID = "";
                if (myReader.Read())
                {
                    keywordID = myReader.GetString(0);
                }
                myReader.Close();

                if (keywordID != "")
                {
                    SQLQuery = "INSERT INTO keyword_relevancy (KeywordID, Relevance, AnswerID) VALUES (" + keywordID + " ,30 ," + ansID + ");";
                    try
                    {
                        myCommand = new MySqlCommand(SQLQuery, connect);
                        int i = myCommand.ExecuteNonQuery();
                    }
                    catch (MySql.Data.MySqlClient.MySqlException ex)
                    {

                    }
                }

                connect.Close();
                return true;

            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                connect.Close();
                Console.WriteLine(ex);
                return true;
            }
        }

        public bool relevancyModification(string ansID, string keyID, bool incrementing)
        {
            try
            {
                connection();
                string SQlQuery = "SELECT Relevance FROM keyword_relevancy WHERE KeywordID = " + keyID + " AND AnswerId = " + ansID + ";";
                MySqlCommand myCommand = new MySqlCommand(SQlQuery, connect);
                MySqlDataReader myReader = myCommand.ExecuteReader();
                int i;
                if (myReader.Read())
                {
                    int.TryParse(myReader.GetString(0), out i);
                }else{
                    return false;
                }
                if (incrementing){
                    i = Math.Min(i + 1, 100);
                }else{
                    i = Math.Max(i - 1, 0);
                }
                SQlQuery = "UPDATE keyword_relevancy SET Relevance = '" + i.ToString() + "' WHERE KeywordID = " + keyID + " AND AnswerId = " + ansID + ";";
                myCommand = new MySqlCommand(SQlQuery, connect);
                int j = myCommand.ExecuteNonQuery();
                Console.WriteLine(j);
                connect.Close();
                return true;
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                connect.Close();
                return false;
            }
        }
        bool DatabaseEditor.editAnswer(Answer ans)
        {
            try
            {
                connection();
                string SQLQuery = "UPDATE answers SET Question = '" + ans.question + "', Answer = '" + ans.answer + "' WHERE ID = " + ans.ID + ";";

                MySqlCommand myCommand = new MySqlCommand(SQLQuery, connect);
                int i = myCommand.ExecuteNonQuery();
                Console.WriteLine(i);
                connect.Close();
                return true;

            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                connect.Close();
                ((DatabaseEditor)this).addAnswer(ans);
                return true;
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

                        answers[ansId].keywords.Add(keyword, relevancy);

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

        