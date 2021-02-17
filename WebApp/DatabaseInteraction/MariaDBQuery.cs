using System;
using Chatbot;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using ChatbotCore.DatabaseInteraction;

namespace Chatbot
{

	public class MariaDBQuery : DatabaseQueryInterface
    {
        public List<Answer> queryDatabaseOnKeywords(List<string> keywords)
        {
            Database db = new Database();
            db.connect();
            foreach (string k in keywords)
            {
                db.fetchAnswer(k);
            }

            return null;
        }

        public List<Answer> queryDatabaseOnAnswer(Answer ans)
        {
            Database db = new Database();
            db.connect();
            db.adapter = new MySqlDataAdapter("select * from info where Answer.equal(ans)", db.connection);
            return null;
        }

        public String getAnswer(String question)
        {
            Database db = new Database();
            db.connect();
            db.fetchAnswer(question);
            return null;
        }
    }
}