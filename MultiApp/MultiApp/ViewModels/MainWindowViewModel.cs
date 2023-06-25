using System;
using System.Diagnostics;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace MultiApp.ViewModels
{
    [Obsolete]
    public class MainWindowViewModel : BindableBase
    {
        private IRegionManager _regionManager;
        private string _title = "Prism Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public MainWindowViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            SelectedTab = 2;
            //regionManager.RequestNavigate("Visualizer3DRegion", "Visualizer3DViewModel");
        }


        private int _selectedTab;
        public int SelectedTab
        {
            get { return _selectedTab; }
            set { SetProperty(ref _selectedTab, value); }
        }

    }
}
