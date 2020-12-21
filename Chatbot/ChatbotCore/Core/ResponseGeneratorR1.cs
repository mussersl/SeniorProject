using System;
using Chatbot;

namespace Chatbot
{
    public class ResponseGeneratorR1 : ResponseGeneratorInterface
    {
        List<String> generateResponse(List<Answer> answers){
            var hold = new List<String>();
            int hold2 = -1;
            int hold3 = -1;
            int count = 0;
            foreach (Answer keep in answers){
                if(keep.relevency > hold3){
                    hold3 = keep.relevency;
                    hold2 = count;
                    count += 1;
                }
            }
            if(hold3 = -1){
                hold.Add("No valid response detected");
                return hold;
            }
            hold.Add(answers[hold3].Answer);
            return hold;
        }
    }
}
