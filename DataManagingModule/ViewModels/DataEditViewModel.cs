using DataManagingModule.Model;
using DataManagingModule.Views;
using MultiApp.Core;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace DataManagingModule.ViewModels
{
    public class DataEditViewModel : BindableBase, INavigationAware
    {

        private Mill _selectedMill;
        IRegionManager _regionManager;
        public Mill SelectedMill
        {
            get { return _selectedMill; }
            set { SetProperty(ref _selectedMill, value); }
        }

        private DelegateCommand _cancelCommand;

        public DelegateCommand CancelCommand =>
            _cancelCommand ?? (_cancelCommand = new DelegateCommand(ExecuteCancelCommand));


        private DelegateCommand _saveChangesCommand;
        public DelegateCommand SaveChangesCommand =>
            _saveChangesCommand ?? (_saveChangesCommand = new DelegateCommand(ExecuteSaveChangesCommand, CanExecuteSaveChangesCommand));


        void ExecuteSaveChangesCommand()
        {
            var parameters = new NavigationParameters {
            {
                "editedMill", SelectedMill
            }};
            _regionManager.RequestNavigate(RegionNames.DataManagingRegion, nameof(DataList), parameters);
        }

        // TODO: Composite?
        private bool CanExecuteSaveChangesCommand()
        {
            return true;
        }

        void ExecuteCancelCommand()
        {
            _regionManager.RequestNavigate(RegionNames.DataManagingRegion, nameof(DataList));
        }


        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            var selectedMill = Helper.CreateDeepCopy(navigationContext.Parameters["selectedMill"] as Mill);
            if (selectedMill != null)
                SelectedMill = selectedMill;
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return false;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        public DataEditViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }
    }
}
