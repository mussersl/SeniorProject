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

        [HttpGet, Route("Edit/{id}/{question}/{answer}")]
        public bool Edit(string id, string question, string answer)
        {
            Console.WriteLine("EDITTING");
            DatabaseEditor db = new Database();
            Answer a = new Answer(id, question, answer);
            a.printToConsole();
            db.editAnswer(a);
            return false;
        }

        [HttpGet, Route("Delete/{id}")]
        public bool Delete(string id)
        {
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
            return "jfjrisduhi4858tru544hgiriu9wt45hgr59u4y8gwwhtiw";
        }

        [HttpGet, Route("VerifyLogin/{password}")]
        public string VerifyLogin(string password)
        {
            if (password == "jfjrisduhi4858tru544hgiriu9wt45hgr59u4y8gwwhtiw")
            {
                return "VerifiedCertificate";
            }
            else
            {
                return "No";
            }
        }
    }
}
