using System;
using Chatbot;
using System.Collections.Generic;

namespace Chatbot
{
    public interface DatabaseQueryInterface
    {
        List<Answer> queryDatabaseOnKeywords(List<string> keywords);
        List<Answer> queryDatabaseOnAnswer(Answer ans);
    }
}