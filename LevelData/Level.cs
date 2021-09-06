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

        /// <summary>Images saved as Base64</summary>
        public string[] LevelImagesData { get; set; }

        /// <summary>Name of Layer. Size of <see cref="LayerNames"/> has to be equal with count of <see cref="LevelImages"/> first dimension</summary>
        public string[] LayerNames { get; set; }

        /// <summary>
        /// Image index of images in level.
        /// First dimension: amount of layer; Second dimension: amount of images. Value is index in <see cref="LevelImagesData"/>.
        /// </summary>
        public int[,] LevelImages { get; set; }

        /// <summary>
        /// Save data as JSON to file
        /// </summary>
        /// <param name="_absolutePath">absolute Path of file</param>
        /// <param name="_overwrite">overwrite old file if it exists</param>
        /// <returns>If save was successful return true; else false</returns>
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

        /// <summary>
        /// Save data as JSON file
        /// </summary>
        /// <param name="_absolutePath">absolute Path of file</param>
        /// <param name="_exception">Exception if one was thrown</param>
        /// <param name="_overwrite">overwrite old file if it exists</param>
        /// <returns>If save was successful return true; else false</returns>
        public bool SaveJson(string _absolutePath, out Exception _exception, bool _overwrite = true)
        {
            _exception = null;

            // check if file already exists. If it exists and overwrite is false, return false
            if (File.Exists(_absolutePath)
                && !_overwrite)
                return false;
            
            try
            {
                // serialize JSON to a string and then write string to a file
                File.WriteAllText(_absolutePath, JsonConvert.SerializeObject(this));

                // serialize JSON directly to a file
                using (StreamWriter file = File.CreateText(_absolutePath))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(file, this);
                }
            }
            catch (Exception ex)
            {
                _exception = ex;
                return false;
            }

            return true;
        }

        /// <summary>
        /// Open a .lvd JSON file
        /// </summary>
        /// <param name="_absolutePath">absolute path of file</param>
        /// <returns></returns>
        public static Level ReadJSON(string _absolutePath)
        {
            // read file into a string and deserialize JSON to a type
            Level tr = JsonConvert.DeserializeObject<Level>(File.ReadAllText(_absolutePath));

            if (CheckLevel(tr))
                return tr;
            else
                return null;
        }

        /// <summary>
        /// Check if all of level data is correct
        /// </summary>
        /// <param name="_lvl">level to check</param>
        /// <returns>true if everything is fine; else false</returns>
        public static bool CheckLevel(Level _lvl)
        {
            if (_lvl == null
                || _lvl.LevelImagesData == null
                || _lvl.LevelImages == null)
                return false;

            if (_lvl.LevelSizeX < 1
                || _lvl.LevelSizeY < 1)
                return false;

            if (_lvl.SpriteSizeX < 1
                || _lvl.SpriteSizeY < 1)
                return false;

            foreach (string s in _lvl.LevelImagesData)
            {
                if (string.IsNullOrEmpty(s))
                    return false;
            }

            if (_lvl.LevelImages.GetLength(1) != _lvl.LevelSizeX * _lvl.LevelSizeY)
                return false;

            if (_lvl.LayerNames != null
                && _lvl.LayerNames.Length != _lvl.LevelImages.GetLength(0))
                return false;

            return true;
        }
    }
}
