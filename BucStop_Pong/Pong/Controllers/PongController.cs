using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pong;

using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
namespace Pong
{
    [ApiController]
    [Route("[controller]")]
    public class PongController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<PongController> _logger;

        private static List<GameInfo> TheInfo = new List<GameInfo>();

        public PongController(IWebHostEnvironment env, ILogger<PongController> logger)
        {
            _env = env;
            _logger = logger;

            string pongFilePath = Path.Combine(_env.WebRootPath, "js", "pong.js");

            string pongFile = System.IO.File.ReadAllText(pongFilePath);



            TheInfo = new List<GameInfo>
            {
                new GameInfo
                {
                    Id = 3,
                    Title = "Pong",
                    Content = pongFile,
                    Author = "Fall 2023 Semester",
                    DateAdded = "",
                    Description = "Pong is a classic arcade game where the player uses a paddle to hit a ball against a computer's paddle. Either party scores when the ball makes it past the opponent's paddle.",
                    HowTo = "Control with arrow keys.",
                    Thumbnail = "/images/pong.jpg" //640x360 resolution
                }
            };

        }

        [HttpGet]
        public IEnumerable<GameInfo> Get()
        {
            return TheInfo;
        }
    }
}
