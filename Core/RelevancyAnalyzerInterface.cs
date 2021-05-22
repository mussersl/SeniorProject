using System;
using Chatbot;
using System.Collections.Generic;

namespace Chatbot
{
    public interface RelevancyAnalyzerInterface
    {
        Answer assignRelevencyOf(Answer ans, List<string> keywords);
        void sortAnswers(List<Answer> answers);
    }
}
