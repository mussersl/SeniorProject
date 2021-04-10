using System;
using Chatbot;
using System.Collections.Generic;

namespace Chatbot
{
    public class KeywordUpdater: KeywordRelevencyUpdaterInterface
    {
        bool updateKeywords(Answer ans, List<string> keywords, bool correct, database subject){
			
			//query database with correctness boolean to see if the answer has recently been modified in this way
			
			bool recentlyModified = false;
			
			//return false if the answer was recently modified this way
			if(!recentlyModified){
				return false;
			}
			//otherwise:
			
			if(correct){
				return incrementKeywords(ans, keywords, subject);
			}else{
				return decrementKeywords(ans, keywords, subject);
			}
		}
		
		bool incrementKeywords(Answer ans, List<string> keywords, database subject){

			//access the database to update the keywords listed attached to the answer up to the max
			Answer previousAnswer = subject.queryDatabaseOnAnswer(ans);
			double outCheck = 0;
			foreach (string hold in keywords){
				
				if(previousAnswer.keywords.TryGetValue(hold, out outCheck)){
					outCheck++;
					if(outCheck > 100){
						outCheck = 100;
					}
					previousAnswer.keywords[hold] = outCheck;
				}else{
					previousAnswer.keywords.Add(hold, 1);
				}

			}

			//add a timestamp for a recent increment in the DB
			return true;
			
		}
		
		bool decrementKeywords(Answer ans, List<string> keywords){

			//access the database to update the keywords listed attached to the answer down to the min
			Answer previousAnswer = subject.queryDatabaseOnAnswer(ans);
			double outCheck = 0;
			foreach (string hold in keywords){
				
				if(previousAnswer.keywords.TryGetValue(hold, out outCheck)){
					outCheck--;
					if(outCheck < 0){
						outCheck = 0;
					}
					previousAnswer.keywords[hold] = outCheck;
				}

			}
			//add a timestamp for a recent decrement in the DB

			return true;
		}

        bool KeywordRelevencyUpdaterInterface.updateKeywords(Answer ans, List<string> keywords, bool correct)
        {
            throw new NotImplementedException();
        }
    }
}
