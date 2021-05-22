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
            keywords = new Dictionary<string, double>();
        }

        public Answer(string ansID, string question, string ansString)
        {
            this.ID = ansID;
            this.question = question;
            this.answer = ansString;
            keywords = new Dictionary<string, double>();
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
            Console.WriteLine("AnswerID: " + ID);
            Console.WriteLine("Answer: " + answer);
            Console.WriteLine("Question: " + question);
        }
    }
}
