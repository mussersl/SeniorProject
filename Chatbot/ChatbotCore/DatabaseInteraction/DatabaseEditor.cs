using System;
using Chatbot;
using System.Collections.Generic;

namespace Chatbot
{
    interface DatabaseEditor
    {

        //public bool addQuestionToAnswer(Answer ans);

        //These may be the same
        bool addAnswer(Answer ans);
       
        bool editAnswer(Answer ans);
    }
}