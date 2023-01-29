using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OurBudgets.ViewModels
{
    public class MessageBoxViewModel : BindableBase, IDialogAware
    {
        public MessageBoxViewModel()
        {
            OKButton = new DelegateCommand(OKButtonExecute);
        }

        //Messageフィールド
        private string _message = string.Empty;
        //Messageプロパティ
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        public DelegateCommand OKButton { get; }

        private void OKButtonExecute()
        {
            RequestClose?.Invoke(new DialogResult(ButtonResult.OK));
        }

        public string Title => "";

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
            Message = parameters.GetValue<string>(nameof(Message));
        }
    }
}
