using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Tetris
{
    [ApiController]
    [Route("[controller]")]
    public class TetrisController : ControllerBase
    {
        private static readonly List<GameInfo> TheInfo = new List<GameInfo>
        {
            new GameInfo
            {
                Id = 2,
                Title = "Tetris",
                Author = "Fall 2023 Semester",
                Content = "", // This will be filled in dynamically
                DateAdded = "",
                Description = "Tetris 2 is a classic arcade puzzle game where the player has to arrange falling blocks, also known as Tetronimos, of different shapes and colors to form complete rows on the bottom of the screen. The game gets faster and harder as the player progresses, and ends when the Tetronimos reach the top of the screen.",
                HowTo = "Control with arrow keys: Up arrow to spin, down to speed up fall, space to insta-drop.",
                Thumbnail = "/images/tetris.jpg"
            }
        };

        private readonly ILogger<TetrisController> _logger;
        private readonly string _jsFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "js", "tetris.js");

        public TetrisController(ILogger<TetrisController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<GameInfo> Get()
        {
            // Read the JS file content and pass it to the GameInfo object
            string jsContent = ReadJsFile();
            if (!string.IsNullOrEmpty(jsContent))
            {
                // Update the Content property of the GameInfo object with the JS code
                TheInfo[0].Content = jsContent;
            }

            return TheInfo;
        }

        private string ReadJsFile()
        {
            try
            {
                if (System.IO.File.Exists(_jsFilePath))
                {
                    // Read the file as a string
                    return System.IO.File.ReadAllText(_jsFilePath);
                }
                else
                {
                    _logger.LogWarning("JavaScript file not found: " + _jsFilePath);
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error reading JavaScript file: " + ex.Message);
                return null;
            }
        }
    }
}
