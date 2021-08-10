using _2Duzz.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace _2Duzz.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        /// <summary>
        /// Constructor of MV. This is called AFTER <see cref="MainWindow.MainWindow"/> so we can't set command there.
        /// </summary>
        public MainViewModel()
        {
            //HeaderNewClickCommand = new RelayCommand((e) => ExecuteHeaderNewClick("MV Constructor New"));
            HeaderOpenClickCommand = new RelayCommand((e) => ExecuteHeaderOpenClick("MV Constructor Open"));
            HeaderSaveClickCommand = new RelayCommand((e) => ExecuteHeaderSaveClick("MV Constructor Save"));
            HeaderSaveAsClickCommand = new RelayCommand((e) => ExecuteHeaderSaveAsClick("MV Constructor SaveAs"));
            HeaderCloseClickCommand = new RelayCommand((e) => ExecuteHeaderCloseClick("MV Constructor Close"));

            GridContentScale = 1;
            GridContentWidth = 1200;
            GridContentHeight = 1200;

            ImageSizeX = 600;
            ImageSizeY = 600;

        }

        #region Statusbar2
        private object m_StatusBarContent;
        public object StatusBarContent
        {
            get => m_StatusBarContent;
            set
            {
                SetProperty(ref m_StatusBarContent, value);
                //NotifyOfPropertyChange(nameof([PROPERTY]));
            }
        }

        private object m_StatusBarScale;
        public object StatusBarScale
        {
            get => m_StatusBarScale;
            set
            {
                SetProperty(ref m_StatusBarScale, value);
                //NotifyOfPropertyChange(nameof([PROPERTY]));
            }
        }
        #endregion


        #region KeyBinding

        #region Header FILE
        #region Header New Click
        private ICommand m_HeaderNewClickCommand;
        /// <summary>
        /// Header New Click command
        /// </summary>
        public ICommand HeaderNewClickCommand
        {
            get => m_HeaderNewClickCommand;
            set
            {
                SetProperty(ref m_HeaderNewClickCommand, value);
            }
        }

        /// <summary>
        /// Header New Click Execution method
        /// </summary>
        /// <param name="_parameter"></param>
        private void ExecuteHeaderNewClick(object _parameter)
        {
            WindowsXAML.NewMap newMap = new WindowsXAML.NewMap();
            newMap.ShowDialog();
            //PanelManager.Get.CreatePanel();
            //for (int x = 0; x < 10; x++)
            //{
            //    for (int y = 0; y < 10; y++)
            //    {
            //        ImageManager.Get.AddImageToPanel(0);
            //    }
            //}
        }
        #endregion

        #region Header Open Click
        private ICommand m_HeaderOpenClickCommand;
        /// <summary>
        /// Header Open Click command
        /// </summary>
        public ICommand HeaderOpenClickCommand
        {
            get => m_HeaderOpenClickCommand;
            set
            {
                SetProperty(ref m_HeaderOpenClickCommand, value);
            }
        }

        /// <summary>
        /// Header Open Click Execution method
        /// </summary>
        /// <param name="_parameter"></param>
        private void ExecuteHeaderOpenClick(object _parameter)
        {
            MessageBox.Show(_parameter.ToString());
        }
        #endregion

        #region Header Save Click
        private ICommand m_HeaderSaveClickCommand;
        /// <summary>
        /// Header Save Click command
        /// </summary>
        public ICommand HeaderSaveClickCommand
        {
            get => m_HeaderSaveClickCommand;
            set
            {
                SetProperty(ref m_HeaderSaveClickCommand, value);
            }
        }

        /// <summary>
        /// Header Save Click Execution method
        /// </summary>
        /// <param name="_parameter"></param>
        private void ExecuteHeaderSaveClick(object _parameter)
        {
            MessageBox.Show(_parameter.ToString());
        }
        #endregion

        #region Header SaveAs Click
        private ICommand m_HeaderSaveAsClickCommand;
        /// <summary>
        /// Header SaveAs Click command
        /// </summary>
        public ICommand HeaderSaveAsClickCommand
        {
            get => m_HeaderSaveAsClickCommand;
            set
            {
                SetProperty(ref m_HeaderSaveAsClickCommand, value);
            }
        }

        

        /// <summary>
        /// Header SaveAs Click Execution method
        /// </summary>
        /// <param name="_parameter"></param>
        private void ExecuteHeaderSaveAsClick(object _parameter)
        {
            MessageBox.Show(_parameter.ToString());
        }
        #endregion

        #region Header Close Click
        private ICommand m_HeaderCloseClickCommand;
        /// <summary>
        /// Header Close Click command
        /// </summary>
        public ICommand HeaderCloseClickCommand
        {
            get => m_HeaderCloseClickCommand;
            set
            {
                SetProperty(ref m_HeaderCloseClickCommand, value);
            }
        }

        /// <summary>
        /// Header Close Click Execution method
        /// </summary>
        /// <param name="_parameter"></param>
        private void ExecuteHeaderCloseClick(object _parameter)
        {
            // We cannot call "Application.Current.Shutdown();" because this would kill the application even though we cancelled it
            Application.Current.MainWindow.Close();
        }
        #endregion

        #region Button Add Layer Click
        private ICommand m_ButtonAddLayerClickCommand;
        /// <summary>
        /// Button Add Layer Click command
        /// </summary>
        public ICommand ButtonAddLayerClickCommand
        {
            get => m_ButtonAddLayerClickCommand;
            set
            {
                SetProperty(ref m_ButtonAddLayerClickCommand, value);
            }
        }
        #endregion

        #region Button Remove Layer Click
        private ICommand m_ButtonRemoveLayerClickCommand;
        /// <summary>
        /// Button Add Layer Click command
        /// </summary>
        public ICommand ButtonRemoveLayerClickCommand
        {
            get => m_ButtonRemoveLayerClickCommand;
            set
            {
                SetProperty(ref m_ButtonRemoveLayerClickCommand, value);
            }
        }
        #endregion
        #endregion

        #region Header IMAGE
        private ICommand m_HeaderAddImagesCommand;
        /// <summary>
        /// Header New Click command
        /// </summary>
        public ICommand HeaderAddImagesCommand
        {
            get => m_HeaderAddImagesCommand;
            set
            {
                SetProperty(ref m_HeaderAddImagesCommand, value);
            }
        }

        #endregion
        #endregion

        #region MouseBinding
        #endregion


        #region Canvas
        private double m_GridContentScale;
        public double GridContentScale
        {
            get => m_GridContentScale;
            set
            {
                SetProperty(ref m_GridContentScale, value);
                m_StatusBarScale = m_GridContentScale;
                NotifyOfPropertyChange(nameof(StatusBarScale));
            }
        }

        private double m_GridContentWidth;
        public double GridContentWidth
        {
            get => m_GridContentWidth;
            set
            {
                SetProperty(ref m_GridContentWidth, value);
                //NotifyOfPropertyChange(nameof([PROPERTY]));
            }
        }

        private double m_GridContentHeight;
        public double GridContentHeight
        {
            get => m_GridContentHeight;
            set
            {
                SetProperty(ref m_GridContentHeight, value);
                //NotifyOfPropertyChange(nameof([PROPERTY]));
            }
        }

        
        #endregion

        #region Image Size
        private double m_ImageSizeX;
        public double ImageSizeX
        {
            get => m_ImageSizeX;
            set
            {
                SetProperty(ref m_ImageSizeX, value);
                //NotifyOfPropertyChange(nameof([PROPERTY]));
            }
        }

        private double m_ImageSizeY;
        public double ImageSizeY
        {
            get => m_ImageSizeY;
            set
            {
                SetProperty(ref m_ImageSizeY, value);
                //NotifyOfPropertyChange(nameof([PROPERTY]));
            }
        }
        #endregion
    }
}
