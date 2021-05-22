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

        private string question = "";

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

        [HttpPost, Route("Edit/{id}/{question}/{answer}")]
        public bool Edit(string id, string question, string answer)
        {
            return false;
        }

        [HttpPost, Route("Delete/{id}")]
        public bool Delete(string id)
        {
            return false;
        }

        [HttpGet, Route("GetAll")]
        public IEnumerable<Answer> GetAll()
        {

            List<string> state = new List<string>();

            state.Add("How many students are enrolled?");
            state.Add("The number of students enrolled at Rose-Hulman in the year 2020 is 2038.");
            
            state.Add("What does IRPA stand for?");
            state.Add("IRPA stands for Institutional Research Planning and Assessment.");
            
            state.Add("What qualifies an undergraduate for part time status?");
            state.Add("Undergraduates that have fewer than 12 credit hours per quarter or fewer than 24 contact hours per quarter are considered part time.");
            
            state.Add("What qualifies a graduate for part time status?");
            state.Add("Graduates that are enrolled for fewer than 9 credit hours per quarter are considered part time.");

            return Enumerable.Range(0, 4).Select(index => new Answer 
            {
                answer = state.ElementAt(index*2+1),
                question = state.ElementAt(index*2)
            }).ToArray();
        }
    }
}
