using OurBudgets.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace OurBudgets.ViewModels
{
    public class UserTabViewModel : BindableBase, INavigationAware, IConfirmNavigationRequest
    {
        IMessageService _messageService;

        public UserTabViewModel() : this(new MessageService())
        {

        }
        public UserTabViewModel(IMessageService messageService)
        {
            _messageService = messageService;
        }

        private string _label = string.Empty;

        public string Label
        {
            get { return _label; }
            set { SetProperty(ref _label, value); }
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            Label = navigationContext.Parameters.GetValue<string>(nameof(Label));
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            
        }

        public void ConfirmNavigationRequest(NavigationContext navigationContext, Action<bool> continuationCallback)
        {
            if (_messageService.Question("保存しないで閉じますか？") == System.Windows.MessageBoxResult.OK)
            {
                continuationCallback(true);
            }
        }
    }
}
