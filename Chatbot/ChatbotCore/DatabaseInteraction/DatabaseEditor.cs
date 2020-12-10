using System;
using Chatbot;

namespace Chatbot
{
    abstract class DatabaseEditor
    {
        DatabaseQueryInterface dq;
        public DatabaseEditor(DatabaseQueryInterface dqi)
        {
            dq = dqi;
        }

        public bool addQuestionToAnswer(String question, Answer ans, Dictionary<String, List<String>> keywords);

        //These may be the same
        public bool addAnswer(Answer ans);
        public bool editAnswer(Answer ans);
    }
}