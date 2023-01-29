using OurBudgets.Services;
using OurBudgets.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace OurBudgets.ViewModels
{
    /// <summary>
    /// UserpopUpViewインターフェース
    /// </summary>
    public class UserPopUpViewModel : BindableBase, IDialogAware
    {
        IDialogService _dialogService;
        IMessageService _messageService;

        public UserPopUpViewModel(IDialogService dialogService) : this(dialogService, new MessageService()) 
        { 

        }
        public UserPopUpViewModel(IDialogService dialogService, IMessageService messageService)
        {
            _dialogService = dialogService;
            _messageService = messageService;

            //OKButtonインスタンス作成
            OKButton = new DelegateCommand(OKButtonExecute);
        }

        //クローズリクエスト
        public event Action<IDialogResult> RequestClose;

        //Properyフィールド
        private string _popUpViewTextBox = "XXX";
        

        // PopUpViewTextBoxプロパティ
        public string PopUpViewTextBox
        {
            get { return _popUpViewTextBox; }
            set { SetProperty(ref _popUpViewTextBox, value); }
        }

        // OKButtonデリゲータープロパティ
        public DelegateCommand OKButton { get; }


        /// <summary>
        /// OKButtonクリック時実行メソッド
        /// </summary>
        private void OKButtonExecute()
        {
            //var message = new DialogParameters();
            
            //message.Add(nameof(MessageBoxViewModel.Message), "Save");
            //_dialogService.ShowDialog(nameof(MessageBoxView), message, null);


            if(_messageService.Question("保存しますか？") == MessageBoxResult.OK)
            {
                _messageService.ShowDialog("保存しました。");
            }


            var pram = new DialogParameters();

            pram.Add(nameof(PopUpViewTextBox), PopUpViewTextBox);
            //OKButtonクリック時にクローズリクエストとパラメータをパラメータで返す
            RequestClose?.Invoke(new DialogResult(ButtonResult.OK, pram));
        }

        //popupWindowタイトル
        public string Title => "UserPopUpView";


        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {

        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            PopUpViewTextBox = parameters.GetValue<string>(nameof(PopUpViewTextBox));
        }
    }
}
