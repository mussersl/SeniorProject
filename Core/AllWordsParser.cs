using System;
using Chatbot;
using System.Collections.Generic;

namespace Chatbot
{
    public class AllWordsParser : KeywordParserInterface
    {
        
		public List<string> parseQuestion(string question){
            string temp = question.ToLower();
			List<string> words = new List<string>(temp.Split(' '));
            EnglishSmartStopWordFilter ssf = new EnglishSmartStopWordFilter();
            List<string> keywords = new List<string>();
            foreach(string s in words)
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
