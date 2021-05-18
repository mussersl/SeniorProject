using System;
using Chatbot;
using System.Collections.Generic;

namespace Chatbot
{
    public class Answer
    {
        public string answerID;
        public string ansString;
        public string questionString;

        public Answer()
        {
        }

        public Answer(string ansID, string question, string ansString)
        {
            this.answerID = ansID;
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

        public void printToConsole()
        {
            Console.WriteLine("AnswerID: " + answerID);
            Console.WriteLine("Answer: " + ansString);
            Console.WriteLine("Question: " + questionString);
        }
    }
}
