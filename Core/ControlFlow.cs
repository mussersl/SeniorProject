using System;
using Chatbot;
using System.Collections.Generic;

namespace Chatbot
{
    public class ControlFlow
    {
        private KeywordParserInterface wordParser;
        private DatabaseQueryInterface db;

        //Constructor
        public ControlFlow()
        {
            this.wordParser = new AllWordsParser(); 
            this.db = new Database();
            //DatabaseEditor dbEditor = new Database();
            List<String> keywords = new List<string>();
        }

        public string askQuestion(string question)
        {
            List<string> questionKeywords = wordParser.parseQuestion(question);
            List<Answer> answers = db.queryDatabaseOnKeywords(questionKeywords);
            if(answers == null)
            {
                return "I'm sorry. I can't answer your question right now.";
            }
            else if (answers.Count == 0)
            {
                return "I'm sorry. I don't understand the question.";
            }
            else
            {
                foreach(Answer a in answers)
                {
                    a.printToConsole();
                }
                return answers[0].ansString;
            }
        }

        public bool addAnswer(List<string> questions, string answer)
        {
            return false;
        }
    }
}
