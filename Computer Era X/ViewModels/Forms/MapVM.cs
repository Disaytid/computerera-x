using System;
using System.Security.RightsManagement;
using System.Windows;
using Computer_Era_X.Models;

namespace Computer_Era_X.ViewModels
{
    public partial class GameVM
    {
        partial void MapInit()
        {

        }

        private Visibility _mapVisibility = Visibility.Visible;
        private string _browserTag;

        public Visibility MapVisibility
        {
            get => _mapVisibility;
            set => SetProperty(ref _mapVisibility, value);
        }

        public string BrowserSource => System.IO.Path.GetFullPath("." + new Uri("pack://application:,,,/Map/index.html").AbsolutePath);
        public string BrowserTag
        {
            get => _browserTag;
            set
            {
                SetProperty(ref _browserTag, value);
                RaisePropertyChanged();
                MessageBox.Show(value);
            } 
        }
    }
}
