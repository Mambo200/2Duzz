using _2Duzz.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace _2Duzz.Config
{
    public class ConfigLoader
    {
        #region Singleton
        private static ConfigLoader m_Instance;
        private ConfigLoader() { }
        static ConfigLoader()
        {
            m_Instance = new ConfigLoader();
        }
        #endregion
        private const string SEPARATOR = "\t";

        public static ConfigLoader Get { get => m_Instance; }
        /// <summary>Folder name (not path)</summary>
        public const string DIRECTORYNAME = "2Duzz";
        /// <summary>File name (not path)</summary>
        public const string FILENAME = "2Duzz.config";
        /// <summary>Full path of Directory of config file (path)</summary>
        public static string ConfigDirectory
        {
            get => Path.Combine(
                Environment.GetFolderPath(
                    Environment.SpecialFolder.MyDocuments),
                DIRECTORYNAME);
        }
        /// <summary>Full path of config file</summary>
        public static string ConfigFile
        {
            get => Path.Combine(ConfigDirectory, FILENAME);
        }
        public static bool ConfigFileExists { get => File.Exists(ConfigFile); }
        public static bool ConfigDirectoryExists { get => Directory.Exists(ConfigDirectory); }

        private string[] FileContent { get; set; }

        private DirectoryInfo ConfigDirectoryInfo { get; set; }

        public bool LoadFile()
        {
            ConfigDirectoryInfo = Directory.CreateDirectory(ConfigDirectory);
            if (!File.Exists(ConfigFile))
            {
                using (Stream s = CreateConfigFile().BaseStream) { }
                FileContent = new string[0];
                return false;
            }

            // File does exist, read its content
            List<string> content = new List<string>();
            using (StreamReader reader = OpenConfigFile())
            {
                while(!reader.EndOfStream)
                {
                    content.Add(reader.ReadLine());
                }
            }
            FileContent = content.ToArray();

            return true;
        }
        public void WriteToFile()
        {
            // check if config directory and file exists. if not, create them.
            StreamWriter writer = null;
            if (!ConfigDirectoryExists)
            {
                Directory.CreateDirectory(ConfigDirectory);
                writer = CreateConfigFile();
            }
            else if (!ConfigFileExists)
            {
                writer = CreateConfigFile();
            }
            else
            {
                writer = new StreamWriter(ConfigFile);
            }

            // check for each TabItem in ItemControl
            for (int i = 0; i < TabItemManager.Get.ItemControl.Items.Count; i++)
            {
                TabItem temp = TabItemManager.Get.ItemControl.Items[i] as TabItem;
                if (temp == null
                    || temp.Tag == null)
                    continue;

                if(!IsCorrectFormat(temp.Tag as FolderInformation?, out FolderInformation fInfo))
                    continue;

                // tag has correct format.
                writer.WriteLine($"{fInfo.folder}{SEPARATOR}{BoolToNumber(fInfo.subfolders)}");
            }

            writer.Close();
            writer.Dispose();
        }

        /// <summary>
        /// Creates the config file
        /// </summary>
        /// <returns><see cref="StreamWriter"/> of file</returns>
        private StreamWriter CreateConfigFile()
        {
            return File.CreateText(ConfigFile);
        }
        /// <summary>
        /// Opens the config file. Make sure the file does exist.
        /// </summary>
        /// <returns><see cref="StreamReader"/> of file</returns>
        private StreamReader OpenConfigFile()
        {
            return File.OpenText(ConfigFile);
        }

        public void FillTabControl(MouseButtonEventHandler _leftButtonDown, MouseButtonEventHandler _rightButtonDown)
        {
            string[] separator = new string[] { SEPARATOR };
            foreach (string dir in FileContent)
            {
                Dictionary<string, string> failed = new Dictionary<string, string>();
                string[] split = dir.Split(separator, StringSplitOptions.None);
                bool subfolders = false;
                // check if split has correct length
                if (split.Length != 2)
                {
                    failed.Add(dir, "Wrong Format.");
                    continue;
                }
                // check if directory does exist
                else if (!Directory.Exists(split[0]))
                {
                    failed.Add(dir, "Directory does not exist.");
                    continue;
                }
                // check if number is actually a number
                else if(!int.TryParse(split[1], out int result))
                {
                    failed.Add(dir, "Number could not converted.");
                    continue;
                }
                else
                {
                    subfolders = NumberToBool(result);
                }

                DirectoryInfo dInfo = new DirectoryInfo(split[0]);
                FolderInformation fInfo = new FolderInformation(split[0], subfolders);

                SearchOption option = subfolders ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
                IEnumerable<string> files = Directory.EnumerateFiles(split[0], "*.*", option)
                    .Where(s =>
                        s.ToUpper().EndsWith("JPEG")
                        || s.ToUpper().EndsWith("JPG")
                        || s.ToUpper().EndsWith("JPE")
                        || s.ToUpper().EndsWith("JFIF")
                        || s.ToUpper().EndsWith("PNG")
                        || s.ToUpper().EndsWith("BMP")
                        || s.ToUpper().EndsWith("GIF")
                        || s.ToUpper().EndsWith("TIFF")
                        || s.ToUpper().EndsWith("TIF")
                        || s.ToUpper().EndsWith("JXR")
                        || s.ToUpper().EndsWith("ICO")
                        );

                // Add Tab
                TabItem item = TabItemManager.Get.AddTabItem(dInfo.Name, _tag:fInfo);

                // Add Images to Tab
                foreach (string file in files)
                {
                    try
                    {
                        TabItemManager.Get.AddImageToTabItem(item, new Uri(file), _leftButtonDown, _rightButtonDown);
                    }
                    catch (Exception _ex)
                    {
                        // save failed image with error message
                        failed.Add(file, _ex.Message);
                    }
                }

                // show User failed lines (if exist)
                if (failed.Count > 0)
                {
                    string caption = "Error";
                    string messageBoxText = "";
                    foreach (KeyValuePair<string, string> f in failed)
                    {
                        messageBoxText += $"Line: \"{f.Key}\"\nError: \"{f.Value}\"\n__\n";
                    }
                    messageBoxText = messageBoxText.Remove(messageBoxText.Length - 3);
                    MessageBox.Show(messageBoxText, caption, MessageBoxButton.OK, MessageBoxImage.Warning);
                }

            }
        }

        private bool NumberToBool(int _number)
        {
            return !(_number == 0);
        }
        private int BoolToNumber(bool _value)
        {
            return _value ? 1 : 0;
        }

        private bool IsCorrectFormat(FolderInformation? _info, out FolderInformation _notNullableFormat)
        {
            _notNullableFormat = new FolderInformation();
            if (_info != null)
            {
                _notNullableFormat = (FolderInformation)_info;
                return IsCorrectFormat(_notNullableFormat);
            }
            else
                return false;
        }

        private bool IsCorrectFormat(FolderInformation _info)
        {
            return Directory.Exists(_info.folder);
        }
    }
}
