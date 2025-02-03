using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
namespace Snake
{
    [ApiController]
    [Route("[controller]")]
    public class SnakeController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<SnakeController> _logger;

        private static List<GameInfo> TheInfo = new List<GameInfo>();

        public SnakeController(IWebHostEnvironment env, ILogger<SnakeController> logger)
        {
            _env = env;
            _logger = logger;

            string snakeFilePath = Path.Combine(_env.WebRootPath, "js", "snake.js");

            string snakeFile = System.IO.File.ReadAllText(snakeFilePath);

            

            TheInfo = new List<GameInfo>
            {
                new GameInfo
                {
                    Id = 1,
                    Title = "Snake",
                    Content = snakeFile,
                    Author = "Fall 2023 Semester",
                    DateAdded = "",
                    Description = "Snake is a classic arcade game that challenges the player to control a snake-like creature that growslonger as it eats apples. The player must avoid hitting the walls or the snake's own body, which can end the game.\r\n",
                    HowTo = "Control with arrow keys.",
                    Thumbnail = "/images/snake.jpg" //640x360 resolution
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
