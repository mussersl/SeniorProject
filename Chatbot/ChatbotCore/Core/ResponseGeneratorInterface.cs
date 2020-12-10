using System;
using Chatbot;

namespace Chatbot
{
    public class ResponseGeneratorInterface
    {
        List<String> generateResponse(List<Answer> answers);
    }
}