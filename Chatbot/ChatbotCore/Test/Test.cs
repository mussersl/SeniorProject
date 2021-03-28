using System;
using Chatbot;
using System.Collections.Generic;
using ChatbotCore.DatabaseInteraction;


namespace Chatbot
{
    public class Test
    {
        static void Main(string[] args)
        {
            //ControlFlow control = new ControlFlow();
            //while (true)
            //{
            //    Console.WriteLine("Welcome to the IRPA chatbot.");
            //    string question = Console.ReadLine();
            //    Answer ans = new Answer();
            //    Answer ans2 = new Answer();
            //    ans.ID = "0001";
            //    ans.question = "What does IRPA stand for?";
            //    ans.answer = "IRPA stands for Institutional Research Planning and Assessment.";
            //    ans.keywords = new System.Collections.Generic.Dictionary<string, double>();
            //    ans.keywords.Add("what", 0.7);
            //    ans.keywords.Add("IRPA", 0.4);
            //    ans.keywords.Add("stand", 0.8);
            //    ans2.ID = "0001";
            //    ans2.question = "How many students are enrolled at Rose-Hulman?";
            //    ans2.answer = "In 2015, 2,304 students were enrolled at Rose-Hulman";
            //    ans2.keywords = new System.Collections.Generic.Dictionary<string, double>();
            //    ans2.keywords.Add("many", 0.6);
            //    ans2.keywords.Add("students", 0.4);
            //    ans2.keywords.Add("enrolled", 0.8);
            //    List<string> keywords1 = new List<string>();
            //    List<string> keywords2 = new List<string>();

            //    KeywordParserInterface kp = new AllWordsParser();
            //    List<string> keywords = kp.parseQuestion(question);
            //    RelevancyAnalyzerInterface ra = new RelevancyAnalyzer();
            //    ra.assignRelevencyOf(ans, keywords, null);
            //    ra.assignRelevencyOf(ans2, keywords, null);
            //    List<Answer> listAns = new List<Answer>();
            //    listAns.Add(ans);
            //    listAns.Add(ans2);
            //    ra.sortAnswers(listAns);

            //    ResponseGeneratorInterface rg = new ResponseGeneratorR1();
            //    List<string> response = rg.generateResponse(listAns);
            //    foreach(string an in response)
            //    {
            //        Console.WriteLine(an);
            //    };

            //}
            databaseConnectionTest();

        }

        static void databaseConnectionTest()
        {
            DatabaseQueryInterface db = new Database();
            List<String> keywords = new List<string>();
            keywords.Add("what");
            keywords.Add("IRPA");
            keywords.Add("stand");
            db.queryDatabaseOnKeywords(keywords);
        }
    }
}