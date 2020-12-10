using System;
using Chatbot;

namespace Chatbot
{
    public interface RelevancyAnalyzerInterface
    {
        Answer assignRelevencyOf(Answer ans, List<String> keywords, List<String> recentKeywords);
    }
}
