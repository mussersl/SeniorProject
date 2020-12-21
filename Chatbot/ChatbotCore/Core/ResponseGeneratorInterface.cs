using System;
using Chatbot;

namespace Chatbot
{
    public interface ResponseGeneratorInterface
    {
        List<String> generateResponse(List<Answer> answers);
    }
}