using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeniorProj.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChatbotController : ControllerBase
    {
        
        public ChatbotController()
        {
            
        }

        [HttpGet]
        public string Get(string question)
        {
            return "question received" + question;
        }
    }
}
