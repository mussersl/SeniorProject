using System;
using Chatbot;

namespace Chatbot
{
    public class Answer
    {
        public String ID { get; set; }
        public String question { get; set; }
        public String answer { get; set; }
        public Dictionary<String, double> keywords { get; set; }
        public double relevency { get; set; }
    }
}
