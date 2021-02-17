using System;
using Chatbot;
using System.Collections.Generic;

namespace Chatbot
{
    abstract class DatabaseEditor
    {
        DatabaseQueryInterface dq;
        public DatabaseEditor(DatabaseQueryInterface dqi)
        {
            dq = dqi;
        }

        public abstract bool addQuestionToAnswer(string question, Answer ans, Dictionary<string, List<string>> keywords);

        //These may be the same
        public abstract bool addAnswer(Answer ans);
        public abstract bool editAnswer(Answer ans);
    }
}