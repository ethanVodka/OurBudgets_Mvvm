using Prism.Mvvm;
using Prism.Commands;
using OurBudgets.Views;
using System;
using Prism.Regions;
using Prism.Services.Dialogs;

namespace OurBudgets.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        //Navigation現在地情報取得
        private readonly IRegionManager _regionManager;
        //Dialog情報取得
        private readonly IDialogService _dialogService;

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="regionManager">CurrenNavigation</param>
        /// <param name="dialogService">DialogInformation</param>
        public MainWindowViewModel(IRegionManager regionManager, IDialogService dialogService)
        {
            _regionManager = regionManager;
            _dialogService = dialogService;

            //Commandインスタンス作成
            UpdateSystemDate = new DelegateCommand(SystemDateUpdateExecute);
            //ShowUserHomeViewインスタンス作成
            ShowUserHomeView = new DelegateCommand(ShowUserHomeViewExecute);
            //ShowUserTabViewインスタンス作成
            ShowUserTabView = new DelegateCommand(ShowUserTabViewExecute);
            //userPopUpViewインスタンス作成
            ShowUserPopUpView = new DelegateCommand(ShowUserPopUpViewExecute);
        }


        /// <summary>
        /// プロパティーフィールド
        /// </summary>
        private string _title = "OurBudgets";
        private string _systemDate = System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

        #region Propertys
        // Tileプロパティ
        public string Title
        {
            get { return _title; }
            set
            {
                //プロパティの変更をViewへ通達
                SetProperty(ref _title, value);
            }
        }

        // SystemDateプロパティ
        public string SystemDate
        {
            get { return _systemDate; }
            set
            {
                //プロパティの変更をViewへ通達
                SetProperty(ref _systemDate, value);
            }
        }

        // UpdateSystemDateButtonデリゲータープロパティ
        public DelegateCommand UpdateSystemDate { get; }

        // ShowUserHomeViewButtonデリゲータープロパティ
        public DelegateCommand ShowUserHomeView { get; }
        // ShowUserTabViewButtonデリゲータープロパティ
        public DelegateCommand ShowUserTabView { get; }

        // ShowUserPopUpViewButtonデリゲータープロパティ
        public DelegateCommand ShowUserPopUpView { get; }
        #endregion


        #region Methods Called from Delegater
        /// <summary>
        /// UpdateSystemDateButtonクリック時実行メソッド
        /// </summary>
        private void SystemDateUpdateExecute()
        {
            SystemDate = System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
        }
        /// <summary>
        /// ShowUserHomeViewButtonクリック時実行メソッド
        /// </summary>
        private void ShowUserHomeViewExecute()
        {
            _regionManager.RequestNavigate("ContentRegion", nameof(UserHomeView));
        }
        /// <summary>
        /// ShowUserTabViewButtonクリック時実行メソッド
        /// </summary>
        private void ShowUserTabViewExecute()
        {
            var pram = new NavigationParameters();


            pram.Add(nameof(UserTabViewModel.Label), SystemDate);
            _regionManager.RequestNavigate("ContentRegion", nameof(UserTabView), pram);
        }
        /// <summary>
        /// ShowUserPopUpViewButtonクリック時実行メソッド
        /// </summary>
        private void ShowUserPopUpViewExecute()
        {
            var pram = new DialogParameters();
            pram.Add(nameof(UserPopUpViewModel.PopUpViewTextBox), SystemDate);

            _dialogService.ShowDialog(nameof(UserPopUpView), pram, PopUpViewClose);
        }
        #endregion

        /// <summary>
        /// userPopUpViewClos時処理メソッド
        /// パラメータ受け取り用
        /// </summary>
        private void PopUpViewClose(IDialogResult dialogResult)
        {
            SystemDate = dialogResult.Parameters.GetValue<string>(nameof(UserPopUpViewModel.PopUpViewTextBox));
        }

    }
}
