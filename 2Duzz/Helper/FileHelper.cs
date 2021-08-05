using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace _2Duzz.Helper
{
    public static partial class FileHelper
    {
        #region Constructor
        static FileHelper()
        {
            ResetLastValidFile();
        }
        #endregion

        /// <summary>Extension of level file</summary>
        private const string EXTENSION = "lvd";
        /// <summary>Filter for <see cref="FileDialog.Filter"/></summary>
        private static string Filter { get => $"Level Data|*.{EXTENSION}"; }

        /// <summary>File which was saved last</summary>
        public static string LastValidFile { get; private set; }

        /// <summary>
        /// Creates an <see cref="OpenFileDialog"/>.
        /// </summary>
        /// <param name="_dialog">the <see cref="OpenFileDialog"/></param>
        /// <returns>true if user choses a file; else false or null</returns>
        public static bool? GetOpenPath(out OpenFileDialog _dialog)
        {
            _dialog = new OpenFileDialog()
            {
                CheckFileExists = true,
                Filter = Filter,
                Multiselect = false
            };

            bool? result = _dialog.ShowDialog();

            if (result == true)
                LastValidFile = _dialog.FileName;

            return result;
        }

        /// <summary>
        /// Get a path the user choses
        /// </summary>
        /// <returns>file chosen</returns>
        public static string GetOpenPath()
        {
            OpenFileDialog d = new OpenFileDialog()
            {
                CheckFileExists = true,
                Filter = Filter,
                Multiselect = false
            };

            bool? result = d.ShowDialog();

            if (result == true)
                LastValidFile = d.FileName;

            return d.FileName;
        }

        /// <summary>
        /// Creates an <see cref="SaveFileDialog"/>.
        /// </summary>
        /// <param name="_dialog">the <see cref="SaveFileDialog"/></param>
        /// <returns>true if user choses a file; else false or null</returns>
        public static bool? GetSavePath(out SaveFileDialog _dialog)
        {
            _dialog = new SaveFileDialog()
            {
                Filter = Filter,
                AddExtension = true
            };

            bool? result = _dialog.ShowDialog();

            if (result == true)
                LastValidFile = _dialog.FileName;

            return result;
        }

        /// <summary>
        /// Get a path the user choses
        /// </summary>
        /// <returns>file chosen</returns>
        public static string GetSavePath()
        {
            SaveFileDialog d = new SaveFileDialog()
            {
                Filter = Filter,
                AddExtension = true
            };

            bool? result = d.ShowDialog();

            if (result == true)
                LastValidFile = d.FileName;

            return d.FileName;
        }

        /// <summary>
        /// Get folder a path the user choses
        /// </summary>
        /// <param name="_subfolders">true if checkbox "Include subfolders" was checked; else false</param>
        /// <returns>selected folder if user choosed one. to check if valid use <see cref="string.IsNullOrEmpty(string)"/></returns>
        public static string OpenFolderPath(out bool _subfolders)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog()
            {
                Multiselect = false,
                IsFolderPicker = true,
                EnsurePathExists = true,
                Title = "Open folder...",
            };

            dialog.Controls.Add(new Microsoft.WindowsAPICodePack.Dialogs.Controls.CommonFileDialogCheckBox("Include subfolders", false));
            _ = dialog.ShowDialog();
            _subfolders = ((Microsoft.WindowsAPICodePack.Dialogs.Controls.CommonFileDialogCheckBox)dialog.Controls[0]).IsChecked;
            return dialog.FileName;
        }

        /// <summary>
        /// Get folder a path the user choses
        /// </summary>
        /// <param name="_subfolders">true if checkbox "Include subfolders" was checked; else false</param>
        /// <param name="_main">Top-level WPF window that will own the modal dialog box.</param>
        /// <returns>selected folder if user choosed one. to check if valid use <see cref="string.IsNullOrEmpty(string)"/></returns>
        public static string OpenFolderPath(out bool _subfolders, System.Windows.Window _main)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog()
            {
                Multiselect = false,
                IsFolderPicker = true,
                EnsurePathExists = true,
                Title = "Open folder...",
            };

            dialog.Controls.Add(new Microsoft.WindowsAPICodePack.Dialogs.Controls.CommonFileDialogCheckBox("Include subfolders", false));
            _ = dialog.ShowDialog(_main);
            _subfolders = ((Microsoft.WindowsAPICodePack.Dialogs.Controls.CommonFileDialogCheckBox)dialog.Controls[0]).IsChecked;
            return dialog.FileName;
        }


        /// <summary>
        /// Get folder a path the user choses
        /// </summary>
        /// <param name="_dialog">the <see cref="CommonOpenFileDialog"/></param>
        /// <param name="_subfolders">true if checkbox "Include subfolders" was checked; else false</param>
        /// <param name="_main">Top-level WPF window that will own the modal dialog box.</param>
        /// <returns>true if user choses a folder; else false or null</returns>
        public static bool? OpenFolderPath(out CommonOpenFileDialog _dialog, out bool _subfolders, System.Windows.Window _main)
        {
            _dialog = new CommonOpenFileDialog()
            {
                Multiselect = false,
                IsFolderPicker = true,
                EnsurePathExists = true,
                Title = "Open folder...",
            };

            _dialog.Controls.Add(new Microsoft.WindowsAPICodePack.Dialogs.Controls.CommonFileDialogCheckBox("Include subfolders", false));
            CommonFileDialogResult result = _dialog.ShowDialog(_main);
            _subfolders = ((Microsoft.WindowsAPICodePack.Dialogs.Controls.CommonFileDialogCheckBox)_dialog.Controls[0]).IsChecked;
            return CommonFileDialogResultConverter(result);
        }

        /// <summary>
        /// Get folder a path the user choses
        /// </summary>
        /// <param name="_dialog">the <see cref="CommonOpenFileDialog"/></param>
        /// <param name="_subfolders">true if checkbox "Include subfolders" was checked; else false</param>
        /// <returns>true if user choses a folder; else false or null</returns>
        public static bool? OpenFolderPath(out CommonOpenFileDialog _dialog, out bool _subfolders)
        {
            _dialog = new CommonOpenFileDialog("NAME OF THIS DIALOG!")
            {
                Multiselect = false,
                IsFolderPicker = true,
                EnsurePathExists = true,
                Title = "Open folder..."
            };

            _dialog.Controls.Add(new Microsoft.WindowsAPICodePack.Dialogs.Controls.CommonFileDialogCheckBox("Include subfolders", false));
            CommonFileDialogResult result = _dialog.ShowDialog();
            _subfolders = ((Microsoft.WindowsAPICodePack.Dialogs.Controls.CommonFileDialogCheckBox)_dialog.Controls[0]).IsChecked;

            return CommonFileDialogResultConverter(result);
        }



        /// <summary>
        /// Reset value of <see cref="LastValidFile"/>
        /// </summary>
        public static void ResetLastValidFile() => LastValidFile = "";

        /// <summary>
        /// Statusbar text for saving a file
        /// </summary>
        /// <param name="_path">absolue path of file</param>
        /// <param name="_successful">operation successful?</param>
        /// <param name="_statusBar">Statusbar</param>
        public static void FileDialogSaveStatusText(string _path, bool _successful, IStatusBar _statusBar)
        {
            string preText;
            if (_successful)
                preText = "Saved successfully to ";
            else
                preText = "Error: Could not save to ";

            _statusBar.ChangeStatusBar(preText + $"\"{_path}\".");
        }

        /// <summary>
        /// Statusbar text for opening a file
        /// </summary>
        /// <param name="_path">absolue path of file</param>
        /// <param name="_successful">operation successful?</param>
        /// <param name="_statusBar">Statusbar</param>
        public static void FileDialogOpenStatusText(string _path, bool _successful, IStatusBar _statusBar)
        {
            string preText;
            if (_successful)
                preText = "File successfully opened: ";
            else
                preText = "Error: Failed to open file: ";

            _statusBar.ChangeStatusBar(preText + $"\"{_path}\".");
        }

        /// <summary>
        /// Get main image folder. Create it if it doesn't exist
        /// </summary>
        /// <param name="_levelFile">absolute path of level file location</param>
        /// <param name="_createFolder">true if folder shall be created if it not exists; else false.</param>
        /// <returns></returns>
        public static DirectoryInfo GetImageMainFolder(string _levelFile)
        {
            string s = RemoveExtension(_levelFile);

                if(Directory.Exists(s))
                    return new DirectoryInfo(s);
                else
                    return Directory.CreateDirectory(s);
        }

        private static bool? CommonFileDialogResultConverter(CommonFileDialogResult _result)
        {
            switch (_result)
            {
                case CommonFileDialogResult.Ok:
                    return true;
                case CommonFileDialogResult.Cancel:
                    return false;
                default:
                    return null;
            }
        }

        private static CommonFileDialogResult CommonFileDialogResultConverter(bool? _result)
        {
            switch (_result)
            {
                case true:
                    return CommonFileDialogResult.Ok;
                case false:
                    return CommonFileDialogResult.Cancel;
                default:
                    return CommonFileDialogResult.None;
            }
        }


        private static string RemoveExtension(string _file)
        {
            // get info
            FileInfo info = new FileInfo(_file);

            // remove extension
            string tr = info.FullName.Remove(
                info.FullName.Length - (info.Extension.Length),
                info.Extension.Length
                );

            return tr;
        }
    }
}
