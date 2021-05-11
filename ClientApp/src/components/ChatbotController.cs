using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


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

        public static string AskTheBot(string question)
        {
            return "Question: " + question;
        }

    }
}
