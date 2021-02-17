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
            return null;
        }
        public void sortAnswers(List<Answer> answers)
        {
            answers.Sort((Answer a,Answer b) => {return a.relevency.CompareTo(b.relevency);});
        }
    }
}