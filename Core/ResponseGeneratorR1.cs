using System;
using Chatbot;
using System.Collections.Generic;

namespace Chatbot
{
    public class ResponseGeneratorR1 : ResponseGeneratorInterface
    {
        public List<string> generateResponse(List<Answer> answers){
            var hold = new List<string>();
            double hold2 = -1;
            double hold3 = -1;
            int count = 0;
			
			//Sort the list so that the most relevent answers are first
			Merge(answers, 0, answers.count);
			
			if(answers[0].relevancy < 30){
				hold.Add("No valid response detected");
			}
			
			if(answers.count <= 2){
				double first = answers[0].relevency;
				double second = answers[1].relevency;
				double percentDiff = ((first - second) / ((first + second) / 2)) * 100;
				if(diff < 8){
					hold.Add("Multiple potential responses:");
					for(int i = 0; i < answers.count; i++){
						double temp = answers[i].relevency;
						double diff = ((first - temp) / ((first + temp) / 2)) * 100;
						if(diff < 8)
							hold.Add(answers[i].answer);
					}
					return hold;
				}
			}
			
			hold.Add(answers[0].answer);
			return hold;
			
			/*
            foreach (Answer keep in answers){
                if(keep.relevency > hold3){
                    hold3 = keep.relevency;
                    hold2 = count;
                    count += 1;
                }
            }
            if(hold3 == -1){
                hold.Add("No valid response detected");
                return hold;
            }
            hold.Add(answers[(int)hold2].answer);
            return hold;
			
			*/
        }
		
		//Mergesort implementation originally written by Princi Singh, modified to work with answers
		
		public List<Answer> Merge(List<Answer> answers, int l, int m, int r){
		
			int n1 = m - l + 1;
        	int n2 = r - m;
			
		    List<Answer> L = new List<Answer>();
       		List<Answer> R = new List<Answer>();
        	int i, j;
			
			for (i = 0; i < n1; ++i)
            	L[i] = answers[l + i];
        	for (j = 0; j < n2; ++j)
            	R[j] = answers[m + 1 + j];
				
			i = 0;
       		j = 0;
			
			int k = l;
        	while (i < n1 && j < n2) {
            	if (L[i].relevancy >= R[j].relevency) {
                	answers[k] = L[i];
                	i++;
            	}
            	else {
                	answers[k] = R[j];
                	j++;
            	}
            	k++;
        	}
			
			while (i < n1) {
            	answers[k] = L[i];
            	i++;
            	k++;
        	}
			
			while (j < n2) {
            	answers[k] = R[j];
            	j++;
            	k++;
        	}
			
		}
		
		public void Sort(List<Answer> answers, int l, int r){
			if (l < r) {
           
            	int m = l+ (r-l)/2;
 
            
            	Sort(answers, l, m);
            	Sort(answers, m + 1, r);
 
            	Merge(arr, l, m, r);
        	}
		}
		
    }
}
