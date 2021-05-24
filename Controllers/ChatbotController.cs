using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chatbot;


namespace SeniorProj.Controllers
{
    [ApiController]
    [Route("Chatbot")]
    public class ChatbotController : ControllerBase
    {
        private string certificate = "jfjrisduhi4858tru544hgiriu9wt45hgr59u4y8gwwhtiw";
        private string verificationMessage = "VerifiedCertificate";
        private readonly ILogger<ChatbotController> _logger;

        public ChatbotController(ILogger<ChatbotController> logger)
        {
            _logger = logger;
        }

        [HttpGet, Route("Connect")]
        public string Connect()
        {
            return "Welcome to the IRPA frequently asked questions chatbot.";
        }

        [HttpGet, Route("Ask/{question}")]
        public string Ask(string question)
        {
            string temp = new ControlFlow().askQuestion(question);
            return temp;
        }

        [HttpGet, Route("Edit/{id}/{question}/{answer}/{certificate}")]
        public bool Edit(string id, string question, string answer, string certificate)
        {
            if (!certificate.Equals(this.certificate))
            {
                return false;
            }
            Console.WriteLine("EDITTING");
            DatabaseEditor db = new Database();
            Answer a = new Answer(id, question, answer);
            KeywordParserInterface kp = new AllWordsParser();
            List<string> keywords = kp.parseQuestion(question);
            db.editAnswer(a);
            a.printToConsole();
            foreach (string key in keywords)
            {
                ((Database)db).addKeywordToAnswer(a.ID, key);
            }
            return true;
        }

        [HttpGet, Route("Delete/{id}/{certificate}")]
        public bool Delete(string id, string certificate)
        {
            if(!certificate.Equals(this.certificate))
            {
                return false;
            }
            new Database().Delete(id);
            return true;
        }

        [HttpGet, Route("GetAll")]
        public IEnumerable<Answer> GetAll()
        {

            Database db = new Database();
            List<Answer> output = db.TotalOutput();
            return Enumerable.Range(0, output.Count).Select(index => output[index]).ToArray();
        }

        [HttpGet, Route("Login/{username}/{password}")]
        public string Login(string username, string password)
        {
            Database db = new Database();
            if (db.VerifyLogin(username, password))
            {
                return certificate;
            }
            return "Incorrect";
        }

        [HttpGet, Route("VerifyLogin/{password}")]
        public string VerifyLogin(string password)
        {
            if (password == certificate)
            {
                return verificationMessage;
            }
            else
            {
                return "No";
            }
        }


       public void helperCall (string answerID, string question, bool incrementing){
            Console.WriteLine("ANSWER ID: " + answerID + ": QUESTION: " + question);
            Database db = new Database();
            AllWordsParser AWP = new AllWordsParser();
            List<string> hold = AWP.parseQuestion(question);
            foreach(string s in hold){
                string keyID = db.queryDatabaseForKeywordID(s);
                Console.WriteLine(s + " ID: " + keyID);
                db.relevancyModification(answerID, keyID, incrementing);
            }

        }

        [HttpGet, Route("Increment/{answerID}/{question}")]
        public void incrementCall(string answerID, string question){
            helperCall(answerID, question, true);
        }

        [HttpGet, Route("Decrement/{answerID}/{question}")]
        public void decrementCall(string answerID, string question){
            helperCall(answerID, question, false);
        }
    }
}
