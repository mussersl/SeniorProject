using System;
using Chatbot;
using System.Collections.Generic;

namespace Chatbot
{
    public interface KeywordRelevencyUpdaterInterface
    {
        bool updateKeywords(Answer ans, List<string> keywords, bool correct, database subject);
    }
}
