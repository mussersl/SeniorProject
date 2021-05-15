using System;
using Chatbot;
using System.Collections.Generic;

namespace Chatbot
{
    public interface KeywordParserInterface
    {
        List<string> parseQuestion(string question);
    }
}
