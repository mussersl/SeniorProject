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
				hold.Add("Something's wrong");
				return hold;
            }

			//If no answer had a score above 30, then no answer is determined as valid
			if (answers[0].relevency < 30){
				hold.Add("No valid response detected");
				return hold;
			}
			
			
			//If there are two or more answers
			if(answers.Count >= 2){
				double first = answers[0].relevency;
				double second = answers[1].relevency;
				//Check if the top 2 answers have a score within 8% of each other
				double percentDiff = ((first - second) / ((first + second) / 2)) * 100;
				if(percentDiff < 8){
					hold.Add("Multiple potential responses:");
					for(int i = 0; i < answers.Count; i++){
						//Add all answers with a score within 8% to the list of answers
						double temp = answers[i].relevency;
						double diff = ((first - temp) / ((first + temp) / 2)) * 100;
						if(diff < 8)
							hold.Add(answers[i].answer);
						else
							//return if an answer is outside of the threshold
							return hold;
					}
					//return either if all answers were in the threshold
					return hold;
				}
			}
			//Return if only one answer was deemed valid
			hold.Add(answers[0].answer);
			return hold;

			
			
        }
		
		
    }
}
