using System;
using Chatbot;

namespace Chatbot
{
    public class ControlFlow
    {
        private Dictionary<String, double> recentKeywords;
        private KeywordParserInterface wordParser;

        //Constructor
        public ControlFlow()
        {
            this.wordParser = new AllWordsParser();
        }

        public String askQuestion(String question)
        {
            List<String> questionKeywords = wordParser.parseQuestion(question);
            return null;
        }

        public bool addAnswer(List<String> questions, String answer)
        {
            return null;
        }
    }
}
