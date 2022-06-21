using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using FluentValidation;
using Freedom.Core.ApiServiceInterface;
using Freedom.Core.CoreInterface;
using Freedom.Frontend.FontIcons;
using Freedom.Frontend.Models.BindableINFO;
using Freedom.UICore.BaseClass;
using Freedom.UICore.Models;
using Freedom.UICore.SendingMessages;
using Freedom.Utility;
using Freedom.Utility.Langs;
using Freedom.Utility.Request;
using Freedom.Utility.Responses;
using Windows.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Freedom.UICore.ViewModels.SearchViews
{
    public class PrioritySearchViewModel : BaseViewModel
    {
        private readonly string _controller = "/Priorities";
        private readonly string _limit = "/limit";
        private readonly string _searchMethod = "/searchText";
        private readonly IPriorityService _priorityService;
        private readonly ILoadMoreHelper _loadMoreHelper;

        public PrioritySearchViewModel(IPriorityService priorityService,
                                       ILoadMoreHelper loadMoreHelper)
        {
            _priorityService = priorityService;
            _loadMoreHelper = loadMoreHelper;
            PageTitle = new PageTitle(Lang.SearchOfPriority, MaterialDesignIcons.AlertBoxOutline);
            SearchCommand = new RelayCommand(SearchCommandExecute);
            ConfirmCommand = new RelayCommand(ConfirmCommandExecute);
            ReloadCommand = new RelayCommand(ReloadCommandExecute);
            LoadMoreCommand = new RelayCommand(LoadMoreCommandExecute);
            DeleteCommand = new RelayCommand<PriorityINFO>(DeleteCommandExecute);
            Collection = new ObservableCollection<PriorityINFO>();
            LoadCollection();
            
        }

        #region Commands

        public ICommand SearchCommand { get; private set; }

        public ICommand ConfirmCommand { get; private set; }

        public ICommand ReloadCommand { get; private set; }

        public ICommand LoadMoreCommand { get; private set; }

        public ICommand DeleteCommand { get; private set; }

        private async void DeleteCommandExecute(PriorityINFO entity)
        {
            try
            {
                if (!_networkService.CheckIfInternet())
                {
                    _statusNotifyService.ShowWarning(Lang.CheckInternetConnection);
                    return;
                }

                var result = await _messageService.ShowMessageConfirmation(Lang.Delete_Question, Lang.Delete);
                if (result == ContentDialogResult.Primary)
                {
                    _cancelTokenSource = new CancellationTokenSource();
                    _cancelTokenSource.CancelAfter(_findResourcesService.GetCancelTokenAfter());

                    IsBusy = true;

                    XResponse response = await _priorityService.DeleteAsync(_prefix, _version, _controller, "", entity.Id,
                                                                           _cancelTokenSource.Token, _variableService.Token);
                    if (!response.Success)
                    {
                        IsBusy = false;
                        _statusNotifyService.ShowError(response.ResultMessage);
                        return;
                    }
                    Collection.Remove(entity);
                    IsBusy = false;
                    _statusNotifyService.ShowSuccess(Lang.RecordsProcessedSuccessfully);
                }
            }
            catch (TaskCanceledException tce)
            {
                IsBusy = false;
                _statusNotifyService.ShowWarning(tce.Message);
            }
            finally
            {
                _cancelTokenSource.Dispose();
            }
        }

        private async void LoadMoreCommandExecute()
        {
            _cancelTokenSource = new CancellationTokenSource();
            _cancelTokenSource.CancelAfter(_findResourcesService.GetCancelTokenAfter());
            try
            {
                if (!string.IsNullOrEmpty(SearhText))
                {
                    _loadMoreHelper.OffSetStatus += _variableService.LimitItem;
                    RequestBySearchText request = _loadMoreHelper.GenerateRequest(_loadMoreHelper.OffSetStatus,
                                                                                  _variableService.LimitItem,
                                                                                  _variableService.CompanyRTO.Id,
                                                                                  SearhText);
                    await GetCollectionAsync(request, _searchMethod, false, _cancelTokenSource.Token);
                }
                else
                {
                    _loadMoreHelper.OffSetStatus += _variableService.LimitItem;
                    RequestByLimit requestLimit = _loadMoreHelper.GenerateRequestLimit(_variableService.CompanyRTO.Id,
                                                                                       _loadMoreHelper.OffSetStatus,
                                                                                       _variableService.LimitItem);
                    await GetCollectionAsync(requestLimit, _limit, false, _cancelTokenSource.Token);
                }
            }
            catch (Exception ex)
            {
                _messageService.ShowMessage(ex.Message, "Error on LoadMore");
            }
            finally
            {
                _cancelTokenSource.Dispose();
            }
        }

        private void ReloadCommandExecute()
        {
            LoadCollection();
        }

        private void ConfirmCommandExecute()
        {
            var SelCollection = Collection.Where(x => x.IsSelected == true).ToList();
            if (SelCollection.Count <= 0 || SelCollection.Count > MaxItemSelectValue)
            {
                _statusNotifyService.ShowWarning(Lang.ErrorWhenSelectingItem);
                return;
            }
            WeakReferenceMessenger.Default.Send(new VMSendMessage<PriorityINFO>(SelCollection), _sendMessageToken);
            _navigationService.GoBack();
        }

        private async void SearchCommandExecute()
        {
            if (string.IsNullOrEmpty(SearhText)) return;

            RequestBySearchText request = _loadMoreHelper.GenerateRequest(0, _variableService.LimitItem,
                                                                        _variableService.CompanyRTO.Id, SearhText);
            _cancelTokenSource = new CancellationTokenSource();
            _cancelTokenSource.CancelAfter(_findResourcesService.GetCancelTokenAfter());
            try
            {
                await GetCollectionAsync(request, _searchMethod, true, _cancelTokenSource.Token);
            }
            catch (Exception ex)
            {
                _messageService.ShowMessage(ex.Message, "Error on SearchCommandExecute");
            }
            finally
            {
                _cancelTokenSource.Dispose();
            }
        }

        #endregion Commands

        #region Properties

        private string _searhText;

        public string SearhText { get => _searhText; set => SetProperty(ref _searhText, value); }

        private ObservableCollection<PriorityINFO> _collection;

        public ObservableCollection<PriorityINFO> Collection
        {
            get => _collection;
            set => SetProperty(ref _collection, value);
        }

        //properties

        #endregion Properties

        #region Methods

        private async void LoadCollection()
        {
            RequestByLimit request = _loadMoreHelper.GenerateRequestLimit(_variableService.CompanyRTO.Id, 0,
                                                                         _variableService.LimitItem);
            _cancelTokenSource = new CancellationTokenSource();
            _cancelTokenSource.CancelAfter(_findResourcesService.GetCancelTokenAfter());

            try
            {
                await GetCollectionAsync(request, _limit, true, _cancelTokenSource.Token);
            }
            catch (Exception ex)
            {
                _messageService.ShowMessage(ex.Message, "Error on LoadCollection");
            }
            finally
            {
                _cancelTokenSource.Dispose();
            }
        }

        private async Task GetCollectionAsync<T>(T request, string methodName, bool clearBeforeLoading, CancellationToken token) where T : class
        {
            IsBusy = true;

            var response = await _priorityService.GetListAsync(_prefix, _version, _controller, methodName,
                                                                               request, token, _variableService.Token);
            if (!response.IsSuccess)
            {
                IsBusy = false;
                throw new Exception($"{response.Message}");
            }
            var collection = _mapper.Map<List<PriorityINFO>>(response.Collection);
            if (clearBeforeLoading)
            {
                _loadMoreHelper.OffSetStatus = 0;
                Collection.Clear();
            }
            
            Collection.AddRange(collection);
            EnableControl = _loadMoreHelper.ResolveLoadMore(collection.Count, _variableService.LimitItem);
            IsBusy = false;
        }

        public override void SelectAllItems(bool value)
        {
            base.SelectAllItems(value);
            Collection.ToList().ForEach(x => x.IsSelected = value);
        }

        public override void OnNavigateTo(object parameter)
        {
            base.OnNavigateTo(parameter);

            if (parameter != null && parameter is NavigationParameter param)
            {
                MaxItemSelectValue = param.MaxItemSelect;
                _sendMessageToken = param.ParameterId;
            };
        }

        #endregion Methods
    }
}