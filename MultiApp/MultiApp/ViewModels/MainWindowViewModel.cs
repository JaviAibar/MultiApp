using System;
using System.Diagnostics;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace MultiApp.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private IRegionManager _regionManager;

        private string _title = "MultiApp";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private int _selectedTab;
        public int SelectedTab
        {
            get { return _selectedTab; }
            set { SetProperty(ref _selectedTab, value); }
        }



        public MainWindowViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

    }
}
