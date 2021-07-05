using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace _2Duzz.ViewModels
{
    public class NewMapViewModel : BaseViewModel
    {
        private const string LEVELNAMEDEFAULTVALUE = "Level Name";
        public NewMapViewModel()
        {
            NewFontSize = 20;

            LevelNameText = LEVELNAMEDEFAULTVALUE;

            SpriteSizeXText = "32";
            SpriteSizeYText = "32";

            LevelSizeXText = "Level Size X";
            LevelSizeYText = "Level Size Y";
            //ClickCreateLevel = new RelayCommand(ClickCreateButton);
            //ClickCancel = new RelayCommand(ClickCancelButton);
        }

        private double newFontSize;
        public double NewFontSize
        {
            get => newFontSize;
            set
            {
                SetProperty(ref newFontSize, value);
                //NotifyOfPropertyChange(nameof([PROPERTY]));
            }
        }

        #region Button Commands
        private ICommand m_ClickCreateLevel;
        public ICommand ClickCreateLevel
        {
            get => m_ClickCreateLevel;
            set
            {
                SetProperty(ref m_ClickCreateLevel, value);
            }
        }

        private ICommand m_ClickCancel;
        public ICommand ClickCancel
        {
            get => m_ClickCancel;
            set
            {
                SetProperty(ref m_ClickCancel, value);
            }
        }

        private void ClickCreateButton(object _parameter)
        {
            string b = this.ToString();
            MessageBox.Show("CREATE");
        }

        private void ClickCancelButton(object _parameter)
        {
            MessageBox.Show("CANCEL");
        }


        public bool CanClickButton
        {
            get => CanClick();
        }
        private bool CanClick()
        {
            if (!string.IsNullOrWhiteSpace(LevelNameText) 
                && LevelNameText != LEVELNAMEDEFAULTVALUE
                && int.TryParse(LevelSizeXText, out int temp1)
                && int.TryParse(LevelSizeYText, out int temp2)
                && int.TryParse(SpriteSizeXText, out int temp3)
                && int.TryParse(SpriteSizeYText, out int temp4))
                return true;
            else
                return false;
        }
        #endregion


        private string m_LevelNameText;
        public string LevelNameText
        {
            get => m_LevelNameText;
            set
            {
                SetProperty(ref m_LevelNameText, value);
                NotifyOfPropertyChange(nameof(CanClickButton));
            }
        }

        #region Level Size Values
        private string m_LevelSizeXText;
        public string LevelSizeXText
        {
            get => m_LevelSizeXText;
            set
            {
                SetProperty(ref m_LevelSizeXText, value);
                NotifyOfPropertyChange(nameof(CanClickButton));
            }
        }

        private string m_LevelSizeYText;
        public string LevelSizeYText
        {
            get => m_LevelSizeYText;
            set
            {
                SetProperty(ref m_LevelSizeYText, value);
                NotifyOfPropertyChange(nameof(CanClickButton));
            }
        }
        #endregion

        #region Sprite Size Values
        private string m_SpriteSizeXText;
        public string SpriteSizeXText
        {
            get => m_SpriteSizeXText;
            set
            {
                SetProperty(ref m_SpriteSizeXText, value);
                NotifyOfPropertyChange(nameof(CanClickButton));
            }
        }

        private string m_SpriteSizeYText;
        public string SpriteSizeYText
        {
            get => m_SpriteSizeYText;
            set
            {
                SetProperty(ref m_SpriteSizeYText, value);
                NotifyOfPropertyChange(nameof(CanClickButton));
            }
        }
        #endregion

    }
}
