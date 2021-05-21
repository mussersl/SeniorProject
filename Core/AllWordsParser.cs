using System;
using Chatbot;
using System.Collections.Generic;

namespace Chatbot
{
    public class AllWordsParser : KeywordParserInterface
    {
        public List<string> parseQuestion(string question)
        {
            char[] temp = question.ToLower().ToCharArray();

            for(int i = 0; i<temp.Length; i++)
            {
                if (!char.IsLetterOrDigit(temp[i]))
                {
                    temp[i] = ' ';
                }
            }

            string newQuestion = temp.ToString();

            List<string> words = new List<string>(newQuestion.Split(' '));
            EnglishSmartStopWordFilter ssf = new EnglishSmartStopWordFilter();
            List<string> keywords = new List<string>();
            foreach (string s in words)
            {
                if (!ssf.IsStopWord(s))
                {
                    keywords.Add(s);
                }
            }
            return keywords;
        }

    }
}
