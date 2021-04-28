using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chatbot
{
    public interface IStopWordFilter
    {
        bool IsStopWord(string word);
        bool IsPunctuation(string word);
    }
}