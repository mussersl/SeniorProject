using System;
using System.Diagnostics;
using Chatbot;

namespace Chatbot
{
    public class RelevancyAnalyzer : RelevancyAnalyzerInterface 
    {
        Answer assignRelevencyOf(Answer ans, List<String> keywords, List<String> recentKeywords) 
        {
            return null;
        }
        void sortAnswers(List<Answer> answers)
        {
            answers.sort((answer a,answer b) => {return a.relevency.CompareTo(b.relevency);});
        }
    }
}