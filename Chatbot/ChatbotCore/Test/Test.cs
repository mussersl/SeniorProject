using System;
using Chatbot;
using System.Collections.Generic;

namespace Chatbot
{
    public class Test
    {
        static void Main(string[] args)
        {
            ControlFlow control = new ControlFlow();
            Console.WriteLine("Welcome to the IRPA chatbot.");
            while (true)
            {
                Console.WriteLine("Please ask a question.");
                string question = Console.ReadLine();
                string answer = control.askQuestion(question);
                Console.WriteLine(answer);
                //Console.WriteLine("Now running test on predetermined Question.");
                if (question.Equals("What does IRPA stand for?"))
                {
                    //Currently cannot connect to database on test computer.
                    Answer ans = new Answer();
                    ans.ID = "0001";
                    ans.question = "What does IRPA stand for?";
                    ans.answer = "IRPA stands for Institutional Research Planning and Assessment.";
                    ans.keywords = new System.Collections.Generic.Dictionary<string, double>();
                    ans.keywords.Add("what", 0.7);
                    ans.keywords.Add("IRPA", 0.4);
                    ans.keywords.Add("stand", 0.8);
                    ans.relevency = 0.8;
                    List<Answer> listAns = new List<Answer>();
                    listAns.Add(ans);
                    ResponseGeneratorInterface rg = new ResponseGeneratorR1();
                    List<string> response = rg.generateResponse(listAns);
                    Console.WriteLine(ans.question);
                    foreach (string an in response)
                    {
                        Console.WriteLine(an);
                    };
                }
            }
        }
    }
}