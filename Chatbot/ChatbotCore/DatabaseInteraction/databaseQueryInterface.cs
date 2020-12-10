using System;
using Chatbot;

namespace Chatbot
{
    public interface DatabaseQueryInterface
    {
        public List<Answer> queryDatabaseOnKeywords(List<String> keywords);
        public List<Answer> queryDatabaseOnAnswer(Answer ans);
    }
}