using System;
using Chatbot;

namespace Chatbot
{
    public class AllWordsParser : KeywordParserInterface
    {
        
		public List<String> parseQuestion(string Question){
			return List<String> toReturn = new ArrayList<String(Arrays.asList(Question.split(" ")));
		}
		
    }
}
