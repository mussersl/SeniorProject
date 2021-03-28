using System;
using Chatbot;
using System.Collections.Generic;

namespace Chatbot
{
    public class Answer
    {
        private string ansID;
        private string ansString;
        private string questionString;

        public Answer(string ansID, string question, string ansString)
        {
            this.ansID = ansID;
            this.questionString = question;
            this.ansString = ansString;
        }

        //retrieved from database
        public string ID { get; set; }
        public string question { get; set; }
        public string answer { get; set; }
        public Dictionary<string, double> keywords { get; set; }  //Keyword, relevency to question

        //Assigned by relevencyAnlyzer
        public double relevency { get; set; }
    }
}
