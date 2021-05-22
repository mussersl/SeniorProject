using System;
using Chatbot;
using System.Collections.Generic;

namespace Chatbot
{
    public class ControlFlow
    {
        private KeywordParserInterface wordParser;
        private DatabaseQueryInterface db;
        private ResponseGeneratorInterface rg;
        private RelevancyAnalyzerInterface ra;

        //Constructor
        public ControlFlow()
        {
            this.wordParser = new AllWordsParser(); 
            this.db = new Database();
            this.rg = new ResponseGeneratorR1();
            this.ra = new RelevancyAnalyzer();
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
            else
            {
                
                for(int i = 0; i < answers.Count; i++)
                {
                    ra.assignRelevencyOf(answers[i], questionKeywords);
                }
                
                List<string> responses = rg.generateResponse(answers);
                string finalResponse = "";
                for(int i = 0; i < responses.Count; i++)
                {
                    finalResponse += responses[i];
                    finalResponse += '\n';
                }
                return finalResponse;
            }
        }

        public bool addAnswer(List<string> questions, string answer)
        {
            return false;
        }
    }
}
