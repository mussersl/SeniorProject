using System;
using Chatbot;
using System.Collections.Generic;

namespace Chatbot
{
    public class Answer
    {
        //retrieved from database
        public string ID { get; set; }
        public string question { get; set; }
        public string answer { get; set; }
        public Dictionary<string, double> keywords { get; set; }  //Keyword, relevency to question

        //Assigned by relevencyAnlyzer
        public double relevency { get; set; }
    }
}
