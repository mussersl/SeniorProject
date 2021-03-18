using System;
using System.Diagnostics;
using Chatbot;
using System.Collections.Generic;

namespace Chatbot
{
    public class RelevancyAnalyzer : RelevancyAnalyzerInterface 
    {
        public Answer assignRelevencyOf(Answer ans, List<string> keywords, List<string> recentKeywords) 
        {
            double total = 0;
            foreach(string key in keywords)
            {
                if (ans.keywords.ContainsKey(key))
                {
                    double temp;
                    ans.keywords.TryGetValue(key, out temp);
                    total += temp;
                }
            }
            ans.relevency = total;
            return ans;
        }
        public void sortAnswers(List<Answer> answers)
        {
            answers.Sort((Answer a,Answer b) => {return a.relevency.CompareTo(b.relevency);});
        }
    }
}