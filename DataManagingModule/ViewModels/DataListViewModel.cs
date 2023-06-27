using DataManagingModule.Interfaces;
using DataManagingModule.Model;
using DataManagingModule.Services;
using DataManagingModule.Views;
using MultiApp.Core;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Resources;
using System.Windows.Shapes;

namespace DataManagingModule.ViewModels
{
    public class DataListViewModel : BindableBase, INavigationAware
    {
        private IDialogService _dialogService;
        private IRegionManager _regionManager;
        private MillDataManager _dataManager;
        private Mill _selectedData;
        public Mill SelectedData
        {
            get { return _selectedData; }
            set { SetProperty(ref _selectedData, value); }
        }


        private int _selectedIndex;
        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set { SetProperty(ref _selectedIndex, value); }
        } 

        //private ObservableCollection<Mill> _millData;

        public ObservableCollection<Mill> MillData
        {
            get { return _dataManager.Data; }
            set { ObservableCollection<Mill> tmp = _dataManager.Data; SetProperty(ref tmp, value); }
        }

        private DelegateCommand _removeSelectedData;
        public DelegateCommand RemoveSelectedData =>
            _removeSelectedData ?? (_removeSelectedData = new DelegateCommand(ExecuteRemoveSelectedData));


        private DelegateCommand _saveChangesCommand;
        public DelegateCommand SaveChangesCommand =>
            //_saveChangesCommand ?? (_saveChangesCommand = new DelegateCommand(ExecuteSaveDataCommand, SaveChangesCanExecute).ObservesProperty(() => _dataManager.UnsavedChanges));
            _saveChangesCommand ?? (_saveChangesCommand = new DelegateCommand(ExecuteSaveDataCommand, SaveChangesCanExecute));


        private DelegateCommand _editDataCommand;

        public DelegateCommand EditDataCommand =>
            _editDataCommand ?? (_editDataCommand = new DelegateCommand(ExecuteEditDataCommand, CanExecuteEditDataCommand));

        void ExecuteEditDataCommand()
        {
            if (SelectedData == null)
            {
                MessageBox.Show("You must select a row before trying to edit it!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var navigationParams = new NavigationParameters
            {
                { "selectedMill", SelectedData }
            };
            //nameof(DataEditView)
            _regionManager.RequestNavigate(RegionNames.DataManagingRegion, "DataEditView", navigationParams);
        }

        bool CanExecuteEditDataCommand()
        {
            return true;
        }
        private bool SaveChangesCanExecute()
        {
            return _dataManager.UnsavedChanges;
        }

        void ExecuteSaveDataCommand()
        {
            if (MessageBox.Show("Are you sure you want to save changes", "Save changes", MessageBoxButton.YesNoCancel, MessageBoxImage.Question) == MessageBoxResult.Yes)
                _dataManager.SaveChanges();
        }

        void ExecuteRemoveSelectedData()
        {
            if (SelectedData == null)
            {
                MessageBox.Show("You must select a row before trying to delete it!", "Error", MessageBoxButton.OK, MessageBoxImage.Error); return;
            }

            _dialogService.ShowDialog(nameof(RemoveDataView), new DialogParameters($"message=Are you sure you want to remove the row \"{SelectedData.CaseName}\"?"), r =>
            {
                if (r.Result == ButtonResult.OK)
                    _dataManager.RemoveRow(SelectedData);
            });
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            var editedMill = navigationContext.Parameters["editedMill"] as Mill;
            if (editedMill != null)
            {
                var index = _dataManager.UpdateRow(SelectedData, editedMill);
                if (index > -1)
                    SelectedIndex = index;
            }
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        public DataListViewModel(IDialogService dialogService, IRegionManager regionManager)
        {
            //Uri uriData = new Uri(@"pack://application:,,,/DataManagingModule;component/Database/mill.csv");
            //Directory.GetFiles("./Database");
            //Uri uriData = new Uri(@"pack://application:,,,/Database/mill.csv");
            // Se usa el reader que corresponda al tipo de archivo que deseemos cargar
            _dialogService = dialogService;
            _regionManager = regionManager;
            _dataManager = new MillDataManager("./Database/mill.csv");
            _dataManager.unsavedChanged += SaveChangesCommand.RaiseCanExecuteChanged;
            SelectedIndex = -1;
            // _dataManager.PropertyChanged += 
            //MillData = new ObservableCollection<Mill>(ReadCSV(""));
        }

    }
}
