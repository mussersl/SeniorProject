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
            hold.Add(answers[(int)hold3].answer);
            return hold;
        }
    }
}
