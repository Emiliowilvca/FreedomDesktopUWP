

using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Freedom.Core.CoreInterface;
using Freedom.UICore.Interface;
using Freedom.UICore.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Windows.Input;

namespace Freedom.UICore.BaseClass
{
    public class BaseViewModel : ObservableRecipient
    {
        public readonly string _prefix = "/api";
        public readonly string _version = "/v1";

        protected readonly IFindResourcesService _findResourcesService;
        protected readonly IFocusService _focusService;
        protected readonly IMapper _mapper;
        protected readonly IMessageService _messageService;
        protected readonly INavigationService _navigationService;
        protected readonly INetworkService _networkService;
        protected readonly IReloadService _reloadService;
        protected readonly IStatusBarService _statusBarService;
        protected readonly IStatusNavigateService _statusNavigateService;
        protected readonly IStatusNotifyService _statusNotifyService;
        protected readonly IVariableService _variableService;       
        public CancellationTokenSource _cancelTokenSource;
        protected string _sendMessageToken;

        public BaseViewModel()
        {
            PageTitle = new PageTitle();
            NavigateAction = OnNavigateTo;
            _findResourcesService = AppEssential.ServiceProvider.GetRequiredService<IFindResourcesService>();
            _focusService = AppEssential.ServiceProvider.GetRequiredService<IFocusService>();
            _mapper = AppEssential.ServiceProvider.GetRequiredService<IMapper>();
            _messageService = AppEssential.ServiceProvider.GetRequiredService<IMessageService>();
            _navigationService = AppEssential.ServiceProvider.GetRequiredService<INavigationService>();
            _networkService = AppEssential.ServiceProvider.GetRequiredService<INetworkService>();
            _reloadService = AppEssential.ServiceProvider.GetRequiredService<IReloadService>();
            _statusBarService = AppEssential.ServiceProvider.GetRequiredService<IStatusBarService>();
            _statusNavigateService = AppEssential.ServiceProvider.GetRequiredService<IStatusNavigateService>();
            _statusNotifyService = AppEssential.ServiceProvider.GetRequiredService<IStatusNotifyService>();
            _variableService = AppEssential.ServiceProvider.GetRequiredService<IVariableService>();             
            GoBackCommand = new RelayCommand(GoBackCommandExecute);
            IsCheckedAllAction = SelectAllItems;
        }

        #region Commands

        public ICommand GoBackCommand { get; private set; }

        public virtual void GoBackCommandExecute()
        {

        }

        public virtual void NavigationCommandExecute()
        {

        }

        public virtual void OnNavigateTo(object parameter)
        {

        }

        #endregion Commands

        #region Properties

        private bool _enableControl;

        private bool _isBusy;
        private bool _isCheckedAll;
        private string _title;
        private PageTitle _pageTitle;
        

        public int MaxItemSelectValue { get; set; }

        public Action<object> NavigateAction { get; set; }

        public bool EnableControl { get => _enableControl; set => SetProperty(ref _enableControl, value); }

        public bool IsBusy { get => _isBusy; set => SetProperty(ref _isBusy, value); }

        public bool IsCheckedAll
        {
            get => _isCheckedAll;
            set
            {
                SetProperty(ref _isCheckedAll, value);
                IsCheckedAllAction?.Invoke(_isCheckedAll);
            }
        }


        public Action<bool> IsCheckedAllAction { get; set; }

        public string Title { get => _title; set => SetProperty(ref _title, value); }

        public PageTitle PageTitle { get => _pageTitle; set => SetProperty(ref _pageTitle, value); }

       

        #endregion Properties

        public static void HandleException(Exception ex, string messageboxTitle = "error", string methodName = "No defined")
        {
            // _messageBoxService.ShowError(ex.Message, messageboxTitle);
            // _logService.LogError(ex, methodName);
        }

        public bool CanSubmitReverse()
        {
            return !EnableControl;
        }

        public bool CanSubmit()
        {
            return EnableControl;
        }

        public virtual void SelectAllItems(bool value)
        {

        }
        
        #region Notifications



        #endregion
    }
}