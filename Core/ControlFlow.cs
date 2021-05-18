using System;
using Chatbot;
using System.Collections.Generic;

namespace Chatbot
{
    public class ControlFlow
    {
        private KeywordParserInterface wordParser;
        private Database db;

        //Constructor
        public ControlFlow()
        {
            this.wordParser = new AllWordsParser(); 
            this.db = new Database();
            this.db.connection();
            //DatabaseEditor dbEditor = new Database();
            List<String> keywords = new List<string>();
        }

        public string askQuestion(string question)
        {
            List<string> questionKeywords = wordParser.parseQuestion(question);
            return  "Keywords parsed : " + questionKeywords.Count;
        }

        public bool addAnswer(List<string> questions, string answer)
        {
            return false;
        }
    }
}
