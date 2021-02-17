using System;
using Chatbot;
using System.Collections.Generic;

namespace Chatbot
{

	public class MariaDBQuery : DatabaseQueryInterface
    {
        public List<Answer> queryDatabaseOnKeywords(List<string> keywords)
        {
            for(k in  keywords)
            {

            }
        }

        public Answer queryDatabaseOnAnswer(Answer ans)
        {
            Database db = new Database();
            db.connect();
            db.adapter = new MySqlDataAdapter("select * from info where Answer.equal(ans)", db.connect);
            return db.adapter.show();
        }

        public String getAnswer(String question)
        {
            Database db = new Database();
            db.connect();
            db.fetchAnswer(question);
            
        }
    }
}