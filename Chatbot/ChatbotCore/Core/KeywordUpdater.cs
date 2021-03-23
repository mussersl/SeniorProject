using System;
using Chatbot;
using System.Collections.Generic;

namespace Chatbot
{
    public class KeywordUpdater: KeywordRelevencyUpdaterInterface
    {
        bool updateKeywords(Answer ans, List<string> keywords, bool correct){
			
			//query database with correctness boolean to see if the answer has recently been modified in this way
			
			bool recentlyModified = false;
			
			//return false if the answer was recently modified this way
			if(!recently Modified){
				return false;
			}
			//otherwise:
			
			if(correct){
				return incrementKeywords(ans, keywords);
			}else{
				return decrementKeywords(ans, keywords);
			}
		}
		
		bool incrementKeywords(Answer ans, List<string> keywords){
		
			//access the database to update the keywords listed attached to the answer up to the max
			
			//add a timestamp for a recent increment in the DB
			
		}
		
		bool decrementKeywords(Answer ans, List<string> keywords){
		
			//access the database to update the keywords listed attached to the answer down to the min
			
			//add a timestamp for a recent decrement in the DB
			
		}
    }
}
