using System;
using Chatbot;
using System.Collections.Generic;

namespace Chatbot
{
    public interface ResponseGeneratorInterface
    {
        List<string> generateResponse(List<Answer> answers);
    }
}