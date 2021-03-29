using System;
using Chatbot;
using System.Collections.Generic;

namespace Chatbot
{
    interface DatabaseEditor
    {

        //public bool addQuestionToAnswer(Answer ans);

        //These may be the same
        public bool addAnswer(Answer ans);
       
        public bool editAnswer(Answer ans);
    }
}