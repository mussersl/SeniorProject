using System;
using Chatbot;
using System.Collections.Generic;

namespace Chatbot
{
    public class ResponseGeneratorR1 : ResponseGeneratorInterface
    {
        public List<string> generateResponse(List<Answer> answers){
            var hold = new List<string>();


			//Sort the list so that the most relevent answers are first
			answers.Sort((Answer a, Answer b) => { return a.relevency.CompareTo(b.relevency); });

			if(answers.Count == 0)
            {
				hold.Add("I'm sorry, I'm not sure what you're asking for right now.");
				return hold;
            }

			//If no answer had a score above 30, then no answer is determined as valid
			if (answers[0].relevency < 30){
				hold.Add("I'm not entirely sure what you were asking, but I think this is what you're looking for: ");
				hold.Add(answers[0].answer);
				return hold;
			}
			
			
			//If there are two or more answers
			if(answers.Count >= 2){
				double first = answers[0].relevency;
				double second = answers[1].relevency;
				//Check if the top 2 answers have a score within 8% of each other
				double percentDiff = ((first - second) / ((first + second) / 2)) * 100;
				if(percentDiff < 8){
					int totalIDs = 0;
					string ids = "";
					string idString = "";
					hold.Add("Multiple potential responses:");
					for(int i = 0; i < answers.Count; i++){
						//Add all answers with a score within 8% to the list of answers
						double temp = answers[i].relevency;
						double diff = ((first - temp) / ((first + temp) / 2)) * 100;
						if(diff < 8)
                        {
							hold.Add(answers[i].answer);
							totalIDs++;
							ids += answers[i].ID + " ";
						}
                        else
						{
							//return if an answer is outside of the threshold
							idString = totalIDs.ToString() + " " + ids;
							hold.Insert(0, idString);
							return hold;
						}
					}
					//return if all answers were in the threshold
					idString = totalIDs.ToString() + " " + ids;
					hold.Insert(0, idString);
					return hold;
				}
			}
			//Return if only one answer was deemed valid
			hold.Add(answers[0].answer);
			return hold;

			
			
        }
		
		
    }
}
