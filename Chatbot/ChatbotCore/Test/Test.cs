using System;
using Chatbot;

namespace Chatbot
{
    public class Test
    {
        static void Main(string[] args)
        {
            ControlFlow control = new ControlFlow();
            while (true)
            {
                Console.WriteLine("Welcome to the IRPA chatbot.");
                string question = Console.ReadLine();
                string answer = control.askQuestion(question);
                Console.WriteLine(answer);
                Console.WriteLine("Now running test on predetermined Answer");
                
            }
        }
    }
}