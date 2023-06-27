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
        // Fields
        public event Action<IDialogResult> RequestClose;
        // Services

        // Properties
        public string Title => "Delete data row";

        // Delegate backing fields
        private DelegateCommand<string> _closeDialogCommand;

        // Delegate Commands
        public DelegateCommand<string> CloseDialogCommand =>
            _closeDialogCommand ??= new DelegateCommand<string>(ExecuteCloseDialogCommand);


        // Properties backing field
        private string _message;

        // Observable Properties
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        // Constructor
        public RemoveDataViewModel()
        {
        }

        void ExecuteCloseDialogCommand(string result)
        {
            ButtonResult buttonResult = ButtonResult.None;

            if (result == "true")
            {
                buttonResult = ButtonResult.OK;
            }

            RequestClose?.Invoke(new DialogResult(buttonResult));
        }

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
