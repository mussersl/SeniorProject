using System;
using Chatbot;

namespace Chatbot
{
    public interface KeywordParserInterface
    {
        List<String> parseQuestion(String question);
    }
}
