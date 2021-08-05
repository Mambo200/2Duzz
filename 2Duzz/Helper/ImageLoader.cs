using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2Duzz.Helper
{
    public static class ImageLoader
    {
        private const string LEVELIMAGEDIRECTORY = "From File";

        /// <summary>
        /// Saves the images from <see cref="LevelData.Level.LevelImagesData"/> to a directory
        /// </summary>
        /// <param name="_imageData">Base64 ImageData</param>
        /// <param name="_absolutePath">Absolute Path of file</param>
        public static void SaveLevelImagesFromFileToDirectory(string[] _imageData, string _absolutePath)
        {
            // get and create main image folder
            DirectoryInfo main = FileHelper.GetImageMainFolder(_absolutePath);

            // create folder for images in file
            DirectoryInfo sub = Directory.CreateDirectory(Path.Combine(main.FullName, LEVELIMAGEDIRECTORY));

            // Save image data to folder
            for (int i = 0; i < _imageData.Length; i++)
            {
                System.Drawing.Image img = null;
                try
                {
                    img = MyConverter.FromBase64(_imageData[i], out MemoryStream ms);
                    string path = Path.Combine(sub.FullName, i.ToString() + ".png");
                    img.Save(path, System.Drawing.Imaging.ImageFormat.Png);
                }
                catch (Exception _ex)
                {
                    System.Windows.MessageBox.Show(_ex.Message, "Exception!");
                }
                finally
                {
                    if(img != null)
                        img.Dispose();
                }
            }
        }

        public static string[] LoadImagesFromLevelFolderToTabItem(string _levelPath, System.Windows.Input.MouseButtonEventHandler _leftClick, System.Windows.Input.MouseButtonEventHandler _rightClick, out System.Windows.Controls.TabItem _tabItem)
        {
            _tabItem = null;

            // get main image folder
            DirectoryInfo main = FileHelper.GetImageMainFolder(_levelPath);

            string imageDirectoryPath = Path.Combine(main.FullName, LEVELIMAGEDIRECTORY);
            // If folder with level images does not exist return immediately
            if (!Directory.Exists(imageDirectoryPath))
                return null;

            string[] images = Directory.GetFiles(imageDirectoryPath, "*.png", SearchOption.TopDirectoryOnly);

            TabItemManager.Get.DeleteFromFileTab();
            _tabItem = TabItemManager.Get.AddTabItem(LEVELIMAGEDIRECTORY);
            TabItemManager.Get.AddFromFileTab(_tabItem);

            for (int i = 0; i < images.Length; i++)
            {
                TabItemManager.Get.AddImageToTabItem(_tabItem, new Uri(images[i]), _leftClick, _rightClick);
            }

            return images;
        }

        public static string[] LoadImagesFromFolderToTabItem(string _folderPath, System.Windows.Input.MouseButtonEventHandler _leftClick, System.Windows.Input.MouseButtonEventHandler _rightClick, out System.Windows.Controls.TabItem _tabItem)
        {
            _tabItem = null;

            // get main image folder
            DirectoryInfo main = FileHelper.GetImageMainFolder(_folderPath);

            string imageDirectoryPath = main.FullName;
            // If folder with level images does not exist return immediately
            if (!Directory.Exists(main.FullName))
                return null;

            string[] images = Directory.GetFiles(main.FullName, "*.png", SearchOption.TopDirectoryOnly);

            TabItemManager.Get.DeleteFromFileTab();
            _tabItem = TabItemManager.Get.AddTabItem(main.Name);
            TabItemManager.Get.AddFromFileTab(_tabItem);

            for (int i = 0; i < images.Length; i++)
            {
                TabItemManager.Get.AddImageToTabItem(_tabItem, new Uri(images[i]), _leftClick, _rightClick);
            }

            return images;
        }

    }
}
