using DataManagingModule.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataManagingModule.ViewModels
{
    public class RemoveDataViewModel : BindableBase, IDialogAware
    {
        private DelegateCommand<string> _closeDialogCommand;
        public DelegateCommand<string> CloseDialogCommand =>
            _closeDialogCommand ?? (_closeDialogCommand = new DelegateCommand<string>(ExecuteCloseDialogCommand));

        void ExecuteCloseDialogCommand(string result)
        {
            ButtonResult buttonResult = ButtonResult.None;

            if (result == "true")
            {
                buttonResult = ButtonResult.OK;
            }

            RequestClose?.Invoke(new DialogResult(buttonResult));
        }

        public RemoveDataViewModel()
        {
        }

        public string Title => "Delete data row";



        private string _message;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        public event Action<IDialogResult> RequestClose;

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            Message = parameters.GetValue<string>("message");
        }
    }
}
