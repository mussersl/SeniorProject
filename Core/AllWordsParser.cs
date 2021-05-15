using System;
using Chatbot;
using System.Collections.Generic;

namespace Chatbot
{
    public class AllWordsParser : KeywordParserInterface
    {
        
		public List<string> parseQuestion(string question){
			return new List<String>(question.Split(' '));
		}
		
    }
}
