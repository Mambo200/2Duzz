using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

//JSON
using Newtonsoft.Json;

namespace LevelData
{
    public class Level
    {
        /// <summary>
        /// Create Level
        /// </summary>
        public Level()
        {
            LevelName = "Custom Level";
        }

        /// <summary>
        /// Create Level
        /// </summary>
        /// <param name="_levelName">Name of level</param>
        public Level(string _levelName)
        {
            LevelName = _levelName;
        }

        /// <summary>
        /// Create Level
        /// </summary>
        /// <param name="_levelName">Name of level</param>
        /// <param name="_levelSizeX">Width of level</param>
        /// <param name="_levelSizeY">Height of level</param>
        public Level(string _levelName, int _levelSizeX, int _levelSizeY)
        {
            LevelName = _levelName;
            LevelSizeX = _levelSizeX;
            LevelSizeY = _levelSizeY;
        }

        /// <summary>
        /// Create Level
        /// </summary>
        /// <param name="_levelName">Name of level</param>
        /// <param name="_levelSizeX">Width of level</param>
        /// <param name="_levelSizeY">Height of level</param>
        /// <param name="_spriteSizeX">Width of sprites</param>
        /// <param name="_spriteSizeY">Height of sprites</param>
        public Level(string _levelName, int _levelSizeX, int _levelSizeY, int _spriteSizeX, int _spriteSizeY)
        {
            LevelName = _levelName;
            LevelSizeX = _levelSizeX;
            LevelSizeY = _levelSizeY;
            SpriteSizeX = _spriteSizeX;
            SpriteSizeY = _spriteSizeY;
        }

        /// <summary>Name of level</summary>
        public string LevelName { get; set; }

        /// <summary>Width of level</summary>
        public int LevelSizeX { get; set; }
        /// <summary>Height of level</summary>
        public int LevelSizeY { get; set; }

        /// <summary>Width of sprites</summary>
        public int SpriteSizeX { get; set; }
        /// <summary>Height of sprites</summary>
        public int SpriteSizeY { get; set; }


        public bool SaveJson(string _absolutePath, bool _overwrite = true)
        {
            // check if file already exists. If it exists and overwrite is false, return false
            if (File.Exists(_absolutePath)
                && !_overwrite)
                return false;

            // serialize JSON to a string and then write string to a file
            File.WriteAllText(_absolutePath, JsonConvert.SerializeObject(this));

            // serialize JSON directly to a file
            using (StreamWriter file = File.CreateText(_absolutePath))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, this);
            }

            return true;
        }
    }
}
