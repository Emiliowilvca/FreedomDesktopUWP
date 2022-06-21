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
using Freedom.Utility.Models.RTO;
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
    public class OperationTypeSearchViewModel : BaseViewModel
    {
        private readonly string _controller = "/OperationTypes";
        private readonly string _limit = "/limit";
        private readonly string _searchMethod = "/searchtext";
        private readonly IOperationTypeService _operationTypeService;
        private readonly ILoadMoreHelper _loadMoreHelper;

        public OperationTypeSearchViewModel(IOperationTypeService operationTypeService,
                                            ILoadMoreHelper loadMoreHelper)
        {
             PageTitle = new PageTitle(Lang.SearchOfOperationTypes, MaterialDesignIcons.Gavel);
            _operationTypeService = operationTypeService;
            _loadMoreHelper = loadMoreHelper;
            OperationTypeCollection = new ObservableCollection<OperationTypeINFO>();
            SearchCommand = new RelayCommand(SearchCommandExecute);
            ConfirmCommand = new RelayCommand(ConfirmCommandExecute);
            ReloadCommand = new RelayCommand(ReloadCommandExecute);
            DeleteCommand = new RelayCommand<OperationTypeINFO>(DeleteCommandExecute);
            LoadMoreCommand = new RelayCommand(LoadMoreCommandExecute);
            InitializeCollection();
        }

        #region Commands

        public ICommand SearchCommand { get; private set; }

        public ICommand ConfirmCommand { get; private set; }

        public ICommand ReloadCommand { get; private set; }

        public ICommand LoadMoreCommand { get; private set; }

        public ICommand DeleteCommand { get; private set; }

        private async void SearchCommandExecute()
        {
            if (string.IsNullOrEmpty(SearhText))
                return;

            RequestBySearchText request = _loadMoreHelper.GenerateRequest(0, _variableService.LimitItem,
                                                                        _variableService.CompanyRTO.Id, SearhText);
            _cancelTokenSource = new CancellationTokenSource();
            _cancelTokenSource.CancelAfter(_findResourcesService.GetCancelTokenAfter());
            try
            {
                await LoadCollection(request, _searchMethod, true, _cancelTokenSource.Token);
            }
            catch (Exception ex)
            {
                _messageService.ShowMessage(ex.Message, "Error ");
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

                    await LoadCollection(request, _searchMethod, false, _cancelTokenSource.Token);
                }
                else
                {
                    _loadMoreHelper.OffSetStatus += _variableService.LimitItem;
                    RequestByLimit requestLimit = _loadMoreHelper.GenerateRequestLimit(_variableService.CompanyRTO.Id,
                                                                                       _loadMoreHelper.OffSetStatus,
                                                                                       _variableService.LimitItem);
                    await LoadCollection(requestLimit, _limit, false, _cancelTokenSource.Token);
                }
            }
            catch (Exception ex)
            {
                _messageService.ShowMessage(ex.Message, "Error");
            }
            finally
            {
                _cancelTokenSource.Dispose();
            }
        }

        private void ReloadCommandExecute()
        {
            InitializeCollection();
        }

        private void ConfirmCommandExecute()
        {
            List<OperationTypeINFO> collection = OperationTypeCollection.ToList().Where(x => x.IsSelected == true).ToList();
            if (collection.Count <= 0 || collection.Count > MaxItemSelectValue)
            {
                _statusNotifyService.ShowWarning(Lang.ErrorWhenSelectingItem);
                return;
            }

            WeakReferenceMessenger.Default.Send(new VMSendMessage<OperationTypeINFO>(collection), _sendMessageToken);
            _navigationService.TryGoBack();
        }

        private async void DeleteCommandExecute(OperationTypeINFO operationType)
        {
            if (operationType == null) return;

            if (!_networkService.CheckIfInternet())
            {
                _statusNotifyService.ShowWarning(Lang.CheckInternetConnection);
                return;
            }
            var result = await _messageService.ShowMessageConfirmation(Lang.Delete_Question, Lang.Delete);

            if (result != ContentDialogResult.Primary)
            {
                return;
            }

            _cancelTokenSource = new CancellationTokenSource();
            _cancelTokenSource.CancelAfter(_findResourcesService.GetCancelTokenAfter());

            try
            {
                await Delete(operationType, _cancelTokenSource.Token);
            }
            catch (Exception ex)
            {
                IsBusy = false;
                _messageService.ShowMessage(ex.Message, "Error on delete");
            }
            finally
            {
                _cancelTokenSource.Dispose();
            }
        }

        #endregion Commands

        #region Properties

        public OperationTypeParameter OperationTypeParameter { get; set; }

        private string _searhText;

        public string SearhText { get => _searhText; set => SetProperty(ref _searhText, value); }

        #endregion Properties

        #region Collections

        private ObservableCollection<OperationTypeINFO> _operationTypeCollection;

        public ObservableCollection<OperationTypeINFO> OperationTypeCollection
        {
            get => _operationTypeCollection;
            set => SetProperty(ref _operationTypeCollection, value);
        }

        #endregion Collections

        #region Methods

        private async Task Delete(OperationTypeINFO operationType, CancellationToken token)
        {
            IsBusy = true;
            XResponse response = await _operationTypeService.DeleteAsync(_prefix, _version, _controller, "", operationType.Id,
                                                         token, _variableService.Token);
            if (!response.Success)
            {
                IsBusy = false;
                throw new Exception(response.ResultMessage);
            }
            OperationTypeCollection.Remove(operationType);
            IsBusy = false;
            _statusNotifyService.ShowSuccess(Lang.RecordsProcessedSuccessfully);
        }

        private async void InitializeCollection()
        {
            RequestByLimit request = _loadMoreHelper.GenerateRequestLimit(_variableService.CompanyRTO.Id, 0, 100);
            _cancelTokenSource = new CancellationTokenSource();
            _cancelTokenSource.CancelAfter(_findResourcesService.GetCancelTokenAfter());
            try
            {
                await LoadCollection(request, _limit, true, _cancelTokenSource.Token);
            }
            catch (Exception ex)
            {
                _messageService.ShowMessage(ex.Message, "Error Initialize Collection");
            }
            finally
            {
                _cancelTokenSource.Dispose();
            }
        }

        private async Task LoadCollection<T>(T request, string methodName, bool clearBeforeLoading, CancellationToken token) where T : class
        {
            IsBusy = true;
            ListResponse<OperationTypeRTO> response = await _operationTypeService.GetListAsync(_prefix, _version, _controller, methodName,
                                                                               request, token, _variableService.Token);
            if (!response.IsSuccess)
            {
                IsBusy = false;
                throw new Exception($"StatusCode = {response.StatusCode} | {response.Message}");
            }

            List<OperationTypeRTO> colSelect = response.Collection.Where(x => OperationTypeParameter.SubTypes.Contains(x.SubclassId)).ToList();

            List<OperationTypeINFO> collection = _mapper.Map<List<OperationTypeINFO>>(colSelect);
            if (clearBeforeLoading)
            {
                _loadMoreHelper.OffSetStatus = 0;
                OperationTypeCollection.Clear();
            }
            OperationTypeCollection.AddRange(collection);
            EnableControl = _loadMoreHelper.ResolveLoadMore(collection.Count, _variableService.LimitItem);
            IsBusy = false;
        }

        public override void SelectAllItems(bool value)
        {
            base.SelectAllItems(value);
            OperationTypeCollection.ToList().ForEach(x => x.IsSelected = value);
        }

        public override void OnNavigateTo(object parameter)
        {
            base.OnNavigateTo(parameter);
            if (parameter != null && parameter is OperationTypeParameter param)
            {
                OperationTypeParameter = param;
                MaxItemSelectValue = OperationTypeParameter.MaxItemSelect;
                _sendMessageToken = OperationTypeParameter.Token;
            }
        }

        #endregion Methods
    }
}