using System;
using Chatbot;

namespace Chatbot
{
    public class Answer
    {
        //retrieved from database
        public String ID { get; set; }
        public String question { get; set; }
        public String answer { get; set; }
        public Dictionary<String, double> keywords { get; set; }  //Keyword, relevency to question

        //Assigned by relevencyAnlyzer
        public double relevency { get; set; }
    }
}
